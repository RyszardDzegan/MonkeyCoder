using System;

namespace MonkeyCoder.Functions.Reactive
{
    internal class DefaultFactoryProvider<TEvaluableFactory> : IEvaluableFactoryProvider
        where TEvaluableFactory : IEvaluableFactory, new()
    {
        public IObservable<IEvaluable> New(IObservable<IEvaluableFactoryProvider> factoryProvidersSource, IObservable<IEvaluable> dataSource, int stackSize) =>
            new TEvaluableFactory
            {
                FactoryProvidersSource = factoryProvidersSource,
                DataSource = dataSource,
                StackSize = stackSize
            };
    }
}
