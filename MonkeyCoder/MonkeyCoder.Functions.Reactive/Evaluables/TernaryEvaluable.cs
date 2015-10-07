using System;

namespace MonkeyCoder.Functions.Reactive
{
    internal abstract class TernaryEvaluable
    {
        public IEvaluable A { get; set; }
        public IEvaluable B { get; set; }
        public IEvaluable C { get; set; }

        protected abstract Func<dynamic, dynamic, dynamic, dynamic> Operation { get; }

        public object Evaluate()
        {
            dynamic a = A.Evaluate();
            dynamic b = B.Evaluate();
            dynamic c = C.Evaluate();

            return Operation(a, b, c);
        }

        public override bool Equals(object obj)
        {
            var other = obj as TernaryEvaluable;
            return other == null ? base.Equals(obj) : A.Equals(other.A) && B.Equals(other.B) && C.Equals(other.C);
        }

        public override int GetHashCode()
        {
            var hash = 23;
            hash = hash * 31 + A.Evaluate();
            hash = hash * 31 + B.Evaluate();
            hash = hash * 31 + C.Evaluate();
            return hash;
        }
    }
}
