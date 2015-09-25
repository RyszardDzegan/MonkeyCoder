using System;

namespace MonkeyCoder.Functions.Reactive
{
    internal abstract class BinaryEvaluable
    {
        public IEvaluable A { get; set; }
        public IEvaluable B { get; set; }

        protected abstract Func<dynamic, dynamic, dynamic> Operation { get; }

        public object Evaluate()
        {
            dynamic a = A.Evaluate();
            dynamic b = B.Evaluate();

            return Operation(a, b);
        }

        public override bool Equals(object obj)
        {
            var other = obj as BinaryEvaluable;
            return other == null ? base.Equals(obj) : A.Equals(other.A) && B.Equals(other.B);
        }

        public override int GetHashCode()
        {
            var hash = 23;
            hash = hash * 31 + A.Evaluate();
            hash = hash * 31 + B.Evaluate();
            return hash;
        }
    }
}
