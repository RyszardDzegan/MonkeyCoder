using System;

namespace MonkeyCoder.Functions.Reactive
{
    internal static class AssignablityHelper<TBase>
    {
        private static readonly Type BaseType = typeof(TBase);
        public static bool IsAssignableFrom(IEvaluable evaluable) => BaseType.IsAssignableFrom(evaluable.GetType());
    }
}
