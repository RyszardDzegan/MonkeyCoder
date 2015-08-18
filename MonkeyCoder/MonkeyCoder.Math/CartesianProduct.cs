using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MonkeyCoder.Math
{
    /// <summary>
    /// Takes a collection of sets and creates a cartesian product.
    /// </summary>
    /// <typeparam name="T">Arbitrary type.</typeparam>
    /// <example>
    /// A pair {a,b,c} {A,B} will produce {a,A} {a,B} {b,A} {b,B} {c,A} {c,B}.
    /// </example>
    internal class CartesianProduct<T> : IEnumerable<IList<T>>
    {
        public IEnumerable<IEnumerator<T>> Items { get; }

        /// <summary>
        /// Constructor that takes items that will be processed.
        /// </summary>
        /// <param name="items">Items that will be processed.</param>
        /// <exception cref="ArgumentNullException">When <paramref name="items"/> is null.</exception>
        /// <exception cref="ArgumentException">When any item in <paramref name="items"/> is null.</exception>
        public CartesianProduct(IEnumerable<IEnumerable<T>> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));
            
            if (items.Any(x => x == null))
            {
                var nullItems = items.Select((x, i) => new { Item = x, Index = i })
                                     .Where(x => x.Item == null)
                                     .Select(x => x.Index);

                throw new ArgumentException($"Item cannot be null. Nulls found at positions: {string.Join(", ", nullItems)}", nameof(items));
            }

            Items = items.Select(x => x.GetEnumerator());
        }

        private bool MoveNext(IEnumerable<IEnumerator<T>> items)
        {
            foreach (var x in items.Reverse())
            {
                if (x.MoveNext())
                    return true;

                x.Reset();
                x.MoveNext();
            }

            return false;
        }

        public IEnumerator<IList<T>> GetEnumerator()
        {
            // Break, if there is no item at all
            if (!Items.Any())
                yield break;

            // Break, if any of item is empty collection
            var items = Items.ToArray();
            foreach (var x in items)
                if (!x.MoveNext())
                    yield break;

            // Load the first result that contains first items of each set
            var resultQuery = from x in items
                              select x.Current;

            // Return the first result and generate next results
            do yield return resultQuery.ToList();
            while (MoveNext(items));
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
