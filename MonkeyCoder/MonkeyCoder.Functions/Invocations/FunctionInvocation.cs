using System;
using System.Collections.Generic;
using System.Linq;

namespace MonkeyCoder.Functions.Invocations
{
    /// <summary>
    /// Stores the information about the invocation.
    /// The <see cref="Function"/> invokes the <see cref="Delegate"/> passing in the <see cref="Arguments"/> and returning its returned value.
    /// <see cref="Arguments"/> stores arguments that will be passed to the <see cref="Delegate"/>.
    /// The <see cref="Delegate"/> is an original function.
    /// </summary>
    public class FunctionInvocation : InvocationBase, IInvocation
    {
        protected override object FunctionBody() =>
            Delegate.DynamicInvoke(Arguments.Select(x => x.Function()).ToArray());

        /// <summary>
        /// The orginal function.
        /// </summary>
        public Delegate Delegate =>
            (Delegate)Value;
        
        /// <summary>
        /// A construtor that takes a delagate and its arguments.
        /// The <see cref="Function"/> will invoke the <paramref name="delegate"/> passing in the <paramref name="arguments"/> and will return its returned value.
        /// <see cref="Arguments"/> will store the <paramref name="arguments"/>.
        /// </summary>
        /// <param name="@delegate">A delegate that will be wrapped by a <see cref="Function"/>.</param>
        /// <param name="arguments">Delegate's arguments that will be stored in <see cref="Arguments"/>.</param>
        public FunctionInvocation(Delegate @delegate, IList<IInvocation> arguments)
            : base(@delegate, arguments)
        { }

        /// <summary>
        /// A visitor that can be used to inspect subclasses of <see cref="IInvocation"/>.
        /// </summary>
        public void Accept(IInvocationVisitor visitor) =>
            visitor.Visit(this);
    }
}
