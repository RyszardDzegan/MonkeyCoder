using MonkeyCoder.Functions.Helpers.Parameters;
using System;

namespace MonkeyCoder.Functions.Helpers.Arguments
{
    /// <summary>
    /// A subclass of <see cref="Argument"/> that manages functions
    /// that are provided as arguments.
    /// </summary>
    /// <seealso cref="FunctionEvaluable"/>
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
    }
}
