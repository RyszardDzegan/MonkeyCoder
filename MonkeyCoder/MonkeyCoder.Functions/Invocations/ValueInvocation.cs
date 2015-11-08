namespace MonkeyCoder.Functions.Invocations
{
    /// <summary>
    /// Stores the information about the invocation.
    /// <see cref="Function"/> returns the <see cref="Value"/>.
    /// <see cref="Arguments"/> are always empty.
    /// <see cref="Value"/> contains an instance of a type that is not a function.
    /// </summary>
    public class ValueInvocation : InvocationBase, IInvocation
    {
        protected override object FunctionBody() =>
            Value;

        /// <summary>
        /// The main constructor.
        /// </summary>
        /// <param name="value">A <see cref="OriginalValue"/>.</param>
        public ValueInvocation(object value)
            : base(value)
        { }

        /// <summary>
        /// A visitor that can be used to inspect subclasses of <see cref="IInvocation"/>.
        /// </summary>
        public void Accept(IInvocationVisitor visitor) =>
            visitor.Visit(this);
    }
}
