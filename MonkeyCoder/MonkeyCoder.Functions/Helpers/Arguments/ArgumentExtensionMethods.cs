using MonkeyCoder.Functions.Internals;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MonkeyCoder.Functions.Helpers.Arguments
{
    /// <summary>
    /// Extension methods aimed to ease the use of <seealso cref="Argument"/> class.
    /// </summary>
    internal static class ArgumentExtensionMethods
    {
        /// <summary>
        /// Creates an instance of <see cref="BasicArgument"/> argument that
        /// considers only direct arguments types and
        /// doesn't check whether they're functions which
        /// can have a matching return type.
        /// </summary>
        /// <param name="possibleArgument">Argument valus.</param>
        /// <returns>An instance of <see cref="BasicArgument"/> argument.</returns>
        public static BasicArgument ToBasicArgument(this object possibleArgument) =>
            new BasicArgument(possibleArgument);

        /// <summary>
        /// Creates instances of <see cref="BasicArgument"/> arguments that
        /// considers only direct arguments types and
        /// doesn't check whether they're functions which
        /// can have a matching return type.
        /// </summary>
        /// <param name="possibleArguments">Arguments values.</param>
        /// <returns>Instances of <see cref="BasicArgument"/> arguments.</returns>
        public static IEnumerable<BasicArgument> ToBasicArguments(this IEnumerable<object> possibleArguments) =>
            from possibleArgument in possibleArguments
            let type = possibleArgument?.GetType()
            select new BasicArgument(possibleArgument, type);

        /// <summary>
        /// Returns an enumerable of arguments.
        /// Their type will be determined basing on arguments values and types
        /// and can be <see cref="BasicArgument"/> or <see cref="ParameterlessArgument"/>.
        /// Notice that <see cref="BasicArgument"/> instance will always be created
        /// because both a function itself and its return type can be consider as an input argument.
        /// </summary>
        /// <param name="possibleArguments">Arguments values upon which arguments instances will be created.</param>
        /// <returns>An enumerable of arguments instances.</returns>
        public static IEnumerable<Argument> ToParameterlessFunctionArguments(this IEnumerable<object> possibleArguments) =>
            from possibleArgument in possibleArguments
            let method = (possibleArgument as Delegate)?.Method
            let basic = new BasicArgument(possibleArgument, possibleArgument?.GetType())
            let parameterless = method != null && !method.GetParameters().Any() ? new ParameterlessArgument(possibleArgument, method.ReturnType) : null
            let arguments = new Argument[] { basic, parameterless }
            from argument in arguments
            where argument != null
            select argument;

        /// <summary>
        /// Returns an enumerable of arguments.
        /// Their type will be determined basing on arguments values and types
        /// and can be <see cref="BasicArgument"/>, <see cref="ParameterlessArgument"/> or <see cref="FunctionArgument"/>.
        /// Notice that <see cref="BasicArgument"/> instance will always be created
        /// because both a function itself and its return type can be consider as an input argument.
        /// </summary>
        /// <param name="possibleArguments">Arguments values upon which arguments instances will be created.</param>
        /// <returns>An enumerable of arguments instances.</returns>
        public static IEnumerable<Argument> ToFunctionArguments(this IEnumerable<object> possibleArguments) =>
            from possibleArgument in possibleArguments
            let method = (possibleArgument as Delegate)?.Method
            let basic = new BasicArgument(possibleArgument, possibleArgument?.GetType())
            let function = method != null && method.GetParameters().Any() ? new FunctionArgument(possibleArgument, method.ReturnType) : null
            let parameterless = method != null && !method.GetParameters().Any() ? new ParameterlessArgument(possibleArgument, method.ReturnType) : null
            let arguments = new Argument[] { basic, function, parameterless }
            from argument in arguments
            where argument != null
            select argument;

        /// <summary>
        /// Converts each <see cref="Func{TResult}"/> to <see cref="FunctionEvaluable"/>.
        /// </summary>
        /// <param name="functions">Functions to be converted.</param>
        /// <returns>An enumerable of <see cref="FunctionEvaluable"/>.</returns>
        public static IEnumerable<IEvaluable> ToFunctionEvaluables(this IEnumerable<IInvocation> invocationInfos) =>
            from invocationInfo in invocationInfos
            select new FunctionEvaluable(invocationInfo);

        /// <summary>
        /// This method maps <paramref name="arguments"/> to <see cref="IEvaluable"/>s.
        /// The <see cref="BasicArgument"/> and <see cref="ParameterlessArgument"/> arguments
        /// implements <see cref="IEvaluable"/> because they can be evaluated without any additional information.
        /// The <see cref="FunctionEvaluable"/> doesn't implement <see cref="IEvaluable"/> because it represents
        /// a function that takes parameters and it requires the information regarding possible arguments.
        /// Given the above, the <see cref="BasicArgument"/> and <see cref="ParameterlessArgument"/> arguments
        /// are mapped to a single <see cref="IEvaluable"/> that they implement.
        /// The <see cref="FunctionEvaluable"/> is used to create an instance of <see cref="Expanding"/> function manager.
        /// The <see cref="Expanding"/> will generate function invocations for each <see cref="FunctionEvaluable"/>.
        /// Therefore in that case, one <see cref="FunctionEvaluable"/> will have multiple <see cref="IEvaluable"/> mapped.
        /// </summary>
        /// <param name="arguments"><see cref="BasicArgument"/>, <see cref="ParameterlessArgument"/> or <see cref="FunctionEvaluable"/> that will be mapped to <see cref="IEvaluable"/>.</param>
        /// <param name="possibleArguments">All argument candidates provided together with the primary function in a function manager constructor.</param>
        /// <param name="currentStackSize">A stack size that determines on which recurrency level the current function is.</param>
        /// <returns>An enumerable of tuples that constitue a mapping of arguments to their eveluables.</returns>
        public static IEnumerable<Tuple<Argument, IEvaluable>> ExpandFunctionArguments(this IEnumerable<Argument> arguments, IReadOnlyCollection<object> possibleArguments, int currentStackSize) =>
            from argument in arguments
            let simpleEvaluable = argument as IEvaluable
            let evaluables =
                simpleEvaluable != null ?
                Enumerable.Repeat(simpleEvaluable, 1) :
                new Expanding((Delegate)argument.Value, possibleArguments, currentStackSize - 1).ToFunctionEvaluables()
            from evaluable in evaluables
            select Tuple.Create(argument, evaluable);
    }
}
