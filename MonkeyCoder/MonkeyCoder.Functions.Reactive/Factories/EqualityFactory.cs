﻿using System;

namespace MonkeyCoder.Functions.Reactive
{
    internal class EqualityFactory : BinaryEvaluableFactory<Equality>
    {
        protected override void OnChildNext(IEvaluable current, IEvaluable childNext, IObserver<IEvaluable> observer)
        {
            if (current.Evaluate().Equals(childNext.Evaluate()))
                base.OnChildNext(current, childNext, observer);
        }
    }
}
