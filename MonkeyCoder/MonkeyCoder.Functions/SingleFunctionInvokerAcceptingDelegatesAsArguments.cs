using MonkeyCoder.Math;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MonkeyCoder.Functions
{
    internal class SingleFunctionInvokerAcceptingDelegatesAsArguments : ISingleFunctionInvoker
    {
        public Delegate Function { get; }
        public IReadOnlyCollection<object> PossibleArguments { get; }

        public SingleFunctionInvokerAcceptingDelegatesAsArguments(Delegate function, params object[] possibleArguments)
            : this(function, (IReadOnlyCollection<object>)possibleArguments)
        { }

        public SingleFunctionInvokerAcceptingDelegatesAsArguments(Delegate function, IReadOnlyCollection<object> possibleArguments)
        {
            if (function == null)
                throw new ArgumentNullException(nameof(function), "Function cannot be null.");

            if (possibleArguments == null)
                throw new ArgumentNullException(nameof(possibleArguments), "Possible arguments cannot be null.");

            Function = function;
            PossibleArguments = possibleArguments;
        }

        public IEnumerator<Func<object>> GetEnumerator()
        {
            var method = Function.Method;
            var parameters = method.GetParameters();
            var nullableType = typeof(Nullable<>);

            if (!parameters.Any())
            {
                yield return new Func<object>(() => Function.DynamicInvoke());
                yield break;
            }

            var argumentsQuery =
                from argument in PossibleArguments
                let type = argument != null ? argument.GetType() : null
                let delegateMethodInfo = (argument as Delegate)?.Method
                let isDelegate = delegateMethodInfo != null
                let isParameterLessDelegate = isDelegate && !delegateMethodInfo.GetParameters().Any()
                let delegateReturnType = delegateMethodInfo?.ReturnType
                select new { Value = argument, Type = type, IsDelegate = isParameterLessDelegate, DelegateReturnType = delegateReturnType };
            var argumentsList = argumentsQuery.ToList();

            var distinctParameters = parameters
                .Select(x => x.ParameterType)
                .Distinct(TypeFullNameEqualityComparer.Instance)
                .ToList();

            var distinctParametersQuery =
                from type in distinctParameters
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

            var distinctAssociationsQuery =
                from parameter in distinctParameterTypes
                let arguments =
                    from argument in argumentsList
                    let isNotNull = argument.Type != null
                    let isAssignable = isNotNull && parameter.Type.IsAssignableFrom(argument.Type)
                    let isReference = !isNotNull && !parameter.Type.IsValueType
                    let isNullable = !isNotNull && parameter.IsNullable.Value
                    let isSimpleValue = isAssignable || isReference || isNullable
                    let isDelegateAssignable = argument.IsDelegate && argument.DelegateReturnType != null && parameter.Type.IsAssignableFrom(argument.DelegateReturnType)
                    let isDelegateReference = argument.IsDelegate && argument.DelegateReturnType == null && !parameter.Type.IsValueType
                    let isDelegateNullable = argument.IsDelegate && argument.DelegateReturnType == null && parameter.IsNullable.Value
                    let isDelegateValue = isDelegateAssignable || isDelegateReference || isDelegateNullable
                    where isSimpleValue || isDelegateValue
                    select new { Value = argument.Value, IsDelegate = isDelegateValue }
                select new { Parameter = parameter, Arguments = arguments.ToArray() };
            var distinctAssociations = distinctAssociationsQuery.ToList();

            var associationsQuery =
                from parameter in parameters
                join association in distinctAssociations on parameter.ParameterType.FullName equals association.Parameter.Type.FullName
                select association;
            var associations = associationsQuery.ToList();

            var cartesianProductInput = associations
                .Select(x => x.Arguments)
                .ToList();

            var valuesCollection =
                from product in cartesianProductInput.AsCartesianProduct()
                let values =
                    from argument in product
                    let value = !argument.IsDelegate ? argument.Value : ((Delegate)argument.Value).DynamicInvoke()
                    select value
                select values.ToArray();

            foreach (var values in valuesCollection)
                yield return new Func<object>(() => Function.DynamicInvoke(values));
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
