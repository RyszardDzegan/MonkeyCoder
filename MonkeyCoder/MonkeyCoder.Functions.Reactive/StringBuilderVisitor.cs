using System.Text;

namespace MonkeyCoder.Functions.Reactive
{
    internal class StringBuilderVisitor : IVisitor
    {
        private StringBuilder StringBuilder { get; } = new StringBuilder();

        public void Visit(IBinaryOperation visitable)
        {
            StringBuilder.Append("(");
            visitable.A.Accept(this);
            StringBuilder.Append("+");
            visitable.B.Accept(this);
            StringBuilder.Append(")");
        }

        public void Visit(INumber visitable)
        {
            StringBuilder.Append(visitable.Evaluate());
        }

        public override string ToString()
        {
            return StringBuilder.ToString();
        }
    }
}
