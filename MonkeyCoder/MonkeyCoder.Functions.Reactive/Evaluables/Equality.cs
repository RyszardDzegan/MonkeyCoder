using System;

namespace MonkeyCoder.Functions.Reactive
{
    internal class Equality : BinaryEvaluable, IBinaryEvaluable, IBoolean
    {
        protected override Func<dynamic, dynamic, dynamic> Operation => (x, y) => x.Equals(y);
        void IVisitable.Accept(IVisitor visitor) => visitor.Visit(this);
        public override bool Equals(object obj) => obj is Equality && base.Equals(obj);
        public override int GetHashCode() => 41 * base.GetHashCode();
    }
}
