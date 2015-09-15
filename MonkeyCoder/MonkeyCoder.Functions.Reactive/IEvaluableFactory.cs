using System;

namespace MonkeyCoder.Functions.Reactive
{
    internal interface IEvaluableFactory
    {
        IObservable<IEvaluable> Subscribe(IObservable<IEvaluable> evaluables);
    }
}
