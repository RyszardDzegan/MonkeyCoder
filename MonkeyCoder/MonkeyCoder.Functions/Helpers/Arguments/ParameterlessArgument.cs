using System;

namespace MonkeyCoder.Functions.Helpers.Arguments
{
    /// <summary>
    /// A subclass of <see cref="Argument"/> that
    /// manages parameterless functions
    /// that are provided as arguments.
    /// </summary>
    internal class ParameterlessArgument : FunctionArgument, IEvaluable
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
        /// Invokes stored parameterless function and returns its value.
        /// </summary>
        /// <returns>FunctionArgument's returned value.</returns>
        public object Evaluate() =>
            ((Delegate)Value).DynamicInvoke();
    }
}
