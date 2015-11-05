using MonkeyCoder.Functions.Helpers.Arguments;
using MonkeyCoder.Functions.Helpers.Parameters;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MonkeyCoder.Functions.Helpers.Relations
{
    /// <summary>
    /// Associates the <see cref="Parameter"/> with its assignable <see cref="Arguments"/>.
    /// </summary>
    [DebuggerDisplay("{Parameter.TypeName} <- {ArgumentsString}")]
    internal class Parameter2ArgumentsRelation : IRelation
    {
        /// <summary>
        /// The primary function parameter.
        /// </summary>
        public Parameter Parameter { get; }

        /// <summary>
        /// Arguments that are assignable to the <see cref="Parameter"/>.
        /// </summary>
        public IEnumerable<Argument> Arguments { get; }

        /// <summary>
        /// Converts <see cref="Arguments"/> to <see cref="string"/> so that they can be displayed for debugging purpose.
        /// </summary>
        private string ArgumentsString => string.Join(", ", Arguments.Select(x => x.Value));

        /// <summary>
        /// A constructor that takes the <see cref="Parameter"/> and <see cref="Arguments"/>.
        /// </summary>
        /// <param name="parameter">The <see cref="Parameter"/>.</param>
        /// <param name="arguments">Arguments that are assignable to the <see cref="Parameter"/>.</param>
        internal Parameter2ArgumentsRelation(Parameter parameter, IEnumerable<Argument> arguments)
        {
            Parameter = parameter;
            Arguments = arguments;
        }
    }
}
