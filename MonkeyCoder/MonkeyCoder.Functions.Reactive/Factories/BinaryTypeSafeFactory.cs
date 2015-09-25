namespace MonkeyCoder.Functions.Reactive
{
    internal abstract class BinaryTypeSafeFactory<TBinaryOperation, TParameterA, TParameterB> : BinaryEvaluableFactory<TBinaryOperation>.RightSide
        where TBinaryOperation : IBinaryEvaluable, new()
    {
        protected override bool AcceptsA(IEvaluable a) =>
            AssignablityHelper<TParameterA>.IsAssignableFrom(a);

        protected override bool AcceptsB(IEvaluable b) =>
            AssignablityHelper<TParameterB>.IsAssignableFrom(b);
    }
}
