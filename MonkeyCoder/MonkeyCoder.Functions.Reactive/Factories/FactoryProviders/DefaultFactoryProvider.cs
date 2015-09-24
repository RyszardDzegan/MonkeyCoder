using System;

namespace MonkeyCoder.Functions.Reactive
{
    internal class DefaultFactoryProvider<TEvaluableFactory> : IEvaluableFactoryProvider
        where TEvaluableFactory : IEvaluableFactory, new()
    {
        public virtual bool AcceptsExpected(IEvaluable expected) => true;

        public IObservable<IEvaluable> New(IObservable<IEvaluableFactoryProvider> factoryProvidersSource, IObservable<IEvaluable> dataSource, IEvaluable expected, int stackSize) =>
            new TEvaluableFactory
            {
                FactoryProvidersSource = factoryProvidersSource,
                DataSource = dataSource,
                Expected = expected,
                StackSize = stackSize
            };
    }
}
