using MonkeyCoder.Math;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MonkeyCoder.Functions
{
    internal class BasicFunctionInvoker : IEnumerable<Func<object>>
    {
        private Lazy<IEnumerable<Func<object>>> Invocations { get; }

        public BasicFunctionInvoker(Delegate function, IReadOnlyCollection<object> possibleArguments)
        {
            if (function == null)
                throw new ArgumentNullException(nameof(function), "Function cannot be null.");

            if (possibleArguments == null)
                throw new ArgumentNullException(nameof(possibleArguments), "Possible arguments cannot be null.");

            var parameters = function.Method.GetParameters();
            var distinctParameterTypeInfo = ParameterInfo.GetDistinct(parameters).ToList();
            var possibleArgumentsInfo = ArgumentInfo.Basic.Get(possibleArguments).ToList();
            var distinctRelationsQuery = RelationsInfo.Basic.GetDistinct(distinctParameterTypeInfo, possibleArgumentsInfo);
            var relationsQuery = RelationsInfo.Get(parameters, distinctRelationsQuery);

            var argumentsQuery =
                from argumentsInfo in relationsQuery.Select(x => x.ArgumentsInfo).AsCartesianProduct()
                select argumentsInfo.Select(x => x.Value).ToArray();

            var invocations =
                from values in argumentsQuery
                select new Func<object>(() => function.DynamicInvoke(values));

            Invocations = new Lazy<IEnumerable<Func<object>>>(() => invocations);
        }

        public IEnumerator<Func<object>> GetEnumerator() => Invocations.Value.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
