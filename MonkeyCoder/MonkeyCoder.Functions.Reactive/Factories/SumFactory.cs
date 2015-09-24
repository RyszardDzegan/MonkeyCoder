﻿using System.Collections.Generic;

namespace MonkeyCoder.Functions.Reactive
{
    internal class SumFactory : BinaryNumberFactory<Sum>
    {
        protected override IEvaluable GetRecursiveExpected(IEvaluable expected, IEvaluable current) => new Number(expected.Evaluate() - current.Evaluate());
        protected override IEnumerable<dynamic> ProhibitedValues => new dynamic[0];
    }
}
