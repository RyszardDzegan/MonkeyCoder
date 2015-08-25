using MonkeyCoder.Math;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MonkeyCoder.Functions
{
    internal class ParameterlessDelegatesExpandingFunctionInvoker : IEnumerable<Func<object>>
    {
        private Lazy<IEnumerable<Func<object>>> Invocations { get; }

        public ParameterlessDelegatesExpandingFunctionInvoker(Delegate function, IReadOnlyCollection<object> possibleArguments)
        {
            if (function == null)
                throw new ArgumentNullException(nameof(function), "Function cannot be null.");

            if (possibleArguments == null)
                throw new ArgumentNullException(nameof(possibleArguments), "Possible arguments cannot be null.");
            
            var parameters = function.Method.GetParameters();
            var distinctParameterTypeInfo = ParameterInfo.GetDistinct(parameters).ToList();

            var possibleArgumentsInfo1 = ArgumentInfo.Basic.Get(possibleArguments).ToList();
            var distinctRelationsQuery1 = RelationsInfo.Basic.GetDistinct(distinctParameterTypeInfo, possibleArgumentsInfo1);

            var possibleArgumentsInfo2 = ArgumentInfo.DelgateExpander.Get(possibleArguments).ToList();
            var distinctRelationsQuery2 = RelationsInfo.Basic.GetDistinct(distinctParameterTypeInfo, possibleArgumentsInfo2);

            var distinctRelationsUnionQuery = RelationsInfo.Union(distinctRelationsQuery1, distinctRelationsQuery2);
            var relationsQuery = RelationsInfo.Get(parameters, distinctRelationsUnionQuery);

            var argumentsQuery =
                from argumentsInfo in relationsQuery.Select(x => x.ArgumentsInfo).AsCartesianProduct()
                select argumentsInfo.Select(x => !(x is ArgumentInfo.DelgateExpander) ? x.Value : ((Delegate)x.Value).DynamicInvoke()).ToArray();

            var invocations =
                from values in argumentsQuery
                select new Func<object>(() => function.DynamicInvoke(values));

            Invocations = new Lazy<IEnumerable<Func<object>>>(() => invocations);
        }

        public IEnumerator<Func<object>> GetEnumerator() => Invocations.Value.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
