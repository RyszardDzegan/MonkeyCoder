namespace MonkeyCoder.Functions.Reactive
{
    internal class Boolean : ValueEvaluable, IBoolean
    {
        public Boolean(object value)
            : base(value)
        { }

        void IVisitable.Accept(IVisitor visitor) =>
            visitor.Visit(this);
    }
}
