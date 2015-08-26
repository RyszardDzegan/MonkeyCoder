using MonkeyCoder.Math;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MonkeyCoder.Functions
{
    internal class MandatoryArgumentBasicFunctionInvoker : IEnumerable<Func<object>>
    {
        private Lazy<IEnumerable<Func<object>>> Invocations { get; }

        public MandatoryArgumentBasicFunctionInvoker(Delegate function, IReadOnlyCollection<object> possibleArguments, object mandatoryArgument)
        {
            if (function == null)
                throw new ArgumentNullException(nameof(function), "Function cannot be null.");

            if (possibleArguments == null)
                throw new ArgumentNullException(nameof(possibleArguments), "Possible arguments cannot be null.");

            var parameters = function.Method.GetParameters();
            var mandatoryArgumentInfo = new ArgumentInfo.Basic(mandatoryArgument);
            var possibleArgumentsInfo = ArgumentInfo.Basic.Get(possibleArguments).ToList();
            var parametersInfo = ParameterInfo.GetDistinct(parameters).ToList();
            var distinctRelationsQuery = RelationsInfo.Mandatory.GetDistinct(parametersInfo, possibleArgumentsInfo, mandatoryArgumentInfo);
            var relationsQuery = RelationsInfo.Get(parameters, distinctRelationsQuery);
            var relations = relationsQuery.ToList();

            var optionalPositions = relations
                .Select((x, i) => new { Relation = x, Index = i })
                .ToList();

            var mandatoryPositions = relations
                .Select((x, i) => new { Relation = x, Index = i })
                .Where(x => x.Relation.IsAssignableFromMandatoryArgument)
                .ToList();

            var mandatoryArgumentList = new[] { mandatoryArgument };

            var invocations =
                from mandatoryPositionsSubset in mandatoryPositions.AsPowerSet().Skip(1)
                let possibleValues =
                    from optionalPosition in optionalPositions
                    join mandatoryPosition in mandatoryPositionsSubset on optionalPosition equals mandatoryPosition into jointMandatoryPositions
                    let possibleValues = jointMandatoryPositions.Any() ? mandatoryArgumentList : optionalPosition.Relation.ArgumentsInfo.Select(x => x.Value).ToArray()
                    select possibleValues
                from valueList in possibleValues.AsCartesianProduct()
                let values = valueList.ToArray()
                select new Func<object>(() => function.DynamicInvoke(values));

            Invocations = new Lazy<IEnumerable<Func<object>>>(() => invocations);
        }

        public IEnumerator<Func<object>> GetEnumerator() => Invocations.Value.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
