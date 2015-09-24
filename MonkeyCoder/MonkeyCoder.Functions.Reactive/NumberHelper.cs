using System;

namespace MonkeyCoder.Functions.Reactive
{
    internal static class NumberHelper
    {
        private static readonly Type NumberType = typeof(INumber);
        public static bool IsAssignableFrom(IEvaluable evaluable) => NumberType.IsAssignableFrom(evaluable.GetType());
    }
}
