using System;

namespace MonkeyCoder.Functions.Reactive
{
    internal class Contrariety : UnaryEvaluable, IUnaryEvaluable, INumber
    {
        protected override Func<dynamic, dynamic> Operation => x => -x;
        void IVisitable.Accept(IVisitor visitor) => visitor.Visit(this);
        public override bool Equals(object obj) => obj is Contrariety && base.Equals(obj);
        public override int GetHashCode() => 53 * base.GetHashCode();
    }
}
