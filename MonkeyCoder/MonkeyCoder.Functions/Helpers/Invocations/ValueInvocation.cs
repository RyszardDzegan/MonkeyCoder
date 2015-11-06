using System;
using System.Collections.Generic;
using System.Linq;

namespace MonkeyCoder.Functions.Helpers.Invocations
{
    /// <summary>
    /// Stores the information about the invocation.
    /// <see cref="Function"/> returns the <see cref="Value"/>.
    /// <see cref="Arguments"/> are always empty.
    /// <see cref="Value"/> contains an instance of a type that is not a function.
    /// </summary>
    internal class ValueInvocation : IInvocation
    {
        /// <summary>
        /// The function that returns the <see cref="Value"/>.
        /// </summary>
        public Func<object> Function { get; }

        /// <summary>
        /// Returns the <see cref="Value"/>.
        /// </summary>
        public object OriginalValue =>
            Value;

        /// <summary>
        /// Arguments of the <see cref="Function"/>.
        /// They are always empty.
        /// </summary>
        public IEnumerable<object> Arguments { get; }

        /// <summary>
        /// An instance of a type that is not a function.
        /// </summary>
        public object Value { get; }

        /// <summary>
        /// The main constructor.
        /// </summary>
        /// <param name="value">A <see cref="Value"/>.</param>
        public ValueInvocation(object value)
        {
            Function = new Func<object>(() => value);
            Arguments = Enumerable.Empty<object>();
            Value = value;
        }
    }
}
