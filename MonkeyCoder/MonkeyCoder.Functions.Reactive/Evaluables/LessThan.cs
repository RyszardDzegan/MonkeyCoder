using System;

namespace MonkeyCoder.Functions.Reactive
{
    internal class LessThan : BinaryEvaluable, IBinaryEvaluable, IBoolean
    {
        protected override Func<dynamic, dynamic, dynamic> Operation => (x, y) => x < y;
        void IVisitable.Accept(IVisitor visitor) => visitor.Visit(this);
        public override bool Equals(object obj) => obj is LessThan && base.Equals(obj);
        public override int GetHashCode() => 43 * base.GetHashCode();
    }
}
