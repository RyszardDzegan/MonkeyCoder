using System;
using System.Reactive.Linq;
using System.Collections.Generic;

namespace MonkeyCoder.Functions.Reactive
{
    internal class SumEvaluableFactory : IEvaluableFactory
    {
        public IObservable<IEvaluable> Subscribe(IObservable<IEvaluable> evaluables)
        {
            return Observable.Create<IEvaluable>(o =>
            {
                var arguments = new List<INumber>();

                return evaluables.Subscribe(e =>
                {
                    var number = e as INumber;

                    if (number == null)
                        return;

                    int count;

                    lock (arguments)
                    {
                        arguments.Add(number);
                        count = arguments.Count;
                    }

                    for (var i = 0; i < count; i++)
                        o.OnNext(new SumEvaluable(arguments[i], number));
                }, () => o.OnCompleted());
            });
        }
    }
}
