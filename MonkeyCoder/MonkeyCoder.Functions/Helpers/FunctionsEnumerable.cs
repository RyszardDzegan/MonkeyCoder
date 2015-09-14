using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MonkeyCoder.Functions.Helpers
{
    internal class FunctionsEnumerable : IEnumerable<Func<object>>
    {
        private IEnumerable<Func<object>> enumerable { get; }

        public FunctionsEnumerable()
        {
            enumerable = Enumerable.Empty<Func<object>>();
        }

        public FunctionsEnumerable(object value)
        {
            enumerable = Enumerable.Repeat(new Func<object>(() => value), 1);
        }

        public FunctionsEnumerable(Delegate @delegate)
        {
            enumerable = Enumerable.Repeat(new Func<object>(() => @delegate.DynamicInvoke()), 1);
        }

        public FunctionsEnumerable(IEnumerable<Func<object>> functions)
        {
            enumerable = functions;
        }

        public IEnumerator<Func<object>> GetEnumerator() => enumerable.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public static Lazy<FunctionsEnumerable> Lazy() => new Lazy<FunctionsEnumerable>(() => new FunctionsEnumerable());
        public static Lazy<FunctionsEnumerable> Lazy(object value) => new Lazy<FunctionsEnumerable>(() => new FunctionsEnumerable(value));
        public static Lazy<FunctionsEnumerable> Lazy(Delegate @delegate) => new Lazy<FunctionsEnumerable>(() => new FunctionsEnumerable(@delegate));
        public static Lazy<FunctionsEnumerable> Lazy(IEnumerable<Func<object>> functions) => new Lazy<FunctionsEnumerable>(() => new FunctionsEnumerable(functions));
    }
}
