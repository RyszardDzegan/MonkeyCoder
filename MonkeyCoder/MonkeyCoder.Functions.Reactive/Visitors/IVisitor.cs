namespace MonkeyCoder.Functions.Reactive
{
    internal interface IVisitor
    {
        void Visit(Number visitable);
        void Visit(Boolean visitable);
        void Visit(Sum visitable);
        void Visit(Multiplication visitable);
        void Visit(Equality visitable);
        void Visit(LessThan visitable);
    }
}
