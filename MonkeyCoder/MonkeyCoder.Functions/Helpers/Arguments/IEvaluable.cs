namespace MonkeyCoder.Functions.Helpers.Arguments
{
    /// <summary>
    /// An interface that is required to treat uniformly
    /// simple argument values as well as delegates.
    /// </summary>
    internal interface IEvaluable
    {
        /// <summary>
        /// Returns argument's value at runtime.
        /// </summary>
        /// <returns>Argument's value.</returns>
        object Evaluate();
    }
}