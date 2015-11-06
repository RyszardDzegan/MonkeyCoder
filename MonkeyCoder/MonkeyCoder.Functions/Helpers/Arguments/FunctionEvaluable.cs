using System.Diagnostics;

namespace MonkeyCoder.Functions.Helpers.Arguments
{
    /// <summary>
    /// A <see cref="FunctionArgument"/> after processing that is able to
    /// implement the <see cref="IEvaluable"/>.
    /// </summary>
    /// <seealso cref="FunctionArgument"/>
    [DebuggerDisplay("{Evaluate()}")]
    internal class FunctionEvaluable : IEvaluable
    {
        /// <summary>
        /// Information about the function and its arguments.
        /// </summary>
        public IInvocation Invocation { get; }

        /// <summary>
        /// A constructor that takes all necessary
        /// information to invoke the function.
        /// </summary>
        /// <param name="invocation">An information about the function and its parameters.</param>
        internal FunctionEvaluable(IInvocation invocation)
        {
            Invocation = invocation;
        }

        /// <summary>
        /// Invokes the underlying function at runtime.
        /// </summary>
        /// <returns>A value returned by the function.</returns>
        public object Evaluate()
            => Invocation.Function();
    }
}
