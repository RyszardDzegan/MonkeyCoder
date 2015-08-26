using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MonkeyCoder.Functions
{
    [DebuggerDisplay("{Value != null ? Value : \"null\"}")]
    internal abstract class ArgumentInfo
    {
        public object Value { get; private set; }
        public Type Type { get; private set; }

        private ArgumentInfo(object value, Type type)
        {
            Value = value;
            Type = type;
        }
        
        public abstract bool IsAssignableTo(ParameterInfo parameterInfo);
        
        internal class Basic : ArgumentInfo
        {
            public bool IsNull { get; }

            public Basic(object value)
                : base(value, value != null ? value.GetType() : null)
            {
                IsNull = Type == null;
            }

            public static IEnumerable<ArgumentInfo> Get(IReadOnlyCollection<object> values) =>
                from value in values
                select new Basic(value);

            public override bool IsAssignableTo(ParameterInfo parameterInfo) =>
                IsNull ? parameterInfo.IsNullAssignable : parameterInfo.Type.IsAssignableFrom(Type);
        }

        internal class DelgateExpander : ArgumentInfo
        {
            public DelgateExpander(object value, Type type)
                : base(value, type)
            { }

            public static IEnumerable<ArgumentInfo> Get(IReadOnlyCollection<object> values) =>
                from value in values
                let delegateMethodInfo = (value as Delegate)?.Method
                let isDelegate = delegateMethodInfo != null
                let isParameterLessDelegate = isDelegate && !delegateMethodInfo.GetParameters().Any()
                where isParameterLessDelegate
                let delegateReturnType = delegateMethodInfo?.ReturnType
                select new DelgateExpander(value, delegateReturnType);

            public override bool IsAssignableTo(ParameterInfo parameterInfo) =>
                parameterInfo.Type.IsAssignableFrom(Type);
        }
    }
}
