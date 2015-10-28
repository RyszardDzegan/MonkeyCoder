using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MonkeyCoder.Functions.Helpers
{
    /// <summary>
    /// Represents a functions invocation list.
    /// Each function is wrapped into parameterless function
    /// that constitues a closure containing original function
    /// call together with its prepared arguments.
    /// The wrapping function always returns an <see cref="object"/>
    /// which is the original function returned value.
    /// The returned value can be of type <see cref="void"/>.
    /// </summary>
    internal class FunctionsEnumerable : IEnumerable<Func<object>>
    {
        private IEnumerable<Func<object>> enumerable { get; }

        /// <summary>
        /// A constructor that creates an empty invocation list.
        /// </summary>
        public FunctionsEnumerable()
        {
            enumerable = Enumerable.Empty<Func<object>>();
        }

        /// <summary>
        /// A constructor that creates an invocation list
        /// consisting of one function
        /// which returned value is provided <paramref name="value"/>.
        /// </summary>
        /// <param name="value">A value that is to be returned by a function.</param>
        public FunctionsEnumerable(object value)
        {
            enumerable = Enumerable.Repeat(new Func<object>(() => value), 1);
        }

        /// <summary>
        /// A constructor that creates an invocation list
        /// consisting of one function
        /// which invokes provided <paramref name="@delegate"/>
        /// and returns its returned value.
        /// </summary>
        /// <param name="@delegate">A delegate that is to be invoked.</param>
        public FunctionsEnumerable(Delegate @delegate)
        {
            enumerable = Enumerable.Repeat(new Func<object>(() => @delegate.DynamicInvoke()), 1);
        }

        /// <summary>
        /// A constructor that takes a preprepared invocation list.
        /// </summary>
        /// <param name="functions">A preprepared invocation list.</param>
        public FunctionsEnumerable(IEnumerable<Func<object>> functions)
        {
            enumerable = functions;
        }

        public IEnumerator<Func<object>> GetEnumerator() => enumerable.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Creates a lazy and empty invocation list.
        /// </summary>
        public static Lazy<FunctionsEnumerable> Lazy() =>
            new Lazy<FunctionsEnumerable>(() => new FunctionsEnumerable());

        /// <summary>
        /// Creates a lazy invocation list
        /// consisting of one function
        /// which returned value is provided <paramref name="value"/>.
        /// </summary>
        /// <param name="value">A value that is to be returned by a function.</param>
        public static Lazy<FunctionsEnumerable> Lazy(object value) =>
            new Lazy<FunctionsEnumerable>(() => new FunctionsEnumerable(value));

        /// <summary>
        /// Creates a lazy invocation list
        /// consisting of one function
        /// which invokes provided <paramref name="@delegate"/>
        /// and returns its returned value.
        /// </summary>
        /// <param name="@delegate">A delegate that is to be invoked.</param>
        public static Lazy<FunctionsEnumerable> Lazy(Delegate @delegate) =>
            new Lazy<FunctionsEnumerable>(() => new FunctionsEnumerable(@delegate));

        /// <summary>
        /// Creates a lazy invocation list
        /// from preprepared functions.
        /// </summary>
        /// <param name="functions">A preprepared invocation list.</param>
        public static Lazy<FunctionsEnumerable> Lazy(IEnumerable<Func<object>> functions) =>
            new Lazy<FunctionsEnumerable>(() => new FunctionsEnumerable(functions));
    }
}
