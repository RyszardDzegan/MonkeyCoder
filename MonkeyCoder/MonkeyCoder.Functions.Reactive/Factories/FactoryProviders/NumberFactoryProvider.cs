namespace MonkeyCoder.Functions.Reactive
{
    internal class NumberFactoryProvider<TEvaluableFactory> : GenericFactoryProvider<TEvaluableFactory, INumber>
        where TEvaluableFactory : IEvaluableFactory, new()
    { }
}
