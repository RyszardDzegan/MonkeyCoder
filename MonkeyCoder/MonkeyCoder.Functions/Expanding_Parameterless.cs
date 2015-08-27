using MonkeyCoder.Functions.Helpers;
using MonkeyCoder.Math;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MonkeyCoder.Functions
{
    internal partial class Expanding
    {
        internal class Parameterless : IEnumerable<Func<object>>
        {
            private Lazy<FunctionsEnumerable> Invocations { get; }

            public Parameterless(Delegate function, IReadOnlyCollection<object> possibleArguments)
            {
                if (function == null)
                    throw new ArgumentNullException(nameof(function), "Function cannot be null.");

                if (possibleArguments == null)
                    throw new ArgumentNullException(nameof(possibleArguments), "Possible arguments cannot be null.");

                var parameters = function.Method.GetParameters();

                if (!parameters.Any())
                {
                    Invocations = FunctionsEnumerable.Lazy(function);
                    return;
                }

                var distinctParameters = Parameter.GetDistinct(parameters);
                var argumentList = Argument.Function.Parameterless.Get(possibleArguments).ToList();
                var distinctRelations = Relation.GetDistinct(distinctParameters, argumentList);
                var relations = Relation.Get(parameters, distinctRelations);

                var valueArrays =
                    from arguments in relations.Select(x => x.Arguments.Cast<Argument.IEvaluable>().ToList()).AsCartesianProduct()
                    select arguments.Select(x => x.Evaluate()).ToArray();

                var functions =
                    from valueArray in valueArrays
                    select new Func<object>(() => function.DynamicInvoke(valueArray));

                Invocations = FunctionsEnumerable.Lazy(functions);
            }

            public IEnumerator<Func<object>> GetEnumerator() => Invocations.Value.GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
