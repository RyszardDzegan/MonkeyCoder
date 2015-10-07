using System;
using System.Collections.Concurrent;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace MonkeyCoder.Functions.Reactive
{
    internal abstract partial class BaseEvaluableFactory : IEvaluableFactory
    {
        public IObservable<IEvaluableFactoryProvider> FactoryProvidersSource { get; set; }
        public IObservable<IEvaluable> DataSource { get; set; }
        public int StackSize { get; set; }
        
        protected IObservable<IEvaluable> CreateHistoryObservable(ConcurrentBag<IEvaluable> history)
        {
            return Observable.Create<IEvaluable>(o =>
            {
                foreach (var previousData in history)
                    o.OnNext(previousData);
                return Disposable.Empty;
            });
        }

        protected IObservable<IEvaluableFactoryProvider> CreateFactoryProvidersObservable(ConcurrentBag<IEvaluableFactoryProvider> factoryProviders)
        {
            return Observable.Create<IEvaluableFactoryProvider>(o =>
            {
                foreach (var factory in factoryProviders)
                    o.OnNext(factory);
                return Disposable.Empty;
            });
        }

        protected abstract void OnNext(IObserver<IEvaluable> observer, IEvaluable current, ConcurrentBag<IEvaluable> history, ConcurrentBag<IEvaluableFactoryProvider> factoryProviders);

        public IDisposable Subscribe(IObserver<IEvaluable> observer)
        {
            if (StackSize < 0)
                return Disposable.Empty;

            var factoryProviders = new ConcurrentBag<IEvaluableFactoryProvider>();
            FactoryProvidersSource.Subscribe(factoryProviders.Add);

            var history = new ConcurrentBag<IEvaluable>();

            return DataSource.Subscribe(
                onNext: current => OnNext(observer, current, history, factoryProviders),
                onCompleted: observer.OnCompleted);
        }
    }
}
