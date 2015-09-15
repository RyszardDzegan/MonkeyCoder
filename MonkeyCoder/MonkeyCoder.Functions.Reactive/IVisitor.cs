namespace MonkeyCoder.Functions.Reactive
{
    internal interface IVisitor
    {
        void Visit(INumber visitable);
        void Visit(IBinaryOperation visitable);
    }
}
