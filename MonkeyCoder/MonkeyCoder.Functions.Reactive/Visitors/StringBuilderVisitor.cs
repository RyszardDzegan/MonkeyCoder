using System.Text;

namespace MonkeyCoder.Functions.Reactive
{
    internal class StringBuilderVisitor : IVisitor
    {
        private StringBuilder StringBuilder { get; } = new StringBuilder();

        public void Visit(Number visitable)
        {
            StringBuilder.Append(visitable.Evaluate());
        }

        public void Visit(Boolean visitable)
        {
            StringBuilder.Append(visitable.Evaluate());
        }

        public void Visit(Sum visitable)
        {
            StringBuilder.Append("(");
            visitable.A.Accept(this);
            StringBuilder.Append("+");
            visitable.B.Accept(this);
            StringBuilder.Append(")");
        }

        public void Visit(Mul visitable)
        {
            StringBuilder.Append("(");
            visitable.A.Accept(this);
            StringBuilder.Append("*");
            visitable.B.Accept(this);
            StringBuilder.Append(")");
        }

        public void Visit(Equ visitable)
        {
            StringBuilder.Append("(");
            visitable.A.Accept(this);
            StringBuilder.Append("==");
            visitable.B.Accept(this);
            StringBuilder.Append(")");
        }

        public override string ToString()
        {
            return StringBuilder.ToString();
        }
    }
}
