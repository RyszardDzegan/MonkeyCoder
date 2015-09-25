using System;

namespace MonkeyCoder.Functions.Reactive
{
    internal class Multiplication : BinaryEvaluable, IBinaryEvaluable, INumber
    {
        protected override Func<dynamic, dynamic, dynamic> Operation => (x, y) => x * y;
        void IVisitable.Accept(IVisitor visitor) => visitor.Visit(this);
        public override bool Equals(object obj) => obj is Multiplication && base.Equals(obj);
        public override int GetHashCode() => 51 * base.GetHashCode();
    }
}
