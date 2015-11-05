using MonkeyCoder.Functions.Helpers.Arguments;
using MonkeyCoder.Functions.Helpers.Parameters;
using System.Collections.Generic;

namespace MonkeyCoder.Functions.Helpers.Relations
{
    /// <summary>
    /// Associates the <see cref="Parameter"/> with its assignable <see cref="Arguments"/>.
    /// It also stores an information whether the mandatory argument is assignable to the parameter.
    /// </summary>
    internal class Parameter2MandatoryArgumentsRelation : Parameter2ArgumentsRelation
    {
        /// <summary>
        /// Determines whether the mandatory argument is assignable to the parameter.
        /// </summary>
        public bool IsAssignableFromMandatoryArgument { get; }

        /// <summary>
        /// A constructor that populates internal properties.
        /// </summary>
        /// <param name="parameter">The function parameter.</param>
        /// <param name="arguments">Arguments that can be assigned to the <paramref name="parameter"/>.</param>
        /// <param name="isAssignableFromMandatoryArgument">Indicates whether the mandatory argument is assignable to the <paramref name="parameter"/>.</param>
        internal Parameter2MandatoryArgumentsRelation(Parameter parameter, IEnumerable<Argument> arguments, bool isAssignableFromMandatoryArgument)
                : base(parameter, arguments)
        {
            IsAssignableFromMandatoryArgument = isAssignableFromMandatoryArgument;
        }
    }
}
