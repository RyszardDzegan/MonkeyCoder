using System;

namespace MonkeyCoder.Functions.Reactive
{
    internal class EqualityFactory : BinaryTypeSafeFactory<Equality, INumber, INumber>
    {
        protected override void OnOperandsReady(IEvaluable a, IEvaluable b, IObserver<IEvaluable> observer)
        {
            if (!a.Equals(b) && a.Evaluate().Equals(b.Evaluate()))
                base.OnOperandsReady(a, b, observer);
        }
    }
}
