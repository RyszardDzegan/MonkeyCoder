using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MonkeyCoder.Functions.Helpers.Invocations
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
    internal class InvocationsEnumerable : IEnumerable<IInvocation>
    {
        private IEnumerable<IInvocation> Invocations { get; }

        /// <summary>
        /// A constructor that creates an empty invocation list.
        /// </summary>
        public InvocationsEnumerable()
        {
            Invocations = Enumerable.Empty<IInvocation>();
        }

        /// <summary>
        /// A constructor that creates an invocation list
        /// consisting of one function
        /// which returned value is provided <paramref name="value"/>.
        /// </summary>
        /// <param name="value">A value that is to be returned by a function.</param>
        public InvocationsEnumerable(object value)
        {
            var invocationInfo = new ValueInvocation(value);
            Invocations = Enumerable.Repeat(invocationInfo, 1);
        }

        /// <summary>
        /// A constructor that creates an invocation list
        /// consisting of one function
        /// which invokes provided <paramref name="@delegate"/>
        /// and returns its returned value.
        /// </summary>
        /// <param name="@delegate">A delegate that is to be invoked.</param>
        public InvocationsEnumerable(Delegate @delegate)
        {
            var invocationInfo = new DelegateInvocation(@delegate);
            Invocations = Enumerable.Repeat(invocationInfo, 1);
        }

        /// <summary>
        /// A constructor that creates an invocation list
        /// consisting of a <paramref name="delegate"/>
        /// that will be invoked with arguments
        /// provided in <paramref name="valueEnumerables"/>
        /// </summary>
        /// <param name="@delegate">A delegate from which ivocation list will be produced.</param>
        /// <param name="valueEnumerables">An enumerable of arguments that will be used to invoke the <paramref name="delegate"/>.</param>
        public InvocationsEnumerable(Delegate @delegate, IEnumerable<IEnumerable<object>> valueEnumerables)
        {
            Invocations = from valueEnumerable in valueEnumerables
                          select new DelegateInvocation(@delegate, valueEnumerable);
        }

        /// <summary>
        /// A constructor that uses existing invocation list.
        /// </summary>
        /// <param name="invocations">An existing invocation list.</param>
        public InvocationsEnumerable(IEnumerable<IInvocation> invocations)
        {
            Invocations = invocations;
        }

        public IEnumerator<IInvocation> GetEnumerator() => Invocations.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Creates a lazy and empty invocation list.
        /// </summary>
        /// <returns>A lazy instance of <see cref="InvocationsEnumerable"/>.</returns>
        public static Lazy<InvocationsEnumerable> Lazy() =>
            new Lazy<InvocationsEnumerable>(() => new InvocationsEnumerable());

        /// <summary>
        /// Creates a lazy invocation list
        /// consisting of one function
        /// which returned value is provided <paramref name="value"/>.
        /// </summary>
        /// <param name="value">A value that is to be returned by a function.</param>
        /// <returns>A lazy instance of <see cref="InvocationsEnumerable"/>.</returns>
        public static Lazy<InvocationsEnumerable> Lazy(object value) =>
            new Lazy<InvocationsEnumerable>(() => new InvocationsEnumerable(value));

        /// <summary>
        /// Creates a lazy invocation list
        /// consisting of one function
        /// which invokes provided <paramref name="@delegate"/>
        /// and returns its returned value.
        /// </summary>
        /// <param name="@delegate">A delegate that is to be invoked.</param>
        /// <returns>A lazy instance of <see cref="InvocationsEnumerable"/>.</returns>
        public static Lazy<InvocationsEnumerable> Lazy(Delegate @delegate) =>
            new Lazy<InvocationsEnumerable>(() => new InvocationsEnumerable(@delegate));

        /// <summary>
        /// Creates a lazy invocation list
        /// consisting of a <paramref name="delegate"/>
        /// that will be invoked with arguments
        /// provided in <paramref name="valueEnumerables"/>
        /// </summary>
        /// <param name="@delegate">A delegate from which ivocation list will be produced.</param>
        /// <param name="valueEnumerables">An enumerable of arguments that will be used to invoke the <paramref name="delegate"/>.</param>
        /// <returns>A lazy instance of <see cref="InvocationsEnumerable"/>.</returns>
        public static Lazy<InvocationsEnumerable> Lazy(Delegate @delegate, IEnumerable<IEnumerable<object>> valueEnumerables) =>
            new Lazy<InvocationsEnumerable>(() => new InvocationsEnumerable(@delegate, valueEnumerables));

        /// <summary>
        /// Creates a lazy invocation list that uses existing invocation list.
        /// </summary>
        /// <param name="invocations">An existing invocation list.</param>
        /// <returns>A lazy instance of <see cref="InvocationsEnumerable"/>.</returns>
        public static Lazy<InvocationsEnumerable> Lazy(IEnumerable<IInvocation> invocations) =>
            new Lazy<InvocationsEnumerable>(() => new InvocationsEnumerable(invocations));
    }
}
