using System;
using System.Linq;
using System.Collections.Concurrent;
using System.Reactive.Linq;

namespace MonkeyCoder.Functions.Reactive
{
    internal abstract partial class BinaryEvaluableFactory<TBinaryOperation> : IEvaluableFactory
        where TBinaryOperation : IBinaryEvaluable, new()
    {
        internal abstract class RightSide : BinaryEvaluableFactory<TBinaryOperation>
        {
            protected override void OnNext(IObserver<IEvaluable> observer, IEvaluable current, ConcurrentBag<IEvaluable> history, ConcurrentBag<IEvaluableFactoryProvider> factoryProviders)
            {
                history.Add(current);

                if (!AcceptsA(current))
                    return;

                var historyObservable = CreateHistoryObservable(history);
                var factoryProvidersObservable = CreateFactoryProvidersObservable(factoryProviders);

                var rightOperandFactories =
                    from factoryProvider in factoryProviders
                    let factories = factoryProvider.New(factoryProvidersObservable, historyObservable, StackSize - 1)
                    let acceptedFactories = factories.Where(AcceptsB)
                    select acceptedFactories;

                foreach (var rightOperandFactory in rightOperandFactories)
                    rightOperandFactory.Subscribe(rightOperand => OnOperandsReady(current, rightOperand, observer));
            }
        }
    }
}
