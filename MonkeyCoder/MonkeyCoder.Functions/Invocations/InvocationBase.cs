using System;
using System.Collections.Generic;
using System.Linq;

namespace MonkeyCoder.Functions.Invocations
{
    /// <summary>
    /// Stores the information about the invocation.
    /// <see cref="Function"/> returns the <see cref="Value"/>.
    /// <see cref="Arguments"/> are always empty.
    /// <see cref="Value"/> contains an instance of a type that is not a function.
    /// </summary>
    public abstract class InvocationBase
    {
        /// <summary>
        /// An instance of a type that is not a function.
        /// </summary>
        public object Value { get; }

        /// <summary>
        /// The function that returns the <see cref="Value"/>.
        /// </summary>
        public Func<object> Function { get; }
        
        /// <summary>
        /// Arguments of the <see cref="Function"/>.
        /// They are always empty.
        /// </summary>
        public IEnumerable<IInvocation> Arguments { get; }

        /// <summary>
        /// Determines the body that will be used in a <see cref="Function"/>.
        /// </summary>
        protected abstract object FunctionBody();

        /// <summary>
        /// A constructor that takes a <see cref="Value"/> and no arguments.
        /// </summary>
        /// <param name="value">A <see cref="Value"/>.</param>
        public InvocationBase(object value)
            : this(value, Enumerable.Empty<IInvocation>())
        { }

        /// <summary>
        /// The main constructor.
        /// </summary>
        /// <param name="value">A <see cref="Value"/>.</param>
        /// <param name="arguments"><see cref="Arguments"/>.</param>
        public InvocationBase(object value, IEnumerable<IInvocation> arguments)
        {
            Value = value;
            Arguments = arguments;
            Function = new Func<object>(FunctionBody);
        }
    }
}
