using MonkeyCoder.Functions.Helpers.Arguments;
using MonkeyCoder.Functions.Helpers.Parameters;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MonkeyCoder.Functions.Helpers.Relations
{
    /// <summary>
    /// Associates the <see cref="Parameter"/> with its assignable <see cref="Evaluables"/>.
    /// </summary>
    [DebuggerDisplay("{Parameter.TypeName} <- {EvaluablesString}")]
    internal class Parameter2EvaluablesRelation : IRelation
    {
        /// <summary>
        /// The primary function parameter.
        /// </summary>
        public Parameter Parameter { get; }

        /// <summary>
        /// Evaluables that are assignable to the <see cref="Parameter"/>.
        /// </summary>
        public IEnumerable<IEvaluable> Evaluables { get; }

        /// <summary>
        /// Converts <see cref="Evaluables"/> to <see cref="string"/> so that they can be displayed for debugging purpose.
        /// </summary>
        private string EvaluablesString => string.Join(", ", Evaluables.Select(x => x.Evaluate()));

        /// <summary>
        /// A constructor that takes the <see cref="Parameter"/> and <see cref="Evaluables"/>.
        /// </summary>
        /// <param name="parameter">The <see cref="Parameter"/>.</param>
        /// <param name="evaluables">Evaluables that are assignable to the <see cref="Parameter"/>.</param>
        public Parameter2EvaluablesRelation(Parameter parameter, IEnumerable<IEvaluable> evaluables)
        {
            Parameter = parameter;
            Evaluables = evaluables;
        }
    }
}
