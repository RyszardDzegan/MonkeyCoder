using System;

namespace MonkeyCoder.Functions.Reactive
{
    internal class NumberFactory : IEvaluableFactory
    {
        public IObservable<IEvaluableFactoryProvider> FactoryProvidersSource { get; set; }
        public IObservable<IEvaluable> DataSource { get; set; }
        public IEvaluable Expected { get; set; }
        public int StackSize { get; set; }

        public IDisposable Subscribe(IObserver<IEvaluable> observer)
        {
            return DataSource.Subscribe(
                onNext: x =>
                {
                    var a = x.Evaluate();
                    var b = Expected.Evaluate();

                    if (a == b)
                        observer.OnNext(new Number(a));
                },
                onCompleted: observer.OnCompleted);
        }
    }
}
