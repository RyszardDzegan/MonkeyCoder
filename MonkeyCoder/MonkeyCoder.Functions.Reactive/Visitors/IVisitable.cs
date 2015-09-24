namespace MonkeyCoder.Functions.Reactive
{
    internal interface IVisitable
    {
        void Accept(IVisitor visitor);
    }
}
