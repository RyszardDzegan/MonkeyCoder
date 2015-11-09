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
    /// An advanced function invocation manager.
    /// In contrast to <see cref="Basic"/> it allows a function argument candidate
    /// to be considered together with its returned value.
    /// Unlike with <see cref="Parameterless"/> it allows a function argument candidate
    /// to be considered also when it takes its own input parameters.
    /// That approach allows a recurrency to occur and to control a stask size,
    /// a special argument is available in the main constructor.
    /// </summary>
    internal partial class Expanding : IEnumerable<IInvocation>
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
        /// <param name="stackSize">Determines to which level a recurrency is allowed. Default is 0 which means that no inner function will be invoked.</param>
        /// <exception cref="ArgumentNullException">When <paramref name="function"/> is null.</exception>
        /// <exception cref="ArgumentNullException">When <paramref name="possibleArguments"/> are null.</exception>
        public Expanding(Delegate function, IReadOnlyCollection<object> possibleArguments, int stackSize = 0)
        {
            if (function == null)
                throw new ArgumentNullException(nameof(function), "FunctionArgument cannot be null.");

            if (possibleArguments == null)
                throw new ArgumentNullException(nameof(possibleArguments), "Possible arguments cannot be null.");

            // If stack size is less than zero
            // then even the primary function shouldn't be invoked.
            if (stackSize < 0)
            {
                Invocations = InvocationsEnumerable.Lazy();
                return;
            }

            var parameters = function.Method.GetParameters();

            // When the primary function doesn't have any parameters
            // Then it can be invoked only once.
            if (!parameters.Any())
            {
                Invocations = InvocationsEnumerable.Lazy(function);
                return;
            }

            var distinctParameters = parameters.GetDistinct();
            var argumentList = (stackSize > 0 ? possibleArguments.ToProFunctionArguments() : possibleArguments.ToValueArguments()).ToList();
            var distinctRelations = distinctParameters.Relate(argumentList);
            var distinctInvocationsRelations = distinctRelations.ToInvocationsRelations(possibleArguments, stackSize);
            var relations = parameters.LeftJoin(distinctInvocationsRelations);
            var argumentsEnumerable = relations.ProducePossibleArgumentSets();

            Invocations = InvocationsEnumerable.Lazy(function, argumentsEnumerable);
        }

        public IEnumerator<IInvocation> GetEnumerator() => Invocations.Value.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
