using System;
using System.Collections.Generic;
using System.Linq;

namespace MonkeyCoder.Functions
{
    internal class RelationsInfo
    {
        public Type Parameter { get; private set; }
        public IReadOnlyCollection<ArgumentInfo> ArgumentsInfo { get; private set; }

        internal class Basic : RelationsInfo
        {
            public static IEnumerable<RelationsInfo> GetDistinct(IReadOnlyCollection<ParameterInfo> parametersInfo, IReadOnlyCollection<ArgumentInfo> argumentsInfo) =>
                from parameterInfo in parametersInfo
                select new RelationsInfo { Parameter = parameterInfo.Type, ArgumentsInfo = parameterInfo.GetAssignableArguments(argumentsInfo).ToList() };
        }

        internal class Mandatory : RelationsInfo
        {
            public bool IsAssignableFromMandatoryArgument { get; private set; }

            public static IEnumerable<Mandatory> GetDistinct(IReadOnlyCollection<ParameterInfo> parametersInfo, IReadOnlyCollection<ArgumentInfo> argumentsInfo, ArgumentInfo.Basic mandatoryArgumentInfo) =>
                from parameterInfo in parametersInfo
                let innerArguments = parameterInfo.GetAssignableArguments(argumentsInfo).ToList()
                let isAssignableFromMandatoryArgument = mandatoryArgumentInfo.IsAssignableTo(parameterInfo)
                select new Mandatory { Parameter = parameterInfo.Type, ArgumentsInfo = innerArguments, IsAssignableFromMandatoryArgument = isAssignableFromMandatoryArgument };
        }

        public static IEnumerable<T> Get<T>(IReadOnlyCollection<System.Reflection.ParameterInfo> parameters, IEnumerable<T> distinctAssociations)
            where T : RelationsInfo =>
            from parameter in parameters
            join association in distinctAssociations on parameter.ParameterType.FullName equals association.Parameter.FullName
            select association;

        public static IEnumerable<RelationsInfo> Union(IEnumerable<RelationsInfo> relations1, IEnumerable<RelationsInfo> relations2) =>
            from relation1 in relations1
            join relation2 in relations2 on relation1.Parameter.FullName equals relation2.Parameter.FullName
            let parameter = relation1.Parameter
            let arguments1 = relation1.ArgumentsInfo
            let arguments2 = relation2.ArgumentsInfo
            let arguments = arguments1.Concat(arguments2).ToList()
            select new RelationsInfo { Parameter = parameter, ArgumentsInfo = arguments };
    }
}
