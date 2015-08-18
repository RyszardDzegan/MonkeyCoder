using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MonkeyCoder.Core.Math
{
    internal class Combinations<T> : IEnumerable<IList<T>>
    {
        public IList<T> Items { get; }
        public int K { get; }
        
        public Combinations(IEnumerable<T> items, int k)
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
                if (bijection[bijection.Count - 1 - i] + 1 < Items.Count - i)
                {
                    bijection[bijection.Count - 1 - i]++;
                    for (var j = 0; j < i; j++)
                        bijection[bijection.Count - i + j] = bijection[bijection.Count - 1 - i + j] + 1;
                    return true;
                }
            }

            return false;
        }

        public IEnumerator<IList<T>> GetEnumerator()
        {
            if (K == 0 || !Items.Any())
                yield break;

            var bijection = Enumerable.Range(0, K).ToList();
            var resultQuery = from x in bijection
                              select Items[x];

            do yield return resultQuery.ToList();
            while (MoveNext(bijection));
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
