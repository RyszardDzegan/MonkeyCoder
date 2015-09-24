using System;

namespace MonkeyCoder.Functions.Reactive
{
    internal interface IEvaluableFactory : IObservable<IEvaluable>
    {
        IObservable<IEvaluableFactoryProvider> FactoryProvidersSource { get; set; }
        IObservable<IEvaluable> DataSource { get; set; }
        IEvaluable Expected { get; set; }
        int StackSize { get; set; }
    }
}
