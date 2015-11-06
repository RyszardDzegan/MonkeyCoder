using MonkeyCoder.Functions.Helpers.Arguments;
using MonkeyCoder.Functions.Helpers.Parameters;
using MonkeyCoder.Math;
using System;
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
        /// an enumerable of <see cref="Parameter2EvaluablesRelation"/>.
        /// It doesn't support the <see cref="FunctionArgument"/>.
        /// </summary>
        /// <param name="distinctRelations">Relations between primary function parameters and their assignable arguments.</param>
        /// <returns>Invocations of <see cref="Parameter2EvaluablesRelation"/>.</returns>
        /// <exception cref="InvalidCastException">When encountered argument is of type <see cref="FunctionArgument"/> which doesn't implement <see cref="IEvaluable"/>.</exception>
        public static IEnumerable<Parameter2EvaluablesRelation> ToEvaluablesRelations(this IEnumerable<Parameter2ArgumentsRelation> distinctRelations) =>
            from relation in distinctRelations
            let completedEvaluables =
                 from argument in relation.Arguments
                 select (IEvaluable)argument
            select new Parameter2EvaluablesRelation(relation.Parameter, completedEvaluables);

        /// <summary>
        /// Converts an enumerable of <see cref="Parameter2ArgumentsRelation"/> to
        /// an enumerable of <see cref="Parameter2EvaluablesRelation"/>.
        /// It does support the <see cref="FunctionArgument"/>.
        /// </summary>
        /// <param name="distinctRelations">Relations between primary function parameters and their assignable arguments.</param>
        /// <param name="arguments2EvaluablesMap">An enumerable of argument - evaluable pairs that determine what evaluables each argument has.</param>
        /// <returns>An enumerable of <see cref="Parameter2EvaluablesRelation"/>.</returns>
        public static IEnumerable<Parameter2EvaluablesRelation> ToEvaluablesRelations(this IEnumerable<Parameter2ArgumentsRelation> distinctRelations, IEnumerable<Tuple<Argument, IEvaluable>> arguments2EvaluablesMap) =>
            from relation in distinctRelations
            let completedEvaluables =
                 from argument in relation.Arguments
                 join argumentAndEvaluable in arguments2EvaluablesMap on argument equals argumentAndEvaluable.Item1 into jointSpawnedArguments
                 let evaluables =
                    jointSpawnedArguments == null ?
                    Enumerable.Repeat((IEvaluable)argument, 1) :
                    from jointSpawnedArgument in jointSpawnedArguments
                    select jointSpawnedArgument.Item2
                 from evaluable in evaluables
                 select evaluable
            select new Parameter2EvaluablesRelation(relation.Parameter, completedEvaluables);

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
        public static IEnumerable<IList<IEvaluable>> ProduceInvocationsValues(this IEnumerable<Parameter2EvaluablesRelation> relations) =>
            from evaluables in relations.Select(x => x.Evaluables.ToList()).AsCartesianProduct()
            select evaluables;

        /// <summary>
        /// Produces an enumerable of functions where each of which invokes
        /// the <paramref name="function"/> giving it an enumerable of input arguments
        /// taken consecutively from <paramref name="valueEnumerables"/>.
        /// </summary>
        /// <param name="function">The function that will be invoked with arguments taken from <paramref name="valueEnumerables"/>.</param>
        /// <param name="valueEnumerables">An enumerable of arguments arrays that will be used to invoke the <paramref name="function"/>.</param>
        /// <returns>
        /// An enumerable of functions where each of which invokes
        /// the <paramref name="function"/> giving it an enumerable of input arguments
        /// taken consecutively from <paramref name="valueEnumerables"/>.
        /// </returns>
        public static IEnumerable<Func<object>> ProduceInvocations(this Delegate function, IEnumerable<IEnumerable<object>> valueEnumerables) =>
            from valueEnumerable in valueEnumerables
            select new Func<object>(() => function.DynamicInvoke(valueEnumerable.ToArray()));
    }
}
