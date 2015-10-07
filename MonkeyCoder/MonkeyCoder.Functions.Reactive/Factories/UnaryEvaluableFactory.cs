using System;
using System.Collections.Concurrent;

namespace MonkeyCoder.Functions.Reactive
{
    internal abstract partial class UnaryEvaluableFactory<TUnaryOperation> : BaseEvaluableFactory
        where TUnaryOperation : IUnaryEvaluable, new()
    {
        protected virtual bool AcceptsA(IEvaluable a) =>
            true;

        protected virtual void OnOperandsReady(IEvaluable a, IObserver<IEvaluable> observer) =>
            observer.OnNext(new TUnaryOperation { A = a });

        protected override void OnNext(IObserver<IEvaluable> observer, IEvaluable current, ConcurrentBag<IEvaluable> history, ConcurrentBag<IEvaluableFactoryProvider> factoryProviders)
        {
            history.Add(current);

            if (!AcceptsA(current))
                return;

            OnOperandsReady(current, observer);
        }
    }
}
