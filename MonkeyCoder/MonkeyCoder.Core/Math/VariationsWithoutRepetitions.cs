using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MonkeyCoder.Core.Math
{
    internal class VariationsWithoutRepetitions<T> : IEnumerable<IList<T>>
    {
        public IList<T> Items { get; }
        public int K { get; }
        
        public VariationsWithoutRepetitions(IEnumerable<T> items, int k)
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

        private bool MoveNext(IList<int> bijection, IList<bool> freeItems)
        {
            for (var i = 0; i < bijection.Count(); i++)
            {
                var nextFreeItem = freeItems
                    .Select((value, index) => new { FreeItemValue = value, FreeItemIndex = index })
                    .Skip(bijection[bijection.Count - 1 - i])
                    .FirstOrDefault(x => x.FreeItemValue);

                freeItems[bijection[bijection.Count - 1 - i]] = true;

                if (nextFreeItem == null)
                    continue;
                
                bijection[bijection.Count - 1 - i] = nextFreeItem.FreeItemIndex;
                freeItems[nextFreeItem.FreeItemIndex] = false;

                var freeItemsToUpdate = freeItems
                    .Select((value, index) => new { FreeItemValue = value, FreeItemIndex = index })
                    .Where(x => x.FreeItemValue)
                    .Select(x => x.FreeItemIndex)
                    .Take(i)
                    .Select((x, k) => new { FreeItemIndex = x, BijectionIndex = bijection.Count - i + k })
                    .ToList();
                
                foreach (var freeItem in freeItemsToUpdate)
                {
                    bijection[freeItem.BijectionIndex] = freeItem.FreeItemIndex;
                    freeItems[freeItem.FreeItemIndex] = false;
                }

                return true;
            }

            return false;
        }

        public IEnumerator<IList<T>> GetEnumerator()
        {
            if (K == 0 || !Items.Any())
                yield break;

            var bijection = Enumerable.Range(0, K).ToList();

            var freeItems = Enumerable.Repeat(true, Items.Count).ToList();
            for (var i = 0; i < K; i++)
                freeItems[i] = false;

            var resultQuery = from x in bijection
                              select Items[x];

            do yield return resultQuery.ToList();
            while (MoveNext(bijection, freeItems));
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
