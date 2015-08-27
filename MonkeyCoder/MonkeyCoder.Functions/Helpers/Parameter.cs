using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace MonkeyCoder.Functions.Helpers
{
    [DebuggerDisplay("{TypeName}")]
    internal class Parameter
    {
        public Type Type { get; private set; }
        public bool IsNullAssignable { get; private set; }

        private string TypeName =>
            !Type.GetGenericArguments().Any() ? Type.Name : Type.Name.Split('`').First() + "<" + string.Join(", ", Type.GetGenericArguments().Select(x => x.Name)) + ">";
        
        public IEnumerable<Argument> GetAssignableArguments(IEnumerable<Argument> arguments) =>
            from argument in arguments
            where argument.IsAssignableTo(this)
            select argument;
        
        private Parameter() { }

        public static IEnumerable<Parameter> GetDistinct(IEnumerable<ParameterInfo> parameters)
        {
            return parameters
                .Select(x => x.ParameterType)
                .Distinct(TypeFullNameEqualityComparer.Instance)
                .Select(x =>
                {
                    var isNullAssignable = !x.IsValueType || x.FullName.StartsWith("System.Nullable`1");
                    return new Parameter { Type = x, IsNullAssignable = isNullAssignable };
                });
        }
    }
}
