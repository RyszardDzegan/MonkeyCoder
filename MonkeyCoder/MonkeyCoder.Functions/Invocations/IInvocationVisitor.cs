namespace MonkeyCoder.Functions.Invocations
{
    /// <summary>
    /// A visitor that can be used to inspect subclasses of <see cref="IInvocation"/>.
    /// </summary>
    public interface IInvocationVisitor
    {
        /// <summary>
        /// Inspects the <see cref="DelegateInvocation"/>.
        /// </summary>
        /// <param name="invocation">A <see cref="DelegateInvocation"/>.</param>
        void Visit(DelegateInvocation invocation);

        /// <summary>
        /// Inspects the <see cref="ValueInvocation"/>.
        /// </summary>
        /// <param name="invocation">A <see cref="ValueInvocation"/>.</param>
        void Visit(ValueInvocation invocation);
    }
}
