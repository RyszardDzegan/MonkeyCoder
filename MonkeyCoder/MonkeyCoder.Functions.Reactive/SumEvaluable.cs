namespace MonkeyCoder.Functions.Reactive
{
    internal class SumEvaluable : IBinaryOperation
    {
        public INumber A { get; }
        public INumber B { get; }

        public SumEvaluable(INumber a, INumber b)
        {
            A = a;
            B = b;
        }

        public object Evaluate()
        {
            dynamic a = A.Evaluate();
            dynamic b = B.Evaluate();

            return a + b;
        }

        void IVisitable.Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
