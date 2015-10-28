using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MonkeyCoder.Functions.Helpers
{
    /// <summary>
    /// A class that stores information about an argument.
    /// </summary>
    [DebuggerDisplay("{Value != null ? Value : \"null\"}")]
    internal abstract class Argument
    {
        /// <summary>
        /// Argument's value.
        /// Can be null or a function.
        /// In case of function, its real value is taken at runtime.
        /// <seealso cref="IEvaluable"/>.
        /// </summary>
        public object Value { get; private set; }

        /// <summary>
        /// Argument's type.
        /// If <see cref="Value"/> is null then <see cref="Type"/> is null as well.
        /// </summary>
        public Type Type { get; private set; }

        private Argument(object value, Type type)
        {
            Value = value;
            Type = type;
        }

        /// <summary>
        /// Checks whether current instance of <see cref="Argument"/> is assignable to given <paramref name="parameter"/>.
        /// A concrete subclass decides, how the assignability is defined.
        /// </summary>
        /// <param name="parameter">A parameter to which the assignability is being checked.</param>
        /// <returns>True, if current instance of <see cref="Argument"/> is assignable to given <paramref name="parameter"/>. Otherwise false.</returns>
        public abstract bool IsAssignableTo(Parameter parameter);
        
        /// <summary>
        /// The interface is used to obtain <see cref="Value"/> at runtime
        /// when it stores a functions instead of simple value.
        /// </summary>
        internal interface IEvaluable
        {
            /// <summary>
            /// Returns argument's value at runtime.
            /// </summary>
            /// <returns>Argument's value.</returns>
            object Evaluate();
        }

        internal class Basic : Argument, IEvaluable
        {
            /// <summary>
            /// True if argument is null.
            /// Otherwise false.
            /// </summary>
            public bool IsNull => Type == null;

            public Basic(object value)
                : this(value, value?.GetType())
            { }

            internal Basic(object value, Type type)
                : base(value, type)
            { }

            public override bool IsAssignableTo(Parameter parameter) =>
                IsNull ? parameter.IsNullAssignable : parameter.Type.IsAssignableFrom(Type);

            public object Evaluate() =>
                Value;

            public static IEnumerable<Argument> Get(IEnumerable<object> possibleArguments) =>
                from possibleArgument in possibleArguments
                let type = possibleArgument?.GetType()
                select new Basic(possibleArgument, type);
        }
        
        internal class Function : Argument
        {
            internal Function(object value, Type type)
                : base(value, type)
            { }

            public override bool IsAssignableTo(Parameter parameter) =>
                parameter.Type.IsAssignableFrom(Type);

            public static IEnumerable<Argument> Get(IEnumerable<object> possibleArguments) =>
                from possibleArgument in possibleArguments
                let method = (possibleArgument as Delegate)?.Method
                let basic = new Basic(possibleArgument, possibleArgument?.GetType())
                let function = method != null && method.GetParameters().Any() ? new Function(possibleArgument, method.ReturnType) : null
                let parameterless = method != null && !method.GetParameters().Any() ? new Parameterless(possibleArgument, method.ReturnType) : null
                let arguments = new Argument[] { basic, function, parameterless }
                from argument in arguments
                where argument != null
                select argument;

            internal class Parameterless : Function, IEvaluable
            {
                internal Parameterless(object value, Type type)
                    : base(value, type)
                { }

                public object Evaluate() =>
                    ((Delegate)Value).DynamicInvoke();

                public new static IEnumerable<Argument> Get(IEnumerable<object> possibleArguments) =>
                    from possibleArgument in possibleArguments
                    let method = (possibleArgument as Delegate)?.Method
                    let basic = new Basic(possibleArgument, possibleArgument?.GetType())
                    let parameterless = method != null && !method.GetParameters().Any() ? new Parameterless(possibleArgument, method.ReturnType) : null
                    let arguments = new Argument[] { basic, parameterless }
                    from argument in arguments
                    where argument != null
                    select argument;
            }

            [DebuggerDisplay("{Evaluate()}")]
            internal class Evaluable : IEvaluable
            {
                private Func<object> Function { get; }

                private Evaluable(Func<object> function)
                {
                    Function = function;
                }

                public object Evaluate() => Function();

                public static IEnumerable<IEvaluable> Get(IEnumerable<Func<object>> functions) =>
                    from function in functions
                    select new Evaluable(function);
            }
        }
    }
}
