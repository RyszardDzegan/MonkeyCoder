using MonkeyCoder.Functions.Helpers.Parameters;
using MonkeyCoder.Functions.Internals;
using MonkeyCoder.Functions.Invocations;
using System;
using System.Collections.Generic;

namespace MonkeyCoder.Functions.Helpers.Arguments
{
    /// <summary>
    /// A subclass of <see cref="Argument"/> that manages functions
    /// that are provided as arguments.
    /// </summary>
    internal class FunctionArgument : Argument
    {
        /// <summary>
        /// A constructor that takes a function and its returned type as parameters.
        /// </summary>
        /// <param name="value">A function.</param>
        /// <param name="type">A function's returned type.</param>
        internal FunctionArgument(object value, Type type)
            : base(value, type)
        { }

        /// <summary>
        /// Determines whether this function's returned type is compatible with given <paramref name="parameter"/>.
        /// </summary>
        /// <param name="parameter">A parameter which assignability will be determined.</param>
        /// <returns>True if this function's returned type is compatible with given <paramref name="parameter"/>. Otherwise false.</returns>
        public override bool IsAssignableTo(Parameter parameter) =>
            parameter.Type.IsAssignableFrom(Type);

        /// <summary>
        /// Converts arguments into function invocations.
        /// There is only one invocation for <see cref="ValueArgument"/> and <see cref="ParameterlessArgument"/>.
        /// There are multiple invocations for <see cref="FunctionArgument"/>.
        /// </summary>
        /// <param name="possibleArguments">All possible argument candidates for the primary function.</param>
        /// <param name="currentStackSize">The current level of the call stack.</param>
        /// <returns>An enumerable of invocations.</returns>
        public override IEnumerable<IInvocation> ToInvocations(IReadOnlyCollection<object> possibleArguments, int currentStackSize) =>
            new Expanding((Delegate)Value, possibleArguments, currentStackSize - 1);
    }
}
