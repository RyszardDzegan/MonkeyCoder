using System.Collections.Generic;

namespace MonkeyCoder.Core.Math
{
    public static class Combinatorics
    {
        /// <summary>
        /// Takes a collection of sets and creates a cartesian product.
        /// </summary>
        /// <typeparam name="T">Arbitrary type.</typeparam>
        /// <example>
        /// A pair {a,b,c} {A,B} will produce: {a,A} {a,B} {b,A} {b,B} {c,A} {c,B}.
        /// </example>
        public static IEnumerable<IList<T>> AsCartesianProduct<T>(this IEnumerable<IEnumerable<T>> items)
        {
            return new CartesianProduct<T>(items);
        }

        public static IEnumerable<IList<T>> AsVariationsWithRepetitions<T>(this IEnumerable<T> items, int k)
        {
            return new VariationsWithRepetitions<T>(items, k);
        }

        public static IEnumerable<IList<T>> AsCombinations<T>(this IEnumerable<T> items, int k)
        {
            return new Combinations<T>(items, k);
        }
    }
}
