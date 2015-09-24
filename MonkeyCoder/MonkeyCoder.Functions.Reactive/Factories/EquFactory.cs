using System.Collections.Generic;

namespace MonkeyCoder.Functions.Reactive
{
    internal class EquFactory : BinaryEvaluableFactory<Equ>
    {
        protected override IEvaluable GetRecursiveExpected(IEvaluable expected, IEvaluable current) => current;
        protected override IEnumerable<dynamic> ProhibitedValues => new dynamic[0];
    }
}
