using MonkeyCoder.Functions.Invocations;
using System;
using System.Collections.Generic;

namespace MonkeyCoder.Functions.Helpers.Arguments
{
    /// <summary>
    /// A subclass of <see cref="Argument"/> that
    /// manages parameterless functions
    /// that are provided as arguments.
    /// </summary>
    internal class ParameterlessArgument : FunctionArgument
    {
        /// <summary>
        /// A constructor that takes a parameterless function and its returned type as parameters.
        /// </summary>
        /// <param name="value">A parameterless function.</param>
        /// <param name="type">A function's returned type.</param>
        internal ParameterlessArgument(object value, Type type)
            : base(value, type)
        { }

        /// <summary>
        /// Converts arguments into function invocations.
        /// There is only one invocation for <see cref="BasicArgument"/> and <see cref="ParameterlessArgument"/>.
        /// There are multiple invocations for <see cref="FunctionArgument"/>.
        /// </summary>
        /// <param name="possibleArguments">All possible argument candidates for the primary function.</param>
        /// <param name="currentStackSize">The current level of the call stack.</param>
        /// <returns>An enumerable of invocations.</returns>
        public override IEnumerable<IInvocation> ToInvocations(IReadOnlyCollection<object> possibleArguments, int currentStackSize)
        {
            yield return new FunctionInvocation((Delegate)Value);
        }
    }
}
