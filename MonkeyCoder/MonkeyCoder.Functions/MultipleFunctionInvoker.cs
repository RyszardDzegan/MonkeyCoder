using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MonkeyCoder.Functions
{
    internal class MultipleFunctionInvoker : ISingleFunctionInvoker
    {
        public Delegate Function => Functions.FirstOrDefault();
        public IReadOnlyCollection<Delegate> Functions { get; }
        public IReadOnlyCollection<object> PossibleArguments { get; }
        private Lazy<IReadOnlyCollection<ISingleFunctionInvoker>> Invokers { get; }
        
        public MultipleFunctionInvoker(IReadOnlyCollection<Delegate> functions, params object[] possibleArguments)
            : this(functions, (IReadOnlyCollection<object>)possibleArguments)
        { }

        public MultipleFunctionInvoker(IReadOnlyCollection<Delegate> functions, IReadOnlyCollection<object> possibleArguments)
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

            Functions = functions;
            PossibleArguments = possibleArguments;

            Invokers = new Lazy<IReadOnlyCollection<ISingleFunctionInvoker>>(
                () => Functions.Select(x => new SingleFunctionInvoker(x, possibleArguments)).ToArray());
        }

        public IEnumerator<Func<object>> GetEnumerator() => Invokers.Value.SelectMany(x => x).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
