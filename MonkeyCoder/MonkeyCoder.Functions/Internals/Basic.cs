using MonkeyCoder.Functions.Helpers.Arguments;
using MonkeyCoder.Functions.Helpers.Parameters;
using MonkeyCoder.Functions.Helpers.Relations;
using MonkeyCoder.Functions.Invocations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MonkeyCoder.Functions.Internals
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
    internal partial class Basic : IEnumerable<IInvocation>
    {
        /// <summary>
        /// All possible function invocations.
        /// </summary>
        private Lazy<InvocationsEnumerable> Invocations { get; }

        /// <summary>
        /// The constructor that takes a function and a collection of argument candidates.
        /// </summary>
        /// <param name="function">The function for which an invocation list is to be created.</param>
        /// <param name="possibleArguments">The candidates for arguments for the function.</param>
        /// <exception cref="ArgumentNullException">When <paramref name="function"/> is null.</exception>
        /// <exception cref="ArgumentNullException">When <paramref name="possibleArguments"/> are null.</exception>
        public Basic(Delegate function, IReadOnlyCollection<object> possibleArguments)
        {
            if (function == null)
                throw new ArgumentNullException(nameof(function), "FunctionArgument cannot be null.");

            if (possibleArguments == null)
                throw new ArgumentNullException(nameof(possibleArguments), "Possible arguments cannot be null.");
            
            var parameters = function.Method.GetParameters();

            // If function doesn't have any parameters
            // prepare an ivocation list consisting of
            // single invocation of the function and return.
            if (!parameters.Any())
            {
                Invocations = InvocationsEnumerable.Lazy(function);
                return;
            }

            var distinctParameters = parameters.GetDistinct();
            var argumentList = possibleArguments.ToBasicArguments().ToList();
            var distinctRelations = distinctParameters.Relate(argumentList);
            var distinctInvocationsRelations = distinctRelations.ToInvocationsRelations(possibleArguments, 0);
            var relations = parameters.LeftJoin(distinctInvocationsRelations);
            var argumentsEnumerable = relations.ProducePossibleArgumentSets();

            Invocations = InvocationsEnumerable.Lazy(function, argumentsEnumerable);
        }

        public IEnumerator<IInvocation> GetEnumerator() => Invocations.Value.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
