using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MonkeyCoder.Math
{
    internal class PowerSet<T> : IEnumerable<IList<T>>
    {
        public IList<T> Items { get; }
        
        public PowerSet(IEnumerable<T> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));
            
            Items = items.ToList();
        }

        public IEnumerable<bool[]> GetBijections()
        {
            var bijection = new bool[Items.Count];
            yield return bijection;

            for (var subsetSize = 1; subsetSize <= bijection.Length; subsetSize++)
            {
                InitializeBijection(bijection, bijection.Length, subsetSize);

                foreach (var change in ShiftBits(bijection, subsetSize, bijection.Length, subsetSize))
                    yield return change;
            }
        }

        private static IEnumerable<bool[]> ShiftBits(bool[] bijection, int subsetSize, int innerSubsetBegin, int innerSubsetSize)
        {
            if (innerSubsetSize > 1)
            {
                foreach (var change in ShiftBits(bijection, subsetSize, innerSubsetBegin - 1, innerSubsetSize - 1))
                    yield return change;
                ClearLastBit(bijection, innerSubsetBegin);

                if (!CanSetInnerSubsetFrontBits(innerSubsetBegin, innerSubsetSize))
                    yield break;

                InitializeBijection(bijection, innerSubsetBegin - 1, innerSubsetSize);
                foreach (var change in ShiftBits(bijection, subsetSize, innerSubsetBegin - 1, innerSubsetSize))
                    yield return change;
                yield break;
            }

            do yield return bijection;
            while (ShiftLeftFirstSubsetBit(bijection, innerSubsetBegin--));
        }

        private static void InitializeBijection(bool[] bijection, int subsetBegin, int subsetSize)
        {
            while (subsetSize-- > 0)
                bijection[--subsetBegin] = true;

            while (--subsetBegin >= 0)
                bijection[subsetBegin] = false;
        }

        private static bool ShiftLeftFirstSubsetBit(bool[] bijection, int innerSubsetBegin)
        {
            if (innerSubsetBegin <= 1)
            {
                bijection[0] = false;
                return false;
            }

            bijection[innerSubsetBegin - 1] = false;
            bijection[innerSubsetBegin - 2] = true;

            return true;
        }

        private static void ClearLastBit(bool[] bijection, int innerSubsetBegin)
        {
            bijection[innerSubsetBegin - 1] = false;
        }

        private static bool CanSetInnerSubsetFrontBits(int innerSubsetBegin, int innerSubsetSize)
        {
            return (innerSubsetBegin - innerSubsetSize >= 1);
        }

        public IEnumerator<IList<T>> GetEnumerator()
        {
            if (!Items.Any())
            {
                yield return new T[0];
                yield break;
            }
            
            var bijections = GetBijections().Select(x => string.Join("", x.Select(y => Convert.ToInt32(y)))).ToArray();

            foreach (var bijection in GetBijections())
            {
                yield return bijection
                    .Reverse()
                    .Select((x, i) => new { IsPresent = x, Index = i })
                    .Where(x => x.IsPresent)
                    .Select(x => Items[x.Index])
                    .ToList();
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}