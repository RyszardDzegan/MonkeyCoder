using System;
using System.Linq;
using System.Collections.Concurrent;
using System.Reactive.Linq;

namespace MonkeyCoder.Functions.Reactive
{
    internal abstract partial class BinaryEvaluableFactory<TBinaryOperation> : IEvaluableFactory
        where TBinaryOperation : IBinaryEvaluable, new()
    {
        internal abstract class BothSides : BinaryEvaluableFactory<TBinaryOperation>
        {
            protected override void OnNext(IObserver<IEvaluable> observer, IEvaluable current, ConcurrentBag<IEvaluable> history, ConcurrentBag<IEvaluableFactoryProvider> factoryProviders)
            {
                history.Add(current);

                var historyObservable = CreateHistoryObservable(history);
                var factoryProvidersObservable = CreateFactoryProvidersObservable(factoryProviders);

                var operandFactories =
                    from factoryProvider in factoryProviders
                    let factories = factoryProvider.New(factoryProvidersObservable, historyObservable, StackSize - 1)
                    select factories;

                foreach (var operandFactory in operandFactories)
                    operandFactory.Where(AcceptsA).Subscribe(left =>
                        operandFactory.Where(AcceptsB).Subscribe(right =>
                            OnRightOperandNext(left, right, observer)));
            }
        }
    }
}
