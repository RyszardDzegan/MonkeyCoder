using System.Text;

namespace MonkeyCoder.Functions.Reactive
{
    internal class StringBuilderVisitor : IVisitor
    {
        private StringBuilder StringBuilder { get; } = new StringBuilder();

        private void VisitUnary(IUnaryEvaluable visitable, string symbol)
        {
            StringBuilder.Append("(");
            StringBuilder.Append(symbol);
            visitable.A.Accept(this);
            StringBuilder.Append(")");
        }

        private void VisitBinary(IBinaryEvaluable visitable, string symbol)
        {
            StringBuilder.Append("(");
            visitable.A.Accept(this);
            StringBuilder.Append(symbol);
            visitable.B.Accept(this);
            StringBuilder.Append(")");
        }

        public void Visit(Number visitable) =>
            StringBuilder.Append(visitable.Evaluate());

        public void Visit(Boolean visitable) =>
            StringBuilder.Append(visitable.Evaluate());

        public void Visit(Contrariety visitable) =>
            VisitUnary(visitable, "-");

        public void Visit(Sum visitable) =>
            VisitBinary(visitable, "+");

        public void Visit(Multiplication visitable) =>
            VisitBinary(visitable, "*");

        public void Visit(Equality visitable) =>
            VisitBinary(visitable, "=");

        public void Visit(LessThan visitable) =>
            VisitBinary(visitable, "<");

        public void Visit(LogicalAnd visitable) =>
            VisitBinary(visitable, " and ");

        public void Visit(IfElse visitable)
        {
            StringBuilder.Append("(");
            visitable.A.Accept(this);
            StringBuilder.Append("?");
            visitable.B.Accept(this);
            StringBuilder.Append(":");
            visitable.C.Accept(this);
            StringBuilder.Append(")");
        }

        public override string ToString() =>
            StringBuilder.ToString();
    }
}
