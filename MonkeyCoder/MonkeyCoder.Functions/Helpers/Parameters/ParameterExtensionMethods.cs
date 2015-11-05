using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MonkeyCoder.Functions.Helpers.Parameters
{
    /// <summary>
    /// Extension methods aimed to ease the use of <seealso cref="Parameter"/> class.
    /// </summary>
    internal static class ParameterExtensionMethods
    {
        /// <summary>
        /// Filters provided <paramref name="parameters"/>
        /// to get only those with unique types.
        /// </summary>
        /// <param name="parameters">A list of original function parameters.</param>
        /// <returns>A filtered parameters.</returns>
        /// <example>
        /// Given a function foo(int, string, int)
        /// and providing its parameters to this method
        /// a parameter Invocations of int and string will be returned.
        /// The second int parameter will be filtered out.
        /// In addition, the string parameter will have
        /// <see cref="IsNullAssignable"/> set to true,
        /// whereas the int parameter will have
        /// <see cref="IsNullAssignable"/> set to false.
        /// </example>
        public static IEnumerable<Parameter> GetDistinct(this IEnumerable<ParameterInfo> parameters) =>
            parameters
                .Select(x => x.ParameterType)
                .Distinct(TypeFullNameEqualityComparer.Instance)
                .Select(x =>
                {
                    var isNullAssignable = !x.IsValueType || x.FullName.StartsWith("System.Nullable`1");
                    return new Parameter(type: x, isNullAssignable: isNullAssignable);
                });
    }
}
