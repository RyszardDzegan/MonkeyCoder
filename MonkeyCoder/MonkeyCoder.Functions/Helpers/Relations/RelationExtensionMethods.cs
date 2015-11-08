using MonkeyCoder.Functions.Helpers.Arguments;
using MonkeyCoder.Functions.Helpers.Parameters;
using MonkeyCoder.Functions.Invocations;
using MonkeyCoder.Math;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MonkeyCoder.Functions.Helpers.Relations
{
    /// <summary>
    /// Extension methods aimed to ease the use of classes that implement the <seealso cref="IRelation"/> interface.
    /// </summary>
    internal static class RelationExtensionMethods
    {
        /// <summary>
        /// Creates an enumerable of relations between each given parameter and its assignable arguments
        /// so that each relation consists of a parameter and only those arguments that
        /// could be assigned to that parameter.
        /// </summary>
        /// <param name="parameters">An enumerable of parameters.</param>
        /// <param name="arguments">A collection of all possible arguments from which each parameter will seek for its matching types.</param>
        /// <returns>An enumerable of relations between each given parameter and its assignable arguments.</returns>
        /// <remarks>
        /// First, invoke the <see cref="Relate(IEnumerable{Parameter}, IReadOnlyCollection{Argument})"/> method with
        /// a list of unique parameters to create a necessary relations.
        /// Then invoke <see cref="LeftJoin{T}(IEnumerable{ParameterInfo}, IEnumerable{T})"/> method to
        /// obtain relations for remaining parameters without a need of redundant computing.
        /// </remarks>
        public static IEnumerable<Parameter2ArgumentsRelation> Relate(this IEnumerable<Parameter> parameters, IReadOnlyCollection<Argument> arguments) =>
            from parameter in parameters
            select new Parameter2ArgumentsRelation(parameter, parameter.GetAssignableArguments(arguments));

        /// <summary>
        /// Creates an enumerable of relations between each given parameter and its assignable arguments
        /// so that each relation consists of a parameter and only those arguments that
        /// could be assigned to that parameter.
        /// This overload includes an additional information regarding mandatory argument assignability.
        /// </summary>
        /// <param name="parameters">An enumerable of parameters.</param>
        /// <param name="arguments">A collection of all possible arguments from which each parameter will seek for its matching types.</param>
        /// <param name="mandatoryArgument">A mandatory argument which assignability to each parameter will be checked.</param>
        /// <returns>An enumerable of relations between each given parameter and its assignable arguments.</returns>
        /// <remarks>
        /// First, invoke the <see cref="Relate(IEnumerable{Parameter}, IReadOnlyCollection{Argument})"/> method with
        /// a list of unique parameters to create a necessary relations.
        /// Then invoke <see cref="LeftJoin{T}(IEnumerable{ParameterInfo}, IEnumerable{T})"/> method to
        /// obtain relations for remaining parameters without a need of redundant computing.
        /// </remarks>
        public static IEnumerable<Parameter2MandatoryArgumentsRelation> Relate(this IEnumerable<Parameter> parameters, IReadOnlyCollection<Argument> arguments, BasicArgument mandatoryArgument) =>
            from parameter in parameters
            let innerArguments = parameter.GetAssignableArguments(arguments)
            let isAssignableFromMandatoryArgument = mandatoryArgument.IsAssignableTo(parameter)
            select new Parameter2MandatoryArgumentsRelation(parameter, innerArguments, isAssignableFromMandatoryArgument);

        /// <summary>
        /// Converts an enumerable of <see cref="Parameter2ArgumentsRelation"/> to
        /// an enumerable of <see cref="Parameter2InvocationsRelation"/>.
        /// </summary>
        /// <param name="distinctRelations">Relations between primary function parameters and their assignable arguments.</param>
        /// <param name="possibleArguments">The candidates for arguments for the function.</param>
        /// <param name="currentStackSize">Determines the current level on a call stack.</param>
        /// <returns>An enumerable of <see cref="Parameter2InvocationsRelation"/>.</returns>
        public static IEnumerable<Parameter2InvocationsRelation> ToInvocationsRelations(this IEnumerable<Parameter2ArgumentsRelation> distinctRelations, IReadOnlyCollection<object> possibleArguments, int currentStackSize) =>
            from relation in distinctRelations
            select new Parameter2InvocationsRelation(
                relation.Parameter,
                from argument in relation.Arguments
                from invocation in argument.ToInvocations(possibleArguments, currentStackSize)
                select invocation);

        /// <summary>
        /// This method gets through the <paramref name="parameters"/> and
        /// matches each of them with the corresponding relation.
        /// <see cref="Parameter2ArgumentsRelation"/> has a <see cref="Parameter"/> property so that
        /// it can be matched with a parameter from <paramref name="parameters"/> invocations.
        /// </summary>
        /// <typeparam name="T">A relation.</typeparam>
        /// <param name="parameters">An enumerable of all parameters.</param>
        /// <param name="distinctRelations">An enumerable of relations created upon distinct parameters.</param>
        /// <returns>An enumerable of relations of all parameters.</returns>
        /// <remarks>
        /// First, invoke the <see cref="Relate(IEnumerable{Parameter}, IReadOnlyCollection{Argument})"/> method with
        /// a list of unique parameters to create a necessary relations.
        /// Then invoke <see cref="LeftJoin{T}(IEnumerable{ParameterInfo}, IEnumerable{T})"/> method to
        /// obtain relations for remaining parameters without a need of redundant computing.
        /// </remarks>
        public static IEnumerable<T> LeftJoin<T>(this IEnumerable<ParameterInfo> parameters, IEnumerable<T> distinctRelations)
            where T : IRelation =>
            from parameter in parameters
            join relation in distinctRelations on parameter.ParameterType.FullName equals relation.Parameter.Type.FullName
            select relation;

        /// <summary>
        /// Produces all possible variations of arguments that could be used to invoke the function.
        /// It takes an enumerable of relations that contains the function's
        /// parameters together with their matching evaluables.
        /// Then it produces a cartesian product to obtain each possible input sets.
        /// </summary>
        /// <param name="relations">Relations between parameters and their matching evaluables.</param>
        /// <returns>An enumerable of value enumerables. Each value Invocations can be used as a function's input set.</returns>
        /// <example>
        /// Given the following relations:
        /// R1 int    {1, 2}
        /// R2 string {"a", "b", "c"}
        /// R3 double {5.2, 3.6}
        /// The following invocation lists will be produced:
        /// 1 "a" 5.2
        /// 1 "a" 3.6
        /// 1 "b" 5.2
        /// 1 "b" 3.6
        /// 1 "c" 5.2
        /// 1 "c" 3.6
        /// 2 "a" 5.2
        /// 2 "a" 3.6
        /// 2 "b" 5.2
        /// 2 "b" 3.6
        /// 2 "c" 5.2
        /// 2 "c" 3.6
        /// </example>
        public static IEnumerable<IList<IInvocation>> ProducePossibleArgumentSets(this IEnumerable<Parameter2InvocationsRelation> relations) =>
            from arguments in relations.Select(x => x.Arguments.ToList()).AsCartesianProduct()
            select arguments;
    }
}
