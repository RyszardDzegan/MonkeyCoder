using System;

namespace MonkeyCoder.Functions.Reactive
{
    internal class EqualityFactory : BinaryTypeSafeFactory<Equality, INumber, INumber>
    {
        protected override void OnRightOperandNext(IEvaluable a, IEvaluable b, IObserver<IEvaluable> observer)
        {
            if (!a.Equals(b) && a.Evaluate().Equals(b.Evaluate()))
                base.OnRightOperandNext(a, b, observer);
        }
    }
}
