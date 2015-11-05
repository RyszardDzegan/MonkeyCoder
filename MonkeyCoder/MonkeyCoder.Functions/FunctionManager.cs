using MonkeyCoder.Functions.Helpers.Invocations;
using MonkeyCoder.Functions.Internals;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MonkeyCoder.Functions
{
    /// <summary>
    /// The function manager takes a collection of items to its constructor
    /// together with a maximum call stack threshold.
    /// Then it produces all possible function invocations
    /// using the <see cref="Expanding"/> class underneath.
    /// </summary>
    internal class FunctionManager : IEnumerable<IInvocation>
    {
        /// <summary>
        /// All possible function invocations.
        /// </summary>
        private Lazy<InvocationsEnumerable> Invocations { get; }

        /// <summary>
        /// A constructor that takes a collection of items.
        /// Each item can be a simple value or a function.
        /// If it is a value, then it will be wrapped into a function that will just return that value.
        /// In case of a function or action, all items will be treated as potential arguments for that function
        /// and steps will be undertaken to invoke that function with all possible arguments combinations.
        /// To prevent an infinitive call stack, a stack size argument is provided.
        /// Its default value is 0 which means that only first level functions will be invoked.
        /// </summary>
        /// <param name="items">A collection of items that can be simple values, functions or actions.</param>
        /// <param name="stackSize">Prevents an infinitive call stack.</param>
        public FunctionManager(IReadOnlyCollection<object> items, int stackSize = 0)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items), "Arguments cannot be null.");

            var invocationEnumerables =
                from argument in items
                let function = argument as Delegate
                let results =
                    function != null ?
                    new InvocationsEnumerable(new Expanding(function, items, stackSize)) :
                    new InvocationsEnumerable(argument)
                from result in results
                select result;

            Invocations = InvocationsEnumerable.Lazy(invocationEnumerables);
        }

        public IEnumerator<IInvocation> GetEnumerator() => Invocations.Value.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
