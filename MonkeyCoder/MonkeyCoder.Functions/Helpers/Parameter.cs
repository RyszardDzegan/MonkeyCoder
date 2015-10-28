using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace MonkeyCoder.Functions.Helpers
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
        public Type Type { get; private set; }

        /// <summary>
        /// Returns true if parameter accepts null arguments, otherwise false.
        /// </summary>
        public bool IsNullAssignable { get; private set; }

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
        
        private Parameter() { }

        /// <summary>
        /// Filters provided <paramref name="parameters"/>
        /// to get only those with distinct types.
        /// </summary>
        /// <param name="parameters">A list of original function parameters.</param>
        /// <returns>A filtered parameters.</returns>
        /// <example>
        /// Given a function foo(int, string, int)
        /// and providing its parameters to this method
        /// a parameter enumerable of int and string will be returned.
        /// The second int parameter will be filtered out.
        /// In addition, the string parameter will have
        /// <see cref="IsNullAssignable"/> set to true,
        /// whereas the int parameter will have
        /// <see cref="IsNullAssignable"/> set to false.
        /// </example>
        public static IEnumerable<Parameter> GetDistinct(IEnumerable<ParameterInfo> parameters)
        {
            return parameters
                .Select(x => x.ParameterType)
                .Distinct(TypeFullNameEqualityComparer.Instance)
                .Select(x =>
                {
                    var isNullAssignable = !x.IsValueType || x.FullName.StartsWith("System.Nullable`1");
                    return new Parameter { Type = x, IsNullAssignable = isNullAssignable };
                });
        }
    }
}
