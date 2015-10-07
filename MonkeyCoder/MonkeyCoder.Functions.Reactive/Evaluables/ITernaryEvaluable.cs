namespace MonkeyCoder.Functions.Reactive
{
    internal interface ITernaryEvaluable : IEvaluable
    {
        IEvaluable A { get; set; }
        IEvaluable B { get; set; }
        IEvaluable C { get; set; }
    }
}
