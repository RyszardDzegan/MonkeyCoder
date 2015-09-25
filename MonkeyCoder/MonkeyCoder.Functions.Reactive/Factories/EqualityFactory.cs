using System;

namespace MonkeyCoder.Functions.Reactive
{
    internal class EqualityFactory : BinaryTypeSafeFactory<Equality, INumber, INumber>
    {
        protected override void OnRightOperandNext(IEvaluable current, IEvaluable childNext, IObserver<IEvaluable> observer)
        {
            if (current.Evaluate().Equals(childNext.Evaluate()))
                base.OnRightOperandNext(current, childNext, observer);
        }
    }
}
