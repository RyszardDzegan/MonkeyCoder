using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MonkeyCoder.Core.Math
{
    internal class VariationsWithRepetitions<T> : IEnumerable<IList<T>>
    {
        public IList<T> Items { get; }
        public int K { get; }
        
        public VariationsWithRepetitions(IEnumerable<T> items, int k)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            if (k < 0)
                throw new ArgumentOutOfRangeException(nameof(k), $"k must be greater or equal 0, but is {k}.");

            Items = items.ToList();

            if (k > Items.Count)
                throw new ArgumentOutOfRangeException(nameof(k), $"k must be less or equal items count, but is {k}.");

            K = k;
        }

        private bool MoveNext(IList<int> bijection)
        {
            for (var i = 0; i < bijection.Count(); i++)
            {
                if (bijection[bijection.Count - 1 - i] + 1 < Items.Count)
                {
                    bijection[bijection.Count - 1 - i]++;
                    return true;
                }

                bijection[bijection.Count - 1 - i] = 0;
            }

            return false;
        }

        public IEnumerator<IList<T>> GetEnumerator()
        {
            if (K == 0 || !Items.Any())
                yield break;

            var bijection = new int[K];
            var resultQuery = from x in bijection
                              select Items[x];

            do yield return resultQuery.ToList();
            while (MoveNext(bijection));
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
