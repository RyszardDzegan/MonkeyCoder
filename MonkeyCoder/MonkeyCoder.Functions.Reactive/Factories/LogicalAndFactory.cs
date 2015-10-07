using System;

namespace MonkeyCoder.Functions.Reactive
{
    internal class LogicalAndFactory : BinaryEvaluableFactory<LogicalAnd>.BothSides
    {
        protected override bool AcceptsA(IEvaluable a) =>
            AssignablityHelper<IBoolean>.IsAssignableFrom(a);

        protected override bool AcceptsB(IEvaluable b) =>
            AssignablityHelper<IBoolean>.IsAssignableFrom(b);

        protected override void OnOperandsReady(IEvaluable a, IEvaluable b, IObserver<IEvaluable> observer)
        {
            if (!a.Equals(b) && a.Evaluate() && b.Evaluate())
                base.OnOperandsReady(a, b, observer);
        }
    }
}
