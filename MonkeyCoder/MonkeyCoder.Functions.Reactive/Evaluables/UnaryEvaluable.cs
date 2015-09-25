namespace MonkeyCoder.Functions.Reactive
{
    internal abstract class UnaryEvaluable
    {
        public object Value { get; }

        protected UnaryEvaluable(dynamic value)
        {
            Value = value;
        }

        public dynamic Evaluate() =>
            Value;

        public override bool Equals(object obj)
        {
            var other = obj as UnaryEvaluable;
            return other == null ? base.Equals(obj) : Value.Equals(other.Value);
        }

        public override int GetHashCode() =>
            Value.GetHashCode();
    }
}
