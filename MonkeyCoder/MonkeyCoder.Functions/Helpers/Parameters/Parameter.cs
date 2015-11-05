using MonkeyCoder.Functions.Helpers.Arguments;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MonkeyCoder.Functions.Helpers.Parameters
{
    /// <summary>
    /// A class that stores information about function parameter.
    /// </summary>
    [DebuggerDisplay("{TypeName}")]
    internal class Parameter
    {
        /// <summary>
        /// An argument type accepted by the parameter.
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// Returns true if parameter accepts null arguments, otherwise false.
        /// </summary>
        public bool IsNullAssignable { get; }

        /// <summary>
        /// Produces a legible type name for debugging purposes.
        /// </summary>
        private string TypeName =>
            !Type.GetGenericArguments().Any() ? Type.Name : Type.Name.Split('`').First() + "<" + string.Join(", ", Type.GetGenericArguments().Select(x => x.Name)) + ">";

        /// <summary>
        /// Filters provided <paramref name="arguments"/>
        /// to get only those assignable to the parameter.
        /// </summary>
        /// <param name="arguments">Arguments to filter against the parameter.</param>
        /// <returns>An enumerable of arguments assignable to the parameter.</returns>
        public IEnumerable<Argument> GetAssignableArguments(IEnumerable<Argument> arguments) =>
            from argument in arguments
            where argument.IsAssignableTo(this)
            select argument;
        
        /// <summary>
        /// A constructor that takes values for <see cref="Type"/> and <see cref="IsNullAssignable"/>.
        /// </summary>
        /// <param name="type">A value for <see cref="Type"/>.</param>
        /// <param name="isNullAssignable">A value for <see cref="IsNullAssignable"/>.</param>
        internal Parameter(Type type, bool isNullAssignable)
        {
            Type = type;
            IsNullAssignable = isNullAssignable;
        }
    }
}
