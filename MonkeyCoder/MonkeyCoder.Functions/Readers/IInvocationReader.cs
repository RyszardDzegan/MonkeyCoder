namespace MonkeyCoder.Functions.Readers
{
    /// <summary>
    /// Allows to travel accross the entire invocation tree and
    /// take an adventage of contained informations.
    /// </summary>
    public interface IInvocationReader
    {
        /// <summary>
        /// When overrided in derived class, allows to check the invocation type and its arguments.
        /// </summary>
        /// <param name="invocation">A single result from a function manager.</param>
        void Visit(IInvocation invocation);
    }
}
