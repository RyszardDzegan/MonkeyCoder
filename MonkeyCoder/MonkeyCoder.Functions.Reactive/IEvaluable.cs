namespace MonkeyCoder.Functions.Reactive
{
    internal interface IEvaluable : IVisitable
    {
        object Evaluate();
    }
}
