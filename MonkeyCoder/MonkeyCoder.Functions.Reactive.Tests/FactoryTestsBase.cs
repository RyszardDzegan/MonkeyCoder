using MonkeyCoder.Functions.Reactive;
using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using static NUnit.Framework.Assert;

namespace MonkeyCoder.Functions.Tests
{
    public abstract class FactoryTestsBase
    {
        internal IObservable<INumber> GetDataSource(params int[] items)
        {
            return Observable.Create<INumber>(o =>
            {
                foreach (var item in items)
                    o.OnNext(new Number(item));
                o.OnCompleted();
                return Disposable.Empty;
            });
        }

        internal IObservable<IEvaluableFactoryProvider> GetFactoryProvidersSource(params IEvaluableFactoryProvider[] items)
        {
            return Observable.Create<IEvaluableFactoryProvider>(o =>
            {
                foreach (var item in items)
                    o.OnNext(item);
                o.OnCompleted();
                return Disposable.Empty;
            });
        }

        internal void Assert(IEnumerator<IEvaluable> enumerator, string formula)
        {
            IsTrue(enumerator.MoveNext());
            var stringBuilderVisitor = new StringBuilderVisitor();
            enumerator.Current.Accept(stringBuilderVisitor);
            AreEqual(formula, stringBuilderVisitor.ToString());
        }
    }
}
