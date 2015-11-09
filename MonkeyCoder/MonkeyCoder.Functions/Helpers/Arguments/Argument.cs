using MonkeyCoder.Functions.Helpers.Parameters;
using MonkeyCoder.Functions.Invocations;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MonkeyCoder.Functions.Helpers.Arguments
{
    /// <summary>
    /// A class that stores information about an argument.
    /// </summary>
    [DebuggerDisplay("{Value != null ? Value : \"null\"} : {GetType().Name}")]
    internal abstract partial class Argument
    {
        /// <summary>
        /// Argument's value.
        /// Can be null or a function.
        /// In case of function, this property stores
        /// the function and its real value is taken at runtime.
        /// <seealso cref="IEvaluable"/>.
        /// </summary>
        public object Value { get; private set; }

        /// <summary>
        /// Argument's type.
        /// If <see cref="Value"/> is null then <see cref="Type"/> is null as well.
        /// </summary>
        public Type Type { get; private set; }

        protected Argument(object value, Type type)
        {
            Value = value;
            Type = type;
        }

        /// <summary>
        /// Checks whether current instance of <see cref="Argument"/> is assignable to given <paramref name="parameter"/>.
        /// A concrete subclass decides, how the assignability is defined.
        /// </summary>
        /// <param name="parameter">A parameter to which the assignability is being checked.</param>
        /// <returns>True, if current instance of <see cref="Argument"/> is assignable to given <paramref name="parameter"/>. Otherwise false.</returns>
        public abstract bool IsAssignableTo(Parameter parameter);

        /// <summary>
        /// Converts arguments into function invocations.
        /// There is only one invocation for <see cref="ValueArgument"/> and <see cref="ParameterlessArgument"/>.
        /// There are multiple invocations for <see cref="FunctionArgument"/>.
        /// </summary>
        /// <param name="possibleArguments">All possible argument candidates for the primary function.</param>
        /// <param name="currentStackSize">The current level of the call stack.</param>
        /// <returns>An enumerable of invocations.</returns>
        public abstract IEnumerable<IInvocation> ToInvocations(IReadOnlyCollection<object> possibleArguments, int currentStackSize);
    }
}
