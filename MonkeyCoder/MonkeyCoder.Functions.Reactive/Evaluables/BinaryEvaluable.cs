using System;

namespace MonkeyCoder.Functions.Reactive
{
    internal abstract class BinaryEvaluable
    {
        public IEvaluable A { get; set; }
        public IEvaluable B { get; set; }

        protected abstract Func<dynamic, dynamic, dynamic> Operation { get; }

        public object Evaluate()
        {
            dynamic a = A.Evaluate();
            dynamic b = B.Evaluate();

            return Operation(a, b);
        }
    }
}
