namespace MonkeyCoder.Functions.Reactive
{
    internal interface IUnaryEvaluable : IEvaluable
    {
        IEvaluable A { get; set; }
    }
}
