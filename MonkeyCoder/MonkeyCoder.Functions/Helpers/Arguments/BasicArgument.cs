using MonkeyCoder.Functions.Helpers.Parameters;
using System;

namespace MonkeyCoder.Functions.Helpers.Arguments
{
    /// <summary>
    /// A subclass of <see cref="Argument"/> that manages
    /// simple objects, values and nulls.
    /// </summary>
    internal class BasicArgument : Argument, IEvaluable
    {
        /// <summary>
        /// True if argument is null.
        /// Otherwise false.
        /// </summary>
        public bool IsNull => Type == null;

        /// <summary>
        /// A constructor that takes an argument's value.
        /// It determines a type automaticaly.
        /// If <paramref name="value"/> is null  the type will be null too.
        /// </summary>
        /// <param name="value">A value of an argument.</param>
        public BasicArgument(object value)
            : this(value, value?.GetType())
        { }

        /// <summary>
        /// A constructor that takes an argument's value and its type.
        /// </summary>
        /// <param name="value">A value of an argument.</param>
        /// /// <param name="type">A type of an argument.</param>
        internal BasicArgument(object value, Type type)
            : base(value, type)
        { }

        /// <summary>
        /// Determines whether this argument can be assigned to given <paramref name="parameter"/>.
        /// It can be assigned if their types are compatible or
        /// when argument value is null and parameter is null assignable.
        /// </summary>
        /// <param name="parameter">A parameter against which the assignability will be determined.</param>
        /// <returns>True if this argument can be assigned to given <paramref name="parameter"/>. Otherwise false.</returns>
        public override bool IsAssignableTo(Parameter parameter) =>
            IsNull ? parameter.IsNullAssignable : parameter.Type.IsAssignableFrom(Type);

        /// <summary>
        /// Returns the original argument's value.
        /// </summary>
        /// <returns>Argument's value that was supplied with a constructor.</returns>
        public object Evaluate() =>
            Value;
    }
}
