using MonkeyCoder.Functions.Invocations;
using NUnit.Framework;
using System;
using System.Linq;
using static NUnit.Framework.Assert;

namespace MonkeyCoder.Functions.Tests
{
    [TestFixture]
    public class InvocationTests
    {
        [Test]
        public void Works_with_simple_value()
        {
            var items = new[] { 1 };
            var sut = items.AsFunctionsTree();
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            IsInstanceOf<ValueInvocation>(e.Current);
            AreEqual(1, ((ValueInvocation)e.Current).Value);
            AreEqual(0, e.Current.Arguments.Count());
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_parameterless_delegate()
        {
            var func = new Func<string>(() => "a");
            var items = new[] { func };
            var sut = items.AsFunctionsTree();
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            IsInstanceOf<DelegateInvocation>(e.Current);
            AreSame(func, ((DelegateInvocation)e.Current).Delegate);
            AreEqual(0, e.Current.Arguments.Count());
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_delegate()
        {
            var func = new Func<int, string>(x => "a" + x);
            var items = new object[] { 1, func, 2 };
            var sut = items.AsFunctionsTree();
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            IsInstanceOf<ValueInvocation>(e.Current);
            AreEqual(1, ((ValueInvocation)e.Current).Value);
            AreEqual(0, e.Current.Arguments.Count());
            IsTrue(e.MoveNext());
            IsInstanceOf<DelegateInvocation>(e.Current);
            AreSame(func, ((DelegateInvocation)e.Current).Delegate);
            AreEqual(1, e.Current.Arguments.Count());
            IsInstanceOf<ValueInvocation>(e.Current.Arguments.First());
            AreEqual(1, ((ValueInvocation)e.Current.Arguments.First()).Value);
            AreEqual("a1", e.Current.Function());
            IsTrue(e.MoveNext());
            IsInstanceOf<DelegateInvocation>(e.Current);
            AreSame(func, ((DelegateInvocation)e.Current).Delegate);
            AreEqual(1, e.Current.Arguments.Count());
            IsInstanceOf<ValueInvocation>(e.Current.Arguments.First());
            AreEqual(2, ((ValueInvocation)e.Current.Arguments.First()).Value);
            AreEqual("a2", e.Current.Function());
            IsTrue(e.MoveNext());
            IsInstanceOf<ValueInvocation>(e.Current);
            AreEqual(2, ((ValueInvocation)e.Current).Value);
            AreEqual(0, e.Current.Arguments.Count());
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_call_stack()
        {
            var func = new Func<string, string>(x => "a" + x);
            var items = new object[] { func, "b" };
            var sut = items.AsFunctionsTree(1);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            IsInstanceOf<DelegateInvocation>(e.Current);
            AreSame(func, ((DelegateInvocation)e.Current).Delegate);
            AreEqual(1, e.Current.Arguments.Count());
            IsInstanceOf<DelegateInvocation>(e.Current.Arguments.First());
            AreSame(func, ((DelegateInvocation)e.Current.Arguments.First()).OriginalValue);
            AreEqual(1, ((DelegateInvocation)e.Current.Arguments.First()).Arguments.Count());
            IsInstanceOf<ValueInvocation>(((DelegateInvocation)e.Current.Arguments.First()).Arguments.First());
            AreEqual("b", ((ValueInvocation)((DelegateInvocation)e.Current.Arguments.First()).Arguments.First()).Value);
            AreEqual("aab", e.Current.Function());
            IsTrue(e.MoveNext());
            IsInstanceOf<DelegateInvocation>(e.Current);
            AreSame(func, ((DelegateInvocation)e.Current).Delegate);
            AreEqual(1, e.Current.Arguments.Count());
            IsInstanceOf<ValueInvocation>(e.Current.Arguments.First());
            AreEqual("b", ((ValueInvocation)e.Current.Arguments.First()).Value);
            AreEqual("ab", e.Current.Function());
            IsTrue(e.MoveNext());
            IsInstanceOf<ValueInvocation>(e.Current);
            AreEqual("b", ((ValueInvocation)e.Current).Value);
            AreEqual(0, e.Current.Arguments.Count());
            IsFalse(e.MoveNext());
        }
    }
}
