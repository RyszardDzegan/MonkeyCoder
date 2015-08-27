using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace MonkeyCoder.Functions
{
    internal partial class Basic
    {
        internal class Appendable : IEnumerable<Func<object>>
        {
            private Delegate Function { get; }
            private BlockingCollection<IEnumerable<Func<object>>> FunctionInvokers { get; } = new BlockingCollection<IEnumerable<Func<object>>>();
            private IList<object> DynamicPossibleArguments { get; }
            private IReadOnlyCollection<object> PossibleArguments => DynamicPossibleArguments.ToArray();
            private object _lock = new object();

            public Appendable(Delegate function, params object[] possibleArguments)
                : this(function, (IReadOnlyCollection<object>)possibleArguments)
            { }

            public Appendable(Delegate function, IReadOnlyCollection<object> possibleArguments)
            {
                if (function == null)
                    throw new ArgumentNullException(nameof(function), "Function cannot be null.");

                if (possibleArguments == null)
                    throw new ArgumentNullException(nameof(possibleArguments), "Possible arguments cannot be null.");

                Function = function;
                DynamicPossibleArguments = new List<object>(possibleArguments);

                var functionInvoker = new Basic(Function, PossibleArguments);
                FunctionInvokers.Add(functionInvoker);
            }

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

            public void Complete()
            {
                lock (_lock) FunctionInvokers.CompleteAdding();
            }

            public IEnumerator<Func<object>> GetEnumerator() =>
                FunctionInvokers.SelectMany(x => x).GetEnumerator();

            public IEnumerable<Func<object>> GetConsumingEnumerable(CancellationToken cancellationToken) =>
                from invoker in FunctionInvokers.GetConsumingEnumerable(cancellationToken)
                from function in invoker
                select function;

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
            public IEnumerable<Func<object>> GetConsumingEnumerable() => GetConsumingEnumerable(CancellationToken.None);
        }
    }
}
