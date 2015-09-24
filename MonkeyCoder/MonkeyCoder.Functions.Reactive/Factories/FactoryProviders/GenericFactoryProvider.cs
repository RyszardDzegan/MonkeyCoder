namespace MonkeyCoder.Functions.Reactive
{
    internal class GenericFactoryProvider<TEvaluableFactory, TExpected> : DefaultFactoryProvider<TEvaluableFactory>
        where TEvaluableFactory : IEvaluableFactory, new()
        where TExpected : IEvaluable
    {
        public override bool AcceptsExpected(IEvaluable expected) => typeof(TExpected).IsAssignableFrom(expected.GetType());
    }
}
