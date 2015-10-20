namespace MonkeyCoder.Functions.Reactive
{
    internal interface IValueEvaluable : IEvaluable
    {
        dynamic Value { get; set; }
    }
}
