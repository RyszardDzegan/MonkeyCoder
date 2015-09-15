namespace MonkeyCoder.Functions.Reactive
{
    internal class Number : INumber
    {
        public object Value { get; }

        public Number(object value)
        {
            Value = value;
        }

        public object Evaluate()
        {
            return Value;
        }

        void IVisitable.Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
