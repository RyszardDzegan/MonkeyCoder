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
        public static IEnumerable<Argument> ToProFunctionArguments(this IEnumerable<object> possibleArguments) =>
            from possibleArgument in possibleArguments
            let method = (possibleArgument as Delegate)?.Method
            let basic = new BasicArgument(possibleArgument, possibleArgument?.GetType())
            let function = method != null && method.GetParameters().Any() ? new FunctionArgument(possibleArgument, method.ReturnType) : null
            let parameterless = method != null && !method.GetParameters().Any() ? new ParameterlessArgument(possibleArgument, method.ReturnType) : null
            let arguments = new Argument[] { basic, function, parameterless }
            from argument in arguments
            where argument != null
            select argument;
    }
}
