namespace MonkeyCoder.Functions.Reactive
{
    internal class Variable : ValueEvaluable, IVariable
    {
        public string Name { get; }

        public Variable(string name, object value)
            : base(value)
        {
            Name = name;
        }

        void IVisitable.Accept(IVisitor visitor) =>
            visitor.Visit(this);
    }
}
