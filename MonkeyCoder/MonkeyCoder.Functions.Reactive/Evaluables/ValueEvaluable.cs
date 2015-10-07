namespace MonkeyCoder.Functions.Reactive
{
    internal abstract class ValueEvaluable
    {
        public object Value { get; }

        protected ValueEvaluable(dynamic value)
        {
            Value = value;
        }

        public dynamic Evaluate() =>
            Value;

        public override bool Equals(object obj)
        {
            var other = obj as ValueEvaluable;
            return other == null ? base.Equals(obj) : Value.Equals(other.Value);
        }

        public override int GetHashCode() =>
            Value.GetHashCode();
    }
}
