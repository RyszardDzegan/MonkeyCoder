namespace MonkeyCoder.Functions.Invocations
{
    /// <summary>
    /// A visitor that can be used to inspect subclasses of <see cref="IInvocation"/>.
    /// </summary>
    public interface IInvocationVisitor
    {
        /// <summary>
        /// Inspects the <see cref="FunctionInvocation"/>.
        /// </summary>
        /// <param name="invocation">A <see cref="FunctionInvocation"/>.</param>
        void Visit(FunctionInvocation invocation);

        /// <summary>
        /// Inspects the <see cref="ParameterlessInvocation"/>.
        /// </summary>
        /// <param name="invocation">A <see cref="ParameterlessInvocation"/>.</param>
        void Visit(ParameterlessInvocation invocation);

        /// <summary>
        /// Inspects the <see cref="ValueInvocation"/>.
        /// </summary>
        /// <param name="invocation">A <see cref="ValueInvocation"/>.</param>
        void Visit(ValueInvocation invocation);
    }
}
