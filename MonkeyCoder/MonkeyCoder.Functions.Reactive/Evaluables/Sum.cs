using System;

namespace MonkeyCoder.Functions.Reactive
{
    internal class Sum : BinaryEvaluable, IBinaryEvaluable, INumber
    {
        protected override Func<dynamic, dynamic, dynamic> Operation => (x, y) => x + y;
        void IVisitable.Accept(IVisitor visitor) => visitor.Visit(this);
        public override bool Equals(object obj) => obj is Sum && base.Equals(obj);
        public override int GetHashCode() => 53 * base.GetHashCode();
    }
}
