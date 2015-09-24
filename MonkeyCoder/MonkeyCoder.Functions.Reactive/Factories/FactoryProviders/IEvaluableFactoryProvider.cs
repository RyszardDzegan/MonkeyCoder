using System;

namespace MonkeyCoder.Functions.Reactive
{
    internal interface IEvaluableFactoryProvider
    {
        bool AcceptsExpected(IEvaluable expected);
        IObservable<IEvaluable> New(IObservable<IEvaluableFactoryProvider> factoriesSource, IObservable<IEvaluable> argumentsSource, IEvaluable expected, int stackSize);
    }
}