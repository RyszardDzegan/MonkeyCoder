using System.Text;

namespace MonkeyCoder.Functions.Reactive
{
    internal class StringBuilderVisitor : IVisitor
    {
        private StringBuilder StringBuilder { get; } = new StringBuilder();

        private void Visit(IBinaryEvaluable visitable, string symbol)
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

        public void Visit(Sum visitable) =>
            Visit(visitable, "+");

        public void Visit(Multiplication visitable) =>
            Visit(visitable, "*");

        public void Visit(Equality visitable) =>
            Visit(visitable, "=");

        public void Visit(LessThan visitable) =>
            Visit(visitable, "<");

        public void Visit(LogicalAnd visitable) =>
            Visit(visitable, " and ");

        public override string ToString() =>
            StringBuilder.ToString();
    }
}
