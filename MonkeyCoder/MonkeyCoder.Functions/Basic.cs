using MonkeyCoder.Functions.Helpers;
using MonkeyCoder.Math;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MonkeyCoder.Functions
{
    /// <summary>
    /// The basic function manager.
    /// It addresses one function and a collection of all possible arguments
    /// that the function could take as its input.
    /// The basic function manager matches
    /// the all possible arguments against the function parameters,
    /// so that each function parameter has a list of
    /// its possible arguments that have a matching type.
    /// Then an enumerable of function invocations is created in a way,
    /// that the function is going to be invoked with all possible combinations of provided arguments.
    /// </summary>
    internal partial class Basic : IEnumerable<Func<object>>
    {
        /// <summary>
        /// All possible function invocations.
        /// </summary>
        private Lazy<FunctionsEnumerable> Invocations { get; }

        /// <summary>
        /// The constructor that takes a function and a collection of argument candidates.
        /// </summary>
        /// <param name="function">The function for which an invocation list is to be created.</param>
        /// <param name="possibleArguments">The candidates for arguments for the function.</param>
        public Basic(Delegate function, IReadOnlyCollection<object> possibleArguments)
        {
            if (function == null)
                throw new ArgumentNullException(nameof(function), "Function cannot be null.");

            if (possibleArguments == null)
                throw new ArgumentNullException(nameof(possibleArguments), "Possible arguments cannot be null.");

            // Get function parameters
            var parameters = function.Method.GetParameters();

            // If function doesn't have any parameters
            // prepare an ivocation list consisting of
            // single invocation of the function and return.
            if (!parameters.Any())
            {
                Invocations = FunctionsEnumerable.Lazy(function);
                return;
            }

            var distinctParameters = Parameter.GetDistinct(parameters);
            var argumentList = Argument.Basic.Get(possibleArguments).ToList();
            var distinctRelations = Relation.GetDistinct(distinctParameters, argumentList);
            var relations = Relation.Get(parameters, distinctRelations);

            var valueArrays =
                from arguments in relations.Select(x => x.Arguments.ToList()).AsCartesianProduct()
                select arguments.Select(x => x.Value).ToArray();

            var functions =
                from valueArray in valueArrays
                select new Func<object>(() => function.DynamicInvoke(valueArray));

            Invocations = FunctionsEnumerable.Lazy(functions);
        }

        public IEnumerator<Func<object>> GetEnumerator() => Invocations.Value.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
