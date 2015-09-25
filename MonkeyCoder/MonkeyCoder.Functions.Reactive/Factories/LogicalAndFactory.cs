using System;

namespace MonkeyCoder.Functions.Reactive
{
    internal class LogicalAndFactory : BinaryEvaluableFactory<LogicalAnd>.BothSides
    {
        protected override bool AcceptsA(IEvaluable a) =>
            AssignablityHelper<IBoolean>.IsAssignableFrom(a);

        protected override bool AcceptsB(IEvaluable b) =>
            AssignablityHelper<IBoolean>.IsAssignableFrom(b);

        protected override void OnRightOperandNext(IEvaluable a, IEvaluable b, IObserver<IEvaluable> observer)
        {
            if (!a.Equals(b) && a.Evaluate() && b.Evaluate())
                base.OnRightOperandNext(a, b, observer);
        }
    }
}
