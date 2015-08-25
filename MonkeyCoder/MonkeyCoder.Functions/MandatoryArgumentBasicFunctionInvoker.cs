using MonkeyCoder.Math;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MonkeyCoder.Functions
{
    internal class MandatoryArgumentBasicFunctionInvoker : IEnumerable<Func<object>>
    {
        public Delegate Function { get; }
        public IReadOnlyCollection<object> PossibleArguments { get; }
        public object MandatoryArgument { get; }
        
        public MandatoryArgumentBasicFunctionInvoker(Delegate function, IReadOnlyCollection<object> possibleArguments, object mandatoryArgument)
        {
            if (function == null)
                throw new ArgumentNullException(nameof(function), "Function cannot be null.");

            if (possibleArguments == null)
                throw new ArgumentNullException(nameof(possibleArguments), "Possible arguments cannot be null.");

            Function = function;
            PossibleArguments = possibleArguments;
            MandatoryArgument = mandatoryArgument;
        }

        public IEnumerator<Func<object>> GetEnumerator()
        {
            var parameters = Function.Method.GetParameters();
            var mandatoryArgumentInfo = new ArgumentInfo.Basic(MandatoryArgument);

            var possibleArgumentsInfo = ArgumentInfo.Basic.Get(PossibleArguments).ToList();
            var parametersInfo = ParameterInfo.GetDistinct(parameters).ToList();
            var distinctRelationsQuery = RelationsInfo.Mandatory.GetDistinct(parametersInfo, possibleArgumentsInfo, mandatoryArgumentInfo);
            var relationsQuery = RelationsInfo.Get(parameters, distinctRelationsQuery);
            var relations = relationsQuery.ToList();
            
            var mandatoryPositions = relations
                .Select((x, i) => new { Association = x, Index = i })
                .Where(x => x.Association.IsAssignableFromMandatoryArgument)
                .ToList();

            var mandatoryArgumentList = new[] { MandatoryArgument };

            var positionsToReplacePowerSet = mandatoryPositions
                .AsPowerSet()
                .Skip(1); // Skip empty set

            foreach (var positionsToReplace in positionsToReplacePowerSet)
            {
                var associationValuesQuery =
                    from association in relations
                    let valuesQuery =
                        from argument in association.ArgumentsInfo
                        select argument.Value
                    let values = valuesQuery.ToArray()
                    select values;
                var associationValues = associationValuesQuery.ToList();

                foreach (var positionToReplace in positionsToReplace)
                    associationValues[positionToReplace.Index] = mandatoryArgumentList;

                var valuesCollection =
                    from valuesList in associationValues.AsCartesianProduct()
                    select valuesList.ToArray();

                foreach (var values in valuesCollection)
                    yield return new Func<object>(() => Function.DynamicInvoke(values));
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
