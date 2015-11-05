using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MonkeyCoder.Functions
{
    /// <summary>
    /// Contains the <see cref="AsFunctionsTree(IEnumerable{object}, int)"/> extension method.
    /// </summary>
    public static class FunctionManagerFactory
    {
        /// <summary>
        /// Each item can be a simple value or a function.
        /// If it is a value, then it will be wrapped into a function that will just return that value.
        /// In case of a function or action, all items will be treated as potential arguments for that function
        /// and steps will be undertaken to invoke that function with all possible arguments combinations.
        /// To prevent an infinitive call stack, a stack size argument is provided.
        /// Its default value is 0 which means that only first level functions will be invoked.
        /// </summary>
        /// <param name="items">A collection of items that can be simple values, functions or actions.</param>
        /// <param name="stackSize">Prevents an infinitive call stack.</param>
        public static IEnumerable<IInvocation> AsFunctionsTree<T>(this IEnumerable<T> items, int stackSize = 0)
        {
            if (items is IReadOnlyCollection<object>)
                return new FunctionManager((IReadOnlyCollection<object>)items, stackSize);

            if (items is IList<object>)
                return new FunctionManager(new ReadOnlyCollection<object>((IList<object>)items), stackSize);

            return new FunctionManager(items.Cast<object>().ToList(), stackSize);
        }
    }
}
