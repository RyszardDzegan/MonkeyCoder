namespace MonkeyCoder.Functions.Reactive
{
    internal class Number : INumber
    {
        public dynamic Value { get; }

        public Number(dynamic value)
        {
            Value = value;
        }

        public dynamic Evaluate() => Value;
        void IVisitable.Accept(IVisitor visitor) => visitor.Visit(this);
    }
}
