using System;

namespace MonkeyCoder.Functions.Reactive
{
    internal class LessThanFactory : BinaryNumberFactory<LessThan>
    {
        protected override void OnChildNext(IEvaluable current, IEvaluable childNext, IObserver<IEvaluable> observer)
        {
            if (current.Evaluate() < childNext.Evaluate())
                base.OnChildNext(current, childNext, observer);
        }
    }
}
