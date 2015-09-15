using MonkeyCoder.Functions.Reactive;
using NUnit.Framework;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using static NUnit.Framework.Assert;

namespace MonkeyCoder.Functions.Tests
{
    [TestFixture]
    public class SumEvaluableTests
    {
        [Test]
        public void Works_with_three_integers()
        {
            var inputs = new Subject<IEvaluable>();
            var sut = new SumEvaluableFactory();
            var results = sut.Subscribe(inputs);
            var enumerator = results.GetEnumerator();

            inputs.OnNext(new Number(1));
            inputs.OnNext(new Number(2));
            inputs.OnNext(new Number(3));
            inputs.OnCompleted();

            Assert(enumerator, 2, "(1+1)");
            Assert(enumerator, 3, "(1+2)");
            Assert(enumerator, 4, "(2+2)");
            Assert(enumerator, 4, "(1+3)");
            Assert(enumerator, 5, "(2+3)");
            Assert(enumerator, 6, "(3+3)");
            IsFalse(enumerator.MoveNext());
        }

        [Test]
        public void Works_with_two_integers_when_looped()
        {
            var inputs = new Subject<IEvaluable>();
            var sut = new SumEvaluableFactory();
            var results1 = sut.Subscribe(inputs);
            var results2 = sut.Subscribe(results1);
            var results = results1.Merge(results2);
            var enumerator = results.GetEnumerator();
            
            inputs.OnNext(new Number(1));
            inputs.OnNext(new Number(2));
            inputs.OnNext(new Number(3));
            inputs.OnCompleted();

            Assert(enumerator, 2, "(1+1)");
            Assert(enumerator, 4, "((1+1)+(1+1))");
            Assert(enumerator, 3, "(1+2)");
            Assert(enumerator, 4, "(2+2)");
            Assert(enumerator, 5, "((1+1)+(1+2))");
            Assert(enumerator, 6, "((1+2)+(1+2))");
            Assert(enumerator, 6, "((1+1)+(2+2))");
            Assert(enumerator, 7, "((1+2)+(2+2))");
            Assert(enumerator, 8, "((2+2)+(2+2))");
            Assert(enumerator, 4, "(1+3)");
            Assert(enumerator, 5, "(2+3)");
            Assert(enumerator, 6, "(3+3)");
            Assert(enumerator, 6, "((1+1)+(1+3))");
            Assert(enumerator, 7, "((1+2)+(1+3))");
            Assert(enumerator, 8, "((2+2)+(1+3))");
            Assert(enumerator, 8, "((1+3)+(1+3))");
            Assert(enumerator, 7, "((1+1)+(2+3))");
            Assert(enumerator, 8, "((1+2)+(2+3))");
            Assert(enumerator, 9, "((2+2)+(2+3))");
            Assert(enumerator, 9, "((1+3)+(2+3))");
            Assert(enumerator, 10, "((2+3)+(2+3))");
            Assert(enumerator, 8, "((1+1)+(3+3))");
            Assert(enumerator, 9, "((1+2)+(3+3))");
            Assert(enumerator, 10, "((2+2)+(3+3))");
            Assert(enumerator, 10, "((1+3)+(3+3))");
            Assert(enumerator, 11, "((2+3)+(3+3))");
            Assert(enumerator, 12, "((3+3)+(3+3))");
            IsFalse(enumerator.MoveNext());
        }

        private void Assert(IEnumerator<IEvaluable> enumerator, int value, string text)
        {
            IsTrue(enumerator.MoveNext());
            IsInstanceOf<INumber>(enumerator.Current);
            var stringBuilderVisitor = new StringBuilderVisitor();
            enumerator.Current.Accept(stringBuilderVisitor);
            AreEqual(text, stringBuilderVisitor.ToString());
            AreEqual(value, enumerator.Current.Evaluate());
        }
    }
}
