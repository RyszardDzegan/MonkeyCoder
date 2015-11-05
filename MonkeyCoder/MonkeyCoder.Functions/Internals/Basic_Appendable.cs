using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace MonkeyCoder.Functions.Internals
{
    internal partial class Basic
    {
        /// <summary>
        /// The <see cref="Appendable"/> class has its core functionality the same as the <see cref="Basic"/> class.
        /// That is, it takes a <see cref="Delegate"/> and a collection of its input arguments candidates.
        /// Argument can be null, doesn't have to be unique and an argument that is a function will be treated
        /// just as a value of a delegate type. It means that a function won't be evaluated at runtime to match its
        /// return value to the main function parameter.
        /// As opose to the <see cref="Basic"/> class, the <see cref="Appendable"/> class allows to append
        /// argument candidates at runtime using the <see cref="Add(object)"/> method.
        /// It works similarly to <see cref="BlockingCollection{T}"/> and it is built upon <see cref="Mandatory"/> class.
        /// </summary>
        internal class Appendable : IEnumerable<IInvocation>
        {
            private Delegate Function { get; }
            private BlockingCollection<IEnumerable<IInvocation>> FunctionInvokers { get; } = new BlockingCollection<IEnumerable<IInvocation>>();
            private IList<object> DynamicPossibleArguments { get; }
            private IReadOnlyCollection<object> PossibleArguments => DynamicPossibleArguments.ToArray();
            private object _lock = new object();

            /// <summary>
            /// The constructor that takes a function and a collection of argument candidates.
            /// </summary>
            /// <param name="function">The function for which an invocation list is to be created.</param>
            /// <param name="possibleArguments">The candidates for arguments for the function.</param>
            /// <exception cref="ArgumentNullException">When <paramref name="function"/> is null.</exception>
            /// <exception cref="ArgumentNullException">When <paramref name="possibleArguments"/> are null.</exception>
            public Appendable(Delegate function, params object[] possibleArguments)
                : this(function, (IReadOnlyCollection<object>)possibleArguments)
            { }

            /// <summary>
            /// The constructor that takes a function and a collection of argument candidates.
            /// </summary>
            /// <param name="function">The function for which an invocation list is to be created.</param>
            /// <param name="possibleArguments">The candidates for arguments for the function.</param>
            /// <exception cref="ArgumentNullException">When <paramref name="function"/> is null.</exception>
            /// <exception cref="ArgumentNullException">When <paramref name="possibleArguments"/> are null.</exception>
            public Appendable(Delegate function, IReadOnlyCollection<object> possibleArguments)
            {
                if (function == null)
                    throw new ArgumentNullException(nameof(function), "FunctionArgument cannot be null.");

                if (possibleArguments == null)
                    throw new ArgumentNullException(nameof(possibleArguments), "Possible arguments cannot be null.");

                Function = function;
                DynamicPossibleArguments = new List<object>(possibleArguments);

                var functionInvoker = new Basic(Function, PossibleArguments);
                FunctionInvokers.Add(functionInvoker);
            }

            /// <summary>
            /// Adds an argument candidate.
            /// This method is thread safe.
            /// </summary>
            /// <param name="possibleArgument">A new argument candidate.</param>
            public void Add(object possibleArgument)
            {
                lock (_lock)
                {
                    if (FunctionInvokers.IsAddingCompleted)
                        return;

                    var functionInvoker = new Mandatory(Function, PossibleArguments, possibleArgument);
                    FunctionInvokers.Add(functionInvoker);
                    DynamicPossibleArguments.Add(possibleArgument);
                }
            }

            /// <summary>
            /// Indicates that no more argument candidates will be added and therefore
            /// releases the <see cref="GetConsumingEnumerable(CancellationToken)"/>.
            /// After invocing this method, all further invocations of <see cref="Add(object)"/> method will be ignored.
            /// This method is thread safe.
            /// </summary>
            public void Complete()
            {
                lock (_lock) FunctionInvokers.CompleteAdding();
            }

            /// <summary>
            /// Creates a snapshot of current function invokers
            /// and allows to enumerate through them.
            /// This method will not block as opposed to <see cref="GetConsumingEnumerable(CancellationToken)"/>.
            /// This method will ignore any <see cref="Add(object)"/> invocations during enumeration.
            /// </summary>
            /// <returns>A snapshot of current function invokers.</returns>
            public IEnumerator<IInvocation> GetEnumerator() =>
                FunctionInvokers.SelectMany(x => x).GetEnumerator();

            /// <summary>
            /// Creates an enumerable of function invokers
            /// and allows to enumerate through them.
            /// This method will block till <see cref="Complete"/> method is invoked.
            /// This method will respect new argument candidates added via <see cref="Add(object)"/> method.
            /// </summary>
            /// <param name="cancellationToken">Allows to release the block.</param>
            /// <returns>An enumerable of function invokers</returns>
            public IEnumerable<IInvocation> GetConsumingEnumerable(CancellationToken cancellationToken) =>
                from invoker in FunctionInvokers.GetConsumingEnumerable(cancellationToken)
                from function in invoker
                select function;

            /// <summary>
            /// Creates a snapshot of current function invokers
            /// and allows to enumerate through them.
            /// This method will not block as opposed to <see cref="GetConsumingEnumerable(CancellationToken)"/>.
            /// This method will ignore any <see cref="Add(object)"/> invocations during enumeration.
            /// </summary>
            /// <returns>A snapshot of current function invokers.</returns>
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            /// <summary>
            /// Creates an enumerable of function invokers
            /// and allows to enumerate through them.
            /// This method will block till <see cref="Complete"/> method is invoked.
            /// This method will respect new argument candidates added via <see cref="Add(object)"/> method.
            /// </summary>
            /// <returns>An enumerable of function invokers</returns>
            public IEnumerable<IInvocation> GetConsumingEnumerable() => GetConsumingEnumerable(CancellationToken.None);
        }
    }
}
