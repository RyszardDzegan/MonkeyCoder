using MonkeyCoder.Functions.Invocations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MonkeyCoder.Functions.Internals
{
    internal partial class Basic
    {
        /// <summary>
        /// The <see cref="Multiple"/> class is built upon <see cref="Basic"/> class.
        /// As opose to the <see cref="Basic"/> class, the <see cref="Multiple"/> class
        /// allows to provide multiple <see cref="Delegate"/>'s to the main constructor.
        /// </summary>
        internal class Multiple : IEnumerable<IInvocation>
        {
            private Lazy<IReadOnlyCollection<IEnumerable<IInvocation>>> Invokers { get; }

            /// <summary>
            /// A constructor that takes multiple <see cref="Delegate"/>'s and their argument candidates.
            /// </summary>
            /// <param name="functions">Functions from which invocation lists will be produced.</param>
            /// <param name="possibleArguments">Argument candidates for <paramref name="functions"/>.</param>
            /// <exception cref="ArgumentNullException">When <paramref name="functions"/> are null.</exception>
            /// <exception cref="ArgumentNullException">When <paramref name="possibleArguments"/> are null.</exception>
            /// <exception cref="ArgumentException">When <paramref name="functions"/> collection contains any null.</exception>
            public Multiple(IReadOnlyCollection<Delegate> functions, params object[] possibleArguments)
                : this(functions, (IReadOnlyCollection<object>)possibleArguments)
            { }

            /// <summary>
            /// A constructor that takes multiple <see cref="Delegate"/>'s and their argument candidates.
            /// </summary>
            /// <param name="functions">Functions from which invocation lists will be produced.</param>
            /// <param name="possibleArguments">Argument candidates for <paramref name="functions"/>.</param>
            /// <exception cref="ArgumentNullException">When <paramref name="functions"/> are null.</exception>
            /// <exception cref="ArgumentNullException">When <paramref name="possibleArguments"/> are null.</exception>
            /// <exception cref="ArgumentException">When <paramref name="functions"/> collection contains any null.</exception>
            public Multiple(IReadOnlyCollection<Delegate> functions, IReadOnlyCollection<object> possibleArguments)
            {
                if (functions == null)
                    throw new ArgumentNullException(nameof(functions), "Functions cannot be null.");

                if (possibleArguments == null)
                    throw new ArgumentNullException(nameof(possibleArguments), "Possible arguments cannot be null.");

                if (functions.Any(x => x == null))
                {
                    var nullPositions = functions
                        .Select((x, i) => new { Function = x, Index = i })
                        .Where(x => x.Function == null)
                        .Select(x => x.Index);
                    var joinedNullPositions = string.Join(", ", nullPositions);
                    throw new ArgumentException($"All functions must be not null. Found nulls at positions: {joinedNullPositions}", nameof(functions));
                }
                
                Invokers = new Lazy<IReadOnlyCollection<IEnumerable<IInvocation>>>(
                    () => functions.Select(x => new Basic(x, possibleArguments)).ToArray());
            }

            public IEnumerator<IInvocation> GetEnumerator() => Invokers.Value.SelectMany(x => x).GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
