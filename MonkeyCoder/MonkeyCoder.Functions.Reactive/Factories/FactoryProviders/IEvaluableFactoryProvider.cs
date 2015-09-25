using System;

namespace MonkeyCoder.Functions.Reactive
{
    internal interface IEvaluableFactoryProvider
    {
        IObservable<IEvaluable> New(IObservable<IEvaluableFactoryProvider> factoriesSource, IObservable<IEvaluable> argumentsSource, int stackSize);
    }
}