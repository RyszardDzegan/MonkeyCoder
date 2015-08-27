using MonkeyCoder.Functions.Helpers;
using MonkeyCoder.Math;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MonkeyCoder.Functions
{
    internal class Mandatory : IEnumerable<Func<object>>
    {
        private Lazy<FunctionsEnumerable> Invocations { get; }

        public Mandatory(Delegate function, IReadOnlyCollection<object> possibleArguments, object mandatoryArgument)
        {
            if (function == null)
                throw new ArgumentNullException(nameof(function), "Function cannot be null.");

            if (possibleArguments == null)
                throw new ArgumentNullException(nameof(possibleArguments), "Possible arguments cannot be null.");

            var parameters = function.Method.GetParameters();
            var mandatory = new Argument.Basic(mandatoryArgument);
            var argumentList = Argument.Basic.Get(possibleArguments).ToList();
            var distinctParameters = Parameter.GetDistinct(parameters);
            var distinctRelations = Relation.Mandatory.GetDistinct(distinctParameters, argumentList, mandatory);
            var relationList = Relation.Get(parameters, distinctRelations).ToList();

            var optionalPositions = relationList
                .Select((x, i) => new { Relation = x, Index = i })
                .ToList();

            var mandatoryPositions = relationList
                .Select((x, i) => new { Relation = x, Index = i })
                .Where(x => x.Relation.IsAssignableFromMandatoryArgument)
                .ToList();

            var mandatoryArgumentList = new[] { mandatoryArgument };

            var functions =
                from mandatoryPositionsSubset in mandatoryPositions.AsPowerSet().Skip(1)
                let possibleValues =
                    from optionalPosition in optionalPositions
                    join mandatoryPosition in mandatoryPositionsSubset on optionalPosition equals mandatoryPosition into jointMandatoryPositions
                    let possibleValues = jointMandatoryPositions.Any() ? mandatoryArgumentList : optionalPosition.Relation.Arguments.Select(x => x.Value).ToArray()
                    select possibleValues
                from valueList in possibleValues.AsCartesianProduct()
                let valueArray = valueList.ToArray()
                select new Func<object>(() => function.DynamicInvoke(valueArray));

            Invocations = FunctionsEnumerable.Lazy(functions);
        }

        public IEnumerator<Func<object>> GetEnumerator() => Invocations.Value.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
