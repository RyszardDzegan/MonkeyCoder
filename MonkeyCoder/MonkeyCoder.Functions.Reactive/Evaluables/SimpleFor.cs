using System.Collections.Generic;

namespace MonkeyCoder.Functions.Reactive
{
    internal class SimpleFor : ITernaryEvaluable, INumber
    {
        public IEvaluable A { get; set; }
        public IEvaluable B { get; set; }
        public IEvaluable C { get; set; }
        
        public object Evaluate()
        {
            var aValue = (IValueEvaluable)A;
            var output = new List<dynamic>();

            for (aValue.Value = 0; B.Evaluate(); aValue.Value++)
                output.Add(C.Evaluate());

            return output;
        }

        public void Accept(IVisitor visitor) =>
            visitor.Visit(this);

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
