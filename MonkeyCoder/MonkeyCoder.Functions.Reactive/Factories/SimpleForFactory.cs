using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reactive.Linq;

namespace MonkeyCoder.Functions.Reactive
{
    internal class SimpleForFactory : TernaryEvaluableFactory<SimpleFor>
    {
        protected override bool AcceptsA(IEvaluable a) =>
            AssignablityHelper<IVariable>.IsAssignableFrom(a);

        protected override bool AcceptsB(IEvaluable b) =>
            AssignablityHelper<IBoolean>.IsAssignableFrom(b);

        protected override bool AcceptsC(IEvaluable c) =>
            AssignablityHelper<INumber>.IsAssignableFrom(c);

        protected override void OnNext(IObserver<IEvaluable> observer, IEvaluable current, ConcurrentBag<IEvaluable> history, ConcurrentBag<IEvaluableFactoryProvider> factoryProviders)
        {
            history.Add(current);

            var a = new NumberVariable("i", 0);
            var originalHistoryObservable = CreateHistoryObservable(history.Concat(new[] { a }));
            var factoryProvidersObservable = CreateFactoryProvidersObservable(factoryProviders);

            var originalOperandFactories =
                from factoryProvider in factoryProviders
                let factories = factoryProvider.New(factoryProvidersObservable, originalHistoryObservable, StackSize - 1)
                select factories;

            var lessThanEnumerable =
                from historyItem in history
                from factoryProvider in factoryProviders
                let factories = factoryProvider.New(factoryProvidersObservable, originalHistoryObservable, StackSize - 1)
                select new LessThan { A = a, B = historyItem };
            
            var historyObservable = CreateHistoryObservable(history.Concat(new[] { a }));

            var operandFactories =
                from factoryProvider in factoryProviders
                let factories = factoryProvider.New(factoryProvidersObservable, historyObservable, StackSize - 1)
                select factories;

            foreach (var bFactory in originalOperandFactories)
            {
                bFactory.Where(AssignablityHelper<INumber>.IsAssignableFrom).Subscribe(b =>
                {
                    var lessThan = new LessThan { A = a, B = b };
                    foreach (var cFactory in operandFactories)
                    {
                        cFactory.Where(AcceptsC).Subscribe(c =>
                            OnOperandsReady(a, lessThan, c, observer));
                    }
                });
            }
        }
    }
}
