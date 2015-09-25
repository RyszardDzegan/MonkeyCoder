using System;
using System.Linq;
using System.Collections.Concurrent;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Collections.Generic;

namespace MonkeyCoder.Functions.Reactive
{
    internal abstract class BinaryEvaluableFactory<TBinaryOperation> : IEvaluableFactory
        where TBinaryOperation : IBinaryEvaluable, new()
    {
        public IObservable<IEvaluableFactoryProvider> FactoryProvidersSource { get; set; }
        public IObservable<IEvaluable> DataSource { get; set; }
        public int StackSize { get; set; }

        protected virtual bool AcceptsA(IEvaluable a) => true;
        protected virtual bool AcceptsB(IEvaluable b) => true;
        protected virtual IEnumerable<dynamic> ProhibitedValues => Enumerable.Empty<IEnumerable<dynamic>>();
        protected virtual void OnChildNext(IEvaluable current, IEvaluable childNext, IObserver<IEvaluable> observer) => observer.OnNext(new TBinaryOperation { A = current, B = childNext });

        public IDisposable Subscribe(IObserver<IEvaluable> observer)
        {
            if (StackSize <= 0)
            {
                observer.OnCompleted();
                return Disposable.Empty;
            }

            var factoryProviders = new ConcurrentBag<IEvaluableFactoryProvider>();
            FactoryProvidersSource.Subscribe(factoryProviders.Add);

            var dataHistory = new ConcurrentBag<IEvaluable>();

            return DataSource.Subscribe(
                onNext: current =>
                {
                    dataHistory.Add(current);

                    if (!AcceptsA(current))
                        return;
                    
                    if (ProhibitedValues.Any(x => x == current.Evaluate()))
                        return;
                    
                    var internalDataSource = Observable.Create<IEvaluable>(o =>
                    {
                        foreach (var previousData in dataHistory)
                            o.OnNext(previousData);
                        return Disposable.Empty;
                    });

                    var internalFactoryProvidersSource = Observable.Create<IEvaluableFactoryProvider>(o =>
                    {
                        foreach (var factory in factoryProviders)
                            o.OnNext(factory);
                        return Disposable.Empty;
                    });
                    
                    var instances =
                        from provider in factoryProviders
                        select provider.New(internalFactoryProvidersSource, internalDataSource, StackSize - 1);

                    foreach (var instance in instances)
                        instance.Where(AcceptsB).Subscribe(childNext => OnChildNext(current, childNext, observer));
                },
                onCompleted: observer.OnCompleted);
        }
    }
}
