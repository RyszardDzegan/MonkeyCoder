using System;
using System.Collections.Generic;

namespace MonkeyCoder.Functions.Helpers.Parameters
{
    /// <summary>
    /// Implements <see cref="IEqualityComparer{T}"/> of <see cref="Type"/>.
    /// Aimed to compare <see cref="System.Reflection.ParameterInfo"/> classes equality basing on their types.
    /// After a type based comparison, we can use a dictinct method on an enumerable of
    /// <see cref="Parameter"/> instances to obtain only unique values for further processing.
    /// </summary>
    /// <seealso cref="ParameterExtensionMethods.GetDistinct(IEnumerable{System.Reflection.ParameterInfo})"/>
    /// <remarks>
    /// This class is a singleton.
    /// </remarks>
    internal class TypeFullNameEqualityComparer : IEqualityComparer<Type>
    {
        /// <summary>
        /// A lazy, singleton instance of <seealso cref="TypeFullNameEqualityComparer"/>.
        /// </summary>
        private static Lazy<TypeFullNameEqualityComparer> _instance =
            new Lazy<TypeFullNameEqualityComparer>(() => new TypeFullNameEqualityComparer());

        /// <summary>
        /// Returns a singleton instance of this type.
        /// </summary>
        public static TypeFullNameEqualityComparer Instance => _instance.Value;

        /// <summary>
        /// Compares to types by their <seealso cref="Type.FullName"/>.
        /// </summary>
        /// <param name="x">First type.</param>
        /// <param name="y">Second type.</param>
        /// <returns>True if types <seealso cref="Type.FullName"/> are equal. Otherwise, false.</returns>
        public bool Equals(Type x, Type y) => x.FullName == y.FullName;

        /// <summary>
        /// Returns the hash code of <seealso cref="Type.FullName"/>.
        /// </summary>
        /// <param name="obj">The type from which a hash code will be obtained.</param>
        /// <returns>The hash code of <seealso cref="Type.FullName"/>.</returns>
        public int GetHashCode(Type obj) => obj.FullName.GetHashCode();
    }
}
