using System;

namespace MonkeyCoder.Functions.Reactive
{
    internal class LessThanFactory : BinaryTypeSafeFactory<LessThan, INumber, INumber>
    {
        protected override void OnRightOperandNext(IEvaluable a, IEvaluable b, IObserver<IEvaluable> observer)
        {
            if (!a.Equals(b) && a.Evaluate() < b.Evaluate())
                base.OnRightOperandNext(a, b, observer);
        }
    }
}
