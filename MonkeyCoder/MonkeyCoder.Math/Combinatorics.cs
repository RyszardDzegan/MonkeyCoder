using System.Collections.Generic;

namespace MonkeyCoder.Math
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
        public static IEnumerable<IList<T>> AsCartesianProduct<T>(this IEnumerable<IReadOnlyCollection<T>> items)
        {
            return new CartesianProduct<T>(items);
        }

        /// <summary>
        /// Takes a collection of <see cref="Items"/> and produces all <see cref="K"/> variations with repetitions of them.
        /// </summary>
        /// <typeparam name="T">Arbitrary type.</typeparam>
        /// <example>
        /// Items {a,b,c} with k = 2 will produce {a,a}, {a,b}, {a,c}, {b,a}, {b,b}, {b,c}, {c,a}, {c,b}. {c,c}
        /// </example>
        public static IEnumerable<IList<T>> AsVariationsWithRepetitions<T>(this IEnumerable<T> items, int k)
        {
            return new VariationsWithRepetitions<T>(items, k);
        }

        /// <summary>
        /// Takes a collection of <see cref="Items"/> and produces all <see cref="K"/> variations without repetitions of them.
        /// </summary>
        /// <typeparam name="T">Arbitrary type.</typeparam>
        /// <example>
        /// Items {a,b,c} with k = 2 will produce {a,b}, {a,c}, {b,a}, {b,c}, {c,a}, {c,b}
        /// </example>
        public static IEnumerable<IList<T>> AsVariationsWithoutRepetitions<T>(this IEnumerable<T> items, int k)
        {
            return new VariationsWithoutRepetitions<T>(items, k);
        }

        /// <summary>
        /// Takes a collection of <see cref="Items"/> and produces all <see cref="K"/> combinations of them.
        /// </summary>
        /// <typeparam name="T">Arbitrary type.</typeparam>
        /// <example>
        /// Items {a,b,c} with k = 2 will produce {a,b}, {a,c}, {b,c}
        /// </example>
        public static IEnumerable<IList<T>> AsCombinations<T>(this IEnumerable<T> items, int k)
        {
            return new Combinations<T>(items, k);
        }

        /// <summary>
        /// Takes a collection of <see cref="Items"/> and produces all possible subsets of them.
        /// </summary>
        /// <typeparam name="T">Arbitrary type.</typeparam>
        /// <example>
        /// Items {a,b,c} will produce {}, {a}, {b}, {a,b}, {a,c}, {b,c}, {a,b,c}
        /// </example>
        public static IEnumerable<IList<T>> AsPowerSet<T>(this IEnumerable<T> items)
        {
            return new PowerSet<T>(items);
        }
    }
}
