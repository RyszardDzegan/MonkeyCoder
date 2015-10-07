using System;

namespace MonkeyCoder.Functions.Reactive
{
    internal abstract partial class BinaryEvaluableFactory<TBinaryOperation> : BaseEvaluableFactory
        where TBinaryOperation : IBinaryEvaluable, new()
    {
        protected virtual bool AcceptsA(IEvaluable a) =>
            true;

        protected virtual bool AcceptsB(IEvaluable b) =>
            true;

        protected virtual void OnRightOperandNext(IEvaluable a, IEvaluable b, IObserver<IEvaluable> observer) =>
            observer.OnNext(new TBinaryOperation { A = a, B = b });
    }
}
