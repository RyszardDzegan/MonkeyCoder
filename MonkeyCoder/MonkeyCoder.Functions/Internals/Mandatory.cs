using MonkeyCoder.Functions.Helpers.Arguments;
using MonkeyCoder.Functions.Helpers.Invocations;
using MonkeyCoder.Functions.Helpers.Parameters;
using MonkeyCoder.Functions.Helpers.Relations;
using MonkeyCoder.Math;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MonkeyCoder.Functions.Internals
{
    /// <summary>
    /// The <see cref="Mandatory"/> class has its core functionality the same as the <see cref="Basic"/> class.
    /// That is, it takes a <see cref="Delegate"/> and a collection of its input arguments candidates.
    /// Argument can be null, doesn't have to be unique and an argument that is a function will be treated
    /// just as a value of a delegate type. It means that a function won't be evaluated at runtime to match its
    /// return value to the main function parameter.
    /// As opose to the <see cref="Basic"/> class, the <see cref="Mandatory"/> class takes an additional
    /// argument candidate to its main constructor.
    /// That additional argument is mandatory and will be always used at least once in every function invocation.
    /// If the function doesn't include any matching parameter, then no invocation will be produced.
    /// </summary>
    internal class Mandatory : IEnumerable<IInvocation>
    {
        /// <summary>
        /// All possible function invocations.
        /// Each invocation contains at least one mandatory argument in its input values.
        /// </summary>
        private Lazy<InvocationsEnumerable> Invocations { get; }

        /// <summary>
        /// The constructor that takes a function, a collection of argument candidates and a mandatory argument.
        /// </summary>
        /// <param name="function">The function for which an invocation list is to be created.</param>
        /// <param name="possibleArguments">The candidates for arguments for the function.</param>
        /// <param name="mandatoryArgument">The mandatory argument for the function. It can be null.</param>
        /// <exception cref="ArgumentNullException">When <paramref name="function"/> is null.</exception>
        /// <exception cref="ArgumentNullException">When <paramref name="possibleArguments"/> are null.</exception>
        public Mandatory(Delegate function, IReadOnlyCollection<object> possibleArguments, object mandatoryArgument)
        {
            if (function == null)
                throw new ArgumentNullException(nameof(function), "FunctionArgument cannot be null.");

            if (possibleArguments == null)
                throw new ArgumentNullException(nameof(possibleArguments), "Possible arguments cannot be null.");

            var parameters = function.Method.GetParameters();
            var mandatory = mandatoryArgument.ToBasicArgument();
            var argumentList = possibleArguments.ToBasicArguments().ToList();
            var distinctParameters = parameters.GetDistinct();
            var distinctRelations = distinctParameters.Relate(argumentList, mandatory);
            var relationList = parameters.LeftJoin(distinctRelations).ToList();

            var optionalPositions = relationList
                .Select((x, i) => new { Relation = x, Index = i })
                .ToList();

            var mandatoryPositions = relationList
                .Select((x, i) => new { Relation = x, Index = i })
                .Where(x => x.Relation.IsAssignableFromMandatoryArgument)
                .ToList();

            var basicMandatoryArgument = new BasicArgument(mandatoryArgument);
            var mandatoryArgumentList = new IEvaluable[] { basicMandatoryArgument };

            var valueEnumerables =
                from mandatoryPositionsSubset in mandatoryPositions.AsPowerSet().Skip(1)
                let possibleValues =
                    from optionalPosition in optionalPositions
                    join mandatoryPosition in mandatoryPositionsSubset on optionalPosition equals mandatoryPosition into jointMandatoryPositions
                    let possibleValues = jointMandatoryPositions.Any() ? mandatoryArgumentList : optionalPosition.Relation.Arguments.Cast<IEvaluable>().ToArray()
                    select possibleValues
                from valueList in possibleValues.AsCartesianProduct()
                select valueList;

            Invocations = InvocationsEnumerable.Lazy(function, valueEnumerables);
        }

        public IEnumerator<IInvocation> GetEnumerator() => Invocations.Value.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
