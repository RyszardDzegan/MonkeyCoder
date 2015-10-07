namespace MonkeyCoder.Functions.Reactive
{
    internal abstract class TernaryTypeSafeFactory<TTernaryOperation, TParameterA, TParameterB, TParameterC> : TernaryEvaluableFactory<TTernaryOperation>
        where TTernaryOperation : ITernaryEvaluable, new()
    {
        protected override bool AcceptsA(IEvaluable a) =>
            AssignablityHelper<TParameterA>.IsAssignableFrom(a);

        protected override bool AcceptsB(IEvaluable b) =>
            AssignablityHelper<TParameterB>.IsAssignableFrom(b);

        protected override bool AcceptsC(IEvaluable c) =>
            AssignablityHelper<TParameterC>.IsAssignableFrom(c);
    }
}
