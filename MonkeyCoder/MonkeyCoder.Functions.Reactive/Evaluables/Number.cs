namespace MonkeyCoder.Functions.Reactive
{
    internal class Number : ValueEvaluable, INumber
    {
        public Number(object value)
            : base(value)
        { }

        void IVisitable.Accept(IVisitor visitor) =>
            visitor.Visit(this);
    }
}
