﻿using System;

namespace MonkeyCoder.Functions.Reactive
{
    internal class Sum : BinaryEvaluable, IBinaryEvaluable, INumber
    {
        protected override Func<dynamic, dynamic, dynamic> Operation => (x, y) => x + y;
        void IVisitable.Accept(IVisitor visitor) => visitor.Visit(this);
    }
}