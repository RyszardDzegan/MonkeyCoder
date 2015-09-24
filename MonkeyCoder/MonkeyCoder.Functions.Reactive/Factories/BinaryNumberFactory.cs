namespace MonkeyCoder.Functions.Reactive
{
    internal abstract class BinaryNumberFactory<TBinaryOperation> : BinaryEvaluableFactory<TBinaryOperation>
        where TBinaryOperation : IBinaryEvaluable, new()
    {
        protected override bool AcceptsA(IEvaluable a) => NumberHelper.IsAssignableFrom(a);
        protected override bool AcceptsB(IEvaluable b) => NumberHelper.IsAssignableFrom(b);
    }
}
