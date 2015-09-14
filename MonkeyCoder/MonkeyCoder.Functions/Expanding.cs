using MonkeyCoder.Functions.Helpers;
using MonkeyCoder.Math;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MonkeyCoder.Functions
{
    internal partial class Expanding : IEnumerable<Func<object>>
    {
        private Lazy<FunctionsEnumerable> Invocations { get; }

        public Expanding(Delegate function, IReadOnlyCollection<object> possibleArguments, int stackSize = 0)
        {
            if (function == null)
                throw new ArgumentNullException(nameof(function), "Function cannot be null.");

            if (possibleArguments == null)
                throw new ArgumentNullException(nameof(possibleArguments), "Possible arguments cannot be null.");

            if (stackSize < 0)
            {
                Invocations = FunctionsEnumerable.Lazy();
                return;
            }

            var parameters = function.Method.GetParameters();

            if (!parameters.Any())
            {
                Invocations = FunctionsEnumerable.Lazy(function);
                return;
            }

            var distinctParameters = Parameter.GetDistinct(parameters);
            var argumentList = (stackSize > 0 ? Argument.Function.Get(possibleArguments) : Argument.Basic.Get(possibleArguments)).ToList();
            var distinctRelations = Relation.GetDistinct(distinctParameters, argumentList);

            var spawnedArguments =
                from argument in argumentList
                let evaluableArgument = argument as Argument.IEvaluable
                let spawnedEvaluables =
                    evaluableArgument != null ?
                    Enumerable.Repeat(evaluableArgument, 1) :
                    Argument.Function.Evaluable.Get(new Expanding((Delegate)argument.Value, possibleArguments, stackSize - 1))
                from spawnedEvaluable in spawnedEvaluables
                select new { Original = argument, Evaluable = spawnedEvaluable };

            var updatedDistinctRelations =
                from relation in distinctRelations
                let updatedArguments =
                     from argument in relation.Arguments
                     join spawnedArgument in spawnedArguments on argument equals spawnedArgument.Original into jointSpawnedArguments
                     let evaluableArguments =
                        jointSpawnedArguments == null ?
                        Enumerable.Repeat((Argument.IEvaluable)argument, 1) :
                        from jointSpawnedArgument in jointSpawnedArguments
                        select jointSpawnedArgument.Evaluable
                     from evaluableArgument in evaluableArguments
                     select evaluableArgument
                select new Relation.Evaluable(relation.Parameter, updatedArguments);

            var relations = Relation.Evaluable.Get(parameters, updatedDistinctRelations);

            var valueArrays =
                from evaluables in relations.Select(x => x.Evaluables.ToList()).AsCartesianProduct()
                select evaluables.Select(x => x.Evaluate()).ToArray();

            var functions =
                from valueArray in valueArrays
                select new Func<object>(() => function.DynamicInvoke(valueArray));

            Invocations = FunctionsEnumerable.Lazy(functions);
        }

        public IEnumerator<Func<object>> GetEnumerator() => Invocations.Value.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
