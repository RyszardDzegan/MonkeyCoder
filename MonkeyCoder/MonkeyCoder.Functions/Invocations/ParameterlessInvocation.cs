using System;
using System.Collections.Generic;
using System.Linq;

namespace MonkeyCoder.Functions.Invocations
{
    /// <summary>
    /// Stores the information about the invocation.
    /// The <see cref="Function"/> invokes the <see cref="Delegate"/> and returning its returned value.
    /// <see cref="Arguments"/> are empty.
    /// The <see cref="Delegate"/> is an original function.
    /// </summary>
    public class ParameterlessInvocation : InvocationBase, IInvocation
    {
        protected override object FunctionBody() =>
            Delegate.DynamicInvoke();

        /// <summary>
        /// The orginal function.
        /// </summary>
        public Delegate Delegate =>
            (Delegate)Value;

        /// <summary>
        /// A construtor that takes a parameterless delagate as its argument.
        /// The <see cref="Function"/> will just invoke the <paramref name="delegate"/> and return its returned value.
        /// <see cref="Arguments"/> will be empty.
        /// </summary>
        /// <param name="@delegate">A delegate that will be wrapped by a <see cref="Function"/>.</param>
        public ParameterlessInvocation(Delegate @delegate)
            : base(@delegate)
        { }

        /// <summary>
        /// A visitor that can be used to inspect subclasses of <see cref="IInvocation"/>.
        /// </summary>
        public void Accept(IInvocationVisitor visitor) =>
            visitor.Visit(this);
    }
}
