namespace MonkeyCoder.Functions.Reactive
{
    internal abstract class UnaryTypeSafeFactory<TUnaryOperation, TParameterA> : UnaryEvaluableFactory<TUnaryOperation>
        where TUnaryOperation : IUnaryEvaluable, new()
    {
        protected override bool AcceptsA(IEvaluable a) =>
            AssignablityHelper<TParameterA>.IsAssignableFrom(a);
    }
}
