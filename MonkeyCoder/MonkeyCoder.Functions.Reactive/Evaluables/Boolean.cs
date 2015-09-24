namespace MonkeyCoder.Functions.Reactive
{
    internal class Boolean : IBoolean
    {
        public dynamic Value { get; }

        public Boolean(dynamic value)
        {
            Value = value;
        }

        public dynamic Evaluate() => Value;
        void IVisitable.Accept(IVisitor visitor) => visitor.Visit(this);
    }
}
