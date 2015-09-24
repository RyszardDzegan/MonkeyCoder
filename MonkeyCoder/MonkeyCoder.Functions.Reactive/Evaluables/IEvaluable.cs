namespace MonkeyCoder.Functions.Reactive
{
    internal interface IEvaluable : IVisitable
    {
        dynamic Evaluate();
    }
}
