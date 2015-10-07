using System;

namespace MonkeyCoder.Functions.Reactive
{
    internal abstract class UnaryEvaluable
    {
        public IEvaluable A { get; set; }

        protected abstract Func<dynamic, dynamic> Operation { get; }

        public object Evaluate()
        {
            dynamic a = A.Evaluate();
            return Operation(a);
        }

        public override bool Equals(object obj)
        {
            var other = obj as BinaryEvaluable;
            return other == null ? base.Equals(obj) : A.Equals(other.A);
        }

        public override int GetHashCode()
        {
            var hash = 23;
            hash = hash * 31 + A.Evaluate();
            return hash;
        }
    }
}
