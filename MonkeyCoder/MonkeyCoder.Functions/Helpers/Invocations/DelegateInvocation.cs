using System;
using System.Collections.Generic;
using System.Linq;

namespace MonkeyCoder.Functions.Helpers.Invocations
{
    /// <summary>
    /// Stores the information about the invocation.
    /// The <see cref="Function"/> invokes the <see cref="Delegate"/> passing in the <see cref="Arguments"/> and returning its returned value.
    /// <see cref="Arguments"/> stores arguments that will be passed to the <see cref="Delegate"/>.
    /// The <see cref="Delegate"/> is an original function.
    /// </summary>
    internal class DelegateInvocation : IInvocation
    {
        /// <summary>
        /// Invokes the <see cref="Delegate"/> passing in the <see cref="Arguments"/> and returning its returned value.
        /// </summary>
        public Func<object> Function { get; }

        /// <summary>
        /// Stores arguments that will be passed to the <see cref="Delegate"/>.
        /// </summary>
        public IEnumerable<object> Arguments { get; }

        /// <summary>
        /// The orginal function.
        /// </summary>
        public Delegate Delegate { get; }

        /// <summary>
        /// A construtor that takes a parameterless delagate as its argument.
        /// The <see cref="Function"/> will just invoke the <paramref name="delegate"/> and return its returned value.
        /// <see cref="Arguments"/> will be empty.
        /// </summary>
        /// <param name="@delegate">A delegate that will be wrapped by a <see cref="Function"/>.</param>
        public DelegateInvocation(Delegate @delegate)
        {
            Function = new Func<object>(() => @delegate.DynamicInvoke());
            Arguments = Enumerable.Empty<object>();
            Delegate = @delegate;
        }

        /// <summary>
        /// A construtor that takes a delagate and its arguments.
        /// The <see cref="Function"/> will invoke the <paramref name="delegate"/> passing in the <paramref name="arguments"/> and will return its returned value.
        /// <see cref="Arguments"/> will store the <paramref name="arguments"/>.
        /// </summary>
        /// <param name="@delegate">A delegate that will be wrapped by a <see cref="Function"/>.</param>
        /// <param name="arguments">Delegate's arguments that will be stored in <see cref="Arguments"/>.</param>
        public DelegateInvocation(Delegate @delegate, IEnumerable<object> arguments)
        {
            Function = new Func<object>(() => @delegate.DynamicInvoke(arguments.ToArray()));
            Arguments = arguments;
            Delegate = @delegate;
        }
    }
}
