using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MonkeyCoder.Functions.Helpers
{
    internal class FunctionsEnumerable : IEnumerable<Func<object>>
    {
        private IEnumerable<Func<object>> enumerable { get; }

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

        public static Lazy<FunctionsEnumerable> Lazy(Delegate d) => new Lazy<FunctionsEnumerable>(() => new FunctionsEnumerable(d));
        public static Lazy<FunctionsEnumerable> Lazy(IEnumerable<Func<object>> functions) => new Lazy<FunctionsEnumerable>(() => new FunctionsEnumerable(functions));
    }
}
