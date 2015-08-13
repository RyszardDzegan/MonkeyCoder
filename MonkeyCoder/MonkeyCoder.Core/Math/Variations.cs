using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MonkeyCoder.Core.Math
{
    public class Variations<T> : IEnumerable<IList<T>>
    {
        public IEnumerable<IEnumerator<T>> Items { get; }

        public Variations(IEnumerable<IEnumerable<T>> items)
        {
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
            var items = Items.ToArray();

            foreach (var x in items)
                if (!x.MoveNext())
                    yield break;

            var resultQuery = from x in items
                              select x.Current;

            do yield return resultQuery.ToList();
            while (MoveNext(items));
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
