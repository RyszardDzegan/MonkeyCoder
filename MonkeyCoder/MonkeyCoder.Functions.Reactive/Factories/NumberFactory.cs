using System;
using System.Reactive.Disposables;

namespace MonkeyCoder.Functions.Reactive
{
    internal class NumberFactory : IEvaluableFactory
    {
        public IObservable<IEvaluableFactoryProvider> FactoryProvidersSource { get; set; }
        public IObservable<IEvaluable> DataSource { get; set; }
        public int StackSize { get; set; }

        public IDisposable Subscribe(IObserver<IEvaluable> observer)
        {
            if (StackSize < 0)
            {
                observer.OnCompleted();
                return Disposable.Empty;
            }

            return DataSource.Subscribe(
                onNext: current => observer.OnNext(new Number(current.Evaluate())),
                onCompleted: observer.OnCompleted);
        }
    }
}
