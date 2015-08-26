using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MonkeyCoder.Functions
{
    [DebuggerDisplay("{Type.Name}")]
    internal class ParameterInfo
    {
        public Type Type { get; private set; }
        public bool IsNullAssignable { get; private set; }
        
        public IEnumerable<ArgumentInfo> GetAssignableArguments(IReadOnlyCollection<ArgumentInfo> argumentsInfo) =>
            from argumentInfo in argumentsInfo
            where argumentInfo.IsAssignableTo(this)
            select argumentInfo;
        
        private ParameterInfo() { }

        public static IEnumerable<ParameterInfo> GetDistinct(IReadOnlyCollection<System.Reflection.ParameterInfo> parameters)
        {
            return parameters
                .Select(x => x.ParameterType)
                .Distinct(TypeFullNameEqualityComparer.Instance)
                .Select(x =>
                {
                    var isNullAssignable = !x.IsValueType || x.FullName.StartsWith("System.Nullable`1");
                    return new ParameterInfo { Type = x, IsNullAssignable = isNullAssignable };
                });
        }
    }
}
