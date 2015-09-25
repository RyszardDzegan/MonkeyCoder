using System;

namespace MonkeyCoder.Functions.Reactive
{
    internal class LogicalAndFactory : BinaryEvaluableFactory<LogicalAnd>.BothSides
    {
        protected override bool AcceptsA(IEvaluable a) =>
            AssignablityHelper<IBoolean>.IsAssignableFrom(a);

        protected override bool AcceptsB(IEvaluable b) =>
            AssignablityHelper<IBoolean>.IsAssignableFrom(b);

        protected override void OnRightOperandNext(IEvaluable current, IEvaluable childNext, IObserver<IEvaluable> observer)
        {
            if (current.Evaluate() && childNext.Evaluate())
                base.OnRightOperandNext(current, childNext, observer);
        }
    }
}
