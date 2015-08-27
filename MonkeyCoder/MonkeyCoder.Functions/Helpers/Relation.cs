using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace MonkeyCoder.Functions.Helpers
{
    [DebuggerDisplay("{Parameter.TypeName} <- {ArgumentsString}")]
    internal class Relation
    {
        public Parameter Parameter { get; }
        public IEnumerable<Argument> Arguments { get; }
        private string ArgumentsString => string.Join(", ", Arguments.Select(x => x.Value));
        
        private Relation(Parameter parameter, IEnumerable<Argument> arguments)
        {
            Parameter = parameter;
            Arguments = arguments;
        }

        public static IEnumerable<Relation> GetDistinct(IEnumerable<Parameter> parameters, IReadOnlyCollection<Argument> arguments) =>
            from parameter in parameters
            select new Relation(parameter, parameter.GetAssignableArguments(arguments));
        
        public static IEnumerable<T> Get<T>(IEnumerable<ParameterInfo> parameters, IEnumerable<T> distinctRelations)
            where T : Relation =>
            from parameter in parameters
            join relation in distinctRelations on parameter.ParameterType.FullName equals relation.Parameter.Type.FullName
            select relation;

        [DebuggerDisplay("{Parameter.TypeName} <- {EvaluablesString}")]
        internal class Evaluable
        {
            public Parameter Parameter { get; }
            public IEnumerable<Argument.IEvaluable> Evaluables { get; }
            private string EvaluablesString => string.Join(", ", Evaluables.Select(x => x.Evaluate()));

            public Evaluable(Parameter parameter, IEnumerable<Argument.IEvaluable> evaluables)
            {
                Parameter = parameter;
                Evaluables = evaluables;
            }

            public static IEnumerable<T> Get<T>(IEnumerable<ParameterInfo> parameters, IEnumerable<T> distinctRelations)
                where T : Evaluable =>
                from parameter in parameters
                join relation in distinctRelations on parameter.ParameterType.FullName equals relation.Parameter.Type.FullName
                select relation;
        }

        internal class Mandatory : Relation
        {
            public bool IsAssignableFromMandatoryArgument { get; private set; }

            private Mandatory(Parameter parameter, IEnumerable<Argument> arguments, bool isAssignableFromMandatoryArgument)
                : base(parameter, arguments)
            {
                IsAssignableFromMandatoryArgument = isAssignableFromMandatoryArgument;
            }

            public static IEnumerable<Mandatory> GetDistinct(IEnumerable<Parameter> parameters, IReadOnlyCollection<Argument> arguments, Argument.Basic mandatoryArgument) =>
                from parameter in parameters
                let innerArguments = parameter.GetAssignableArguments(arguments)
                let isAssignableFromMandatoryArgument = mandatoryArgument.IsAssignableTo(parameter)
                select new Mandatory(parameter, innerArguments, isAssignableFromMandatoryArgument);
        }
    }
}
