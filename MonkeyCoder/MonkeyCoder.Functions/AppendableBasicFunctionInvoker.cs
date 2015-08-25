using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace MonkeyCoder.Functions
{
    internal class AppendableBasicFunctionInvoker : IEnumerable<Func<object>>
    {
        public Delegate Function { get; }
        public IReadOnlyCollection<object> PossibleArguments => DynamicPossibleArguments.ToArray();

        private object _lock = new object();
        private IList<object> DynamicPossibleArguments { get; }
        private BlockingCollection<IEnumerable<Func<object>>> FunctionInvokers { get; } = new BlockingCollection<IEnumerable<Func<object>>>();

        public AppendableBasicFunctionInvoker(Delegate function, params object[] possibleArguments)
            : this(function, (IReadOnlyCollection<object>)possibleArguments)
        { }

        public AppendableBasicFunctionInvoker(Delegate function, IReadOnlyCollection<object> possibleArguments)
        {
            if (function == null)
                throw new ArgumentNullException(nameof(function), "Function cannot be null.");

            if (possibleArguments == null)
                throw new ArgumentNullException(nameof(possibleArguments), "Possible arguments cannot be null.");

            Function = function;
            DynamicPossibleArguments = new List<object>(possibleArguments);

            var functionInvoker = new BasicFunctionInvoker(Function, PossibleArguments);
            FunctionInvokers.Add(functionInvoker);
        }

        public void Add(object possibleArgument)
        {
            lock (_lock)
            {
                if (FunctionInvokers.IsAddingCompleted)
                    return;

                var functionInvoker = new MandatoryArgumentBasicFunctionInvoker(Function, PossibleArguments, possibleArgument);
                FunctionInvokers.Add(functionInvoker);
                DynamicPossibleArguments.Add(possibleArgument);
            }
        }

        public void Complete()
        {
            lock (_lock) FunctionInvokers.CompleteAdding();
        }

        public IEnumerator<Func<object>> GetEnumerator()
        {
            var resultsQuery =
                from invoker in FunctionInvokers
                from function in invoker
                select function;
            return resultsQuery.GetEnumerator();
        }
        
        public IEnumerable<Func<object>> GetConsumingEnumerable(CancellationToken cancellationToken)
        {
            var resultsQuery =
                from invoker in FunctionInvokers.GetConsumingEnumerable(cancellationToken)
                from function in invoker
                select function;

            foreach (var result in resultsQuery)
                yield return result;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerable<Func<object>> GetConsumingEnumerable() => GetConsumingEnumerable(CancellationToken.None);
    }
}
