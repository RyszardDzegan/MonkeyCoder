using System;

namespace MonkeyCoder.Functions.Reactive
{
    internal class LessThanFactory : BinaryTypeSafeFactory<LessThan, INumber, INumber>
    {
        protected override void OnRightOperandNext(IEvaluable current, IEvaluable childNext, IObserver<IEvaluable> observer)
        {
            if (current.Evaluate() < childNext.Evaluate())
                base.OnRightOperandNext(current, childNext, observer);
        }
    }
}
