namespace MonkeyCoder.Functions.Reactive
{
    internal interface IBinaryOperation : INumber
    {
        INumber A { get; }
        INumber B { get; }
    }
}
