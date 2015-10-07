using System;

namespace MonkeyCoder.Functions.Reactive
{
    internal class LessThanFactory : BinaryTypeSafeFactory<LessThan, INumber, INumber>
    {
        protected override void OnOperandsReady(IEvaluable a, IEvaluable b, IObserver<IEvaluable> observer)
        {
            if (!a.Equals(b) && a.Evaluate() < b.Evaluate())
                base.OnOperandsReady(a, b, observer);
        }
    }
}
