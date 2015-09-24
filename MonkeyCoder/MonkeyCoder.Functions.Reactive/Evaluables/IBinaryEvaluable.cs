namespace MonkeyCoder.Functions.Reactive
{
    internal interface IBinaryEvaluable : IEvaluable
    {
        IEvaluable A { get; set; }
        IEvaluable B { get; set; }
    }
}
