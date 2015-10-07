using System;

namespace MonkeyCoder.Functions.Reactive
{
    internal class IfElse : TernaryEvaluable, ITernaryEvaluable, INumber
    {
        protected override Func<dynamic, dynamic, dynamic, dynamic> Operation => (a, b, c) => a ? b : c;
        void IVisitable.Accept(IVisitor visitor) => visitor.Visit(this);
        public override bool Equals(object obj) => obj is IfElse && base.Equals(obj);
        public override int GetHashCode() => 43 * base.GetHashCode();
    }
}
