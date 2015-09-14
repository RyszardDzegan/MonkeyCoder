using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MonkeyCoder.Functions
{
    public static class FunctionManagerFactory
    {
        public static IEnumerable<Func<object>> AsFunctionsTree(this IEnumerable<object> items, int stackSize = 0)
        {
            if (items is IReadOnlyCollection<object>)
                return new FunctionManager((IReadOnlyCollection<object>) items, stackSize);

            if (items is IList<object>)
                return new FunctionManager(new ReadOnlyCollection<object>((IList<object>) items), stackSize);

            return new FunctionManager(items.ToList(), stackSize);
        }
    }
}
