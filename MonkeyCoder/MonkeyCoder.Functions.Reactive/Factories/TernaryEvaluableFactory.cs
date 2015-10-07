using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reactive.Linq;

namespace MonkeyCoder.Functions.Reactive
{
    internal abstract partial class TernaryEvaluableFactory<TTernaryOperation> : BaseEvaluableFactory
        where TTernaryOperation : ITernaryEvaluable, new()
    {
        protected virtual bool AcceptsA(IEvaluable a) =>
            true;

        protected virtual bool AcceptsB(IEvaluable b) =>
            true;

        protected virtual bool AcceptsC(IEvaluable c) =>
            true;

        protected virtual bool AcceptsAll(IEvaluable a, IEvaluable b, IEvaluable c) =>
            true;

        protected virtual void OnOperandsReady(IEvaluable a, IEvaluable b, IEvaluable c, IObserver<IEvaluable> observer) =>
            observer.OnNext(new TTernaryOperation { A = a, B = b, C = c });

        protected override void OnNext(IObserver<IEvaluable> observer, IEvaluable current, ConcurrentBag<IEvaluable> history, ConcurrentBag<IEvaluableFactoryProvider> factoryProviders)
        {
            history.Add(current);

            var historyObservable = CreateHistoryObservable(history);
            var factoryProvidersObservable = CreateFactoryProvidersObservable(factoryProviders);

            var operandFactories =
                from factoryProvider in factoryProviders
                let factories = factoryProvider.New(factoryProvidersObservable, historyObservable, StackSize - 1)
                select factories;

            foreach (var aFactory in operandFactories)
            {
                aFactory.Where(AcceptsA).Subscribe(a =>
                {
                    foreach (var bFactory in operandFactories)
                    {
                        bFactory.Where(AcceptsB).Subscribe(b =>
                        {
                            foreach (var cFactory in operandFactories)
                            {
                                cFactory.Where(AcceptsC).Where(c => AcceptsAll(a, b, c)).Subscribe(c =>
                                    OnOperandsReady(a, b, c, observer));
                            }
                        });
                    }
                });
            }
        }
    }
}
