using System;
using System.Collections.Generic;

namespace MonkeyCoder.Functions.Invocations
{
    /// <summary>
    /// An interface that stores informations regarding
    /// all details that are necessary to invoke the function.
    /// Having that information you can recreate a call stack.
    /// </summary>
    public interface IInvocation
    {
        /// <summary>
        /// A function that wraps a simple value or a delegate.
        /// </summary>
        Func<object> Function { get; }

        /// <summary>
        /// An original value or delegate that was wrapped with a <see cref="Function"/>.
        /// </summary>
        object OriginalValue { get; }

        /// <summary>
        /// Potential arguments used by a delegate (<see cref="OriginalValue"/>) wrapped by a <seealso cref="Function"/>.
        /// </summary>
        IEnumerable<IInvocation> Arguments { get; }

        /// <summary>
        /// A part of the visitor pattern.
        /// <see cref="IInvocationVisitor"/>
        /// </summary>
        /// <param name="visitor">A visitor that allow to inspect a tree of invocations.</param>
        void Accept(IInvocationVisitor visitor);
    }
}
