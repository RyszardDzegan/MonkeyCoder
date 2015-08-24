using MonkeyCoder.Math;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MonkeyCoder.Functions
{
    internal class SingleFunctionInvokerWithMandatoryArgument : ISingleFunctionInvoker
    {
        public Delegate Function { get; }
        public IReadOnlyCollection<object> PossibleArguments { get; }
        public object MandatoryArgument { get; }
        
        public SingleFunctionInvokerWithMandatoryArgument(Delegate function, IReadOnlyCollection<object> possibleArguments, object mandatoryArgument)
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
            var method = Function.Method;
            var parameters = method.GetParameters();
            var nullableType = typeof(Nullable<>);
            
            var argumentsQuery =
                from argument in PossibleArguments
                let type = argument != null ? argument.GetType() : null
                select new { Value = argument, Type = type };
            var arguments = argumentsQuery.ToList();

            var distinctParameters = parameters
                .Select(x => x.ParameterType)
                .Distinct(TypeFullNameEqualityComparer.Instance)
                .ToList();

            var distinctParametersQuery =
                from type in distinctParameters
                let genericArgument = type.GetGenericArguments().FirstOrDefault()
                let isNullable = new Lazy<bool>(() =>
                {
                    var genericArguments = type.GetGenericArguments();
                    if (genericArguments.Count() != 1) return false;
                    var genericArgument = genericArguments.First();
                    if (!genericArgument.IsValueType || genericArgument.FullName.StartsWith("System.Nullable`1")) return false;
                    return nullableType.MakeGenericType(genericArgument).IsAssignableFrom(type);
                })
                select new { Type = type, IsNullable = isNullable };
            var distinctParameterTypes = distinctParametersQuery.ToList();

            var mandatoryArgumentType = MandatoryArgument != null ? MandatoryArgument.GetType() : null;
            var isMandatoryArgumentNotNull = mandatoryArgumentType != null;

            var distinctAssociationsQuery =
                from parameter in distinctParameterTypes
                let isMandatoryArgumentAssignable = isMandatoryArgumentNotNull && parameter.Type.IsAssignableFrom(mandatoryArgumentType)
                let isMandatoryArgumentReference = !isMandatoryArgumentNotNull && !parameter.Type.IsValueType
                let isMandatoryArgumentNullable = !isMandatoryArgumentNotNull && parameter.IsNullable.Value
                let isAssignableFromMandatoryArgument = isMandatoryArgumentAssignable || isMandatoryArgumentReference || isMandatoryArgumentNullable
                let innerArgumentsQuery =
                    from argument in arguments
                    let isNotNull = argument.Type != null
                    let isAssignable = isNotNull && parameter.Type.IsAssignableFrom(argument.Type)
                    let isReference = !isNotNull && !parameter.Type.IsValueType
                    let isNullable = !isNotNull && parameter.IsNullable.Value
                    where isAssignable || isReference || isNullable
                    select argument
                let innerArguments = innerArgumentsQuery.ToList()
                select new { Parameter = parameter, Arguments = innerArguments, IsAssignableFromMandatoryArgument = isAssignableFromMandatoryArgument };
            var distinctAssociations = distinctAssociationsQuery.ToList();

            var associationsQuery =
                from parameter in parameters
                join association in distinctAssociations on parameter.ParameterType.FullName equals association.Parameter.Type.FullName
                select association;
            var associations = associationsQuery.ToList();
            
            var mandatoryPositions = associations
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
                    from association in associations
                    let valuesQuery =
                        from argument in association.Arguments
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
