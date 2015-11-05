using MonkeyCoder.Functions.Helpers.Parameters;

namespace MonkeyCoder.Functions.Helpers.Relations
{
    /// <summary>
    /// An interface implemented by all classes that define relation between
    /// function parameters and other classes like arguments or evaluables.
    /// </summary>
    /// <remarks>
    /// This interface is required by <seealso cref="RelationExtensionMethods.LeftJoin{T}(System.Collections.Generic.IEnumerable{System.Reflection.ParameterInfo}, System.Collections.Generic.IEnumerable{T})"/>
    /// to treat uniformly all classes that implement it.
    /// </remarks>
    internal interface IRelation
    {
        /// <summary>
        /// The primary function parameter.
        /// </summary>
        Parameter Parameter { get; }
    }
}
