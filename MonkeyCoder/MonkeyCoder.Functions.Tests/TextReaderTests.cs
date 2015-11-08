using MonkeyCoder.Functions.Readers;
using NUnit.Framework;
using System;
using static NUnit.Framework.Assert;

namespace MonkeyCoder.Functions.Tests
{
    [TestFixture]
    public class TextReaderTests
    {
        [Test]
        public void Works_with_simple_value()
        {
            var items = new[] { 1 };
            var invocations = items.AsFunctionsTree();
            var e = invocations.GetEnumerator();
            var sut = new TextInvocationReader();
            IsTrue(e.MoveNext());
            e.Current.Accept(sut);
            AreEqual("1", sut.ToString());
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_parameterless_delegate()
        {
            var func = new Func<string>(() => "a");
            var items = new[] { func };
            var invocations = items.AsFunctionsTree();
            var e = invocations.GetEnumerator();
            var sut = new TextInvocationReader();
            IsTrue(e.MoveNext());
            e.Current.Accept(sut);
            AreEqual("foo()", sut.ToString());
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_delegate()
        {
            var func = new Func<int, string>(x => "a" + x);
            var items = new object[] { 1, func, 2 };
            var invocations = items.AsFunctionsTree();
            var e = invocations.GetEnumerator();
            var sut = new TextInvocationReader();
            IsTrue(e.MoveNext());
            e.Current.Accept(sut);
            AreEqual("1", sut.ToString());
            IsTrue(e.MoveNext());
            sut.Clear();
            e.Current.Accept(sut);
            AreEqual("foo(1)", sut.ToString());
            IsTrue(e.MoveNext());
            sut.Clear();
            e.Current.Accept(sut);
            AreEqual("foo(2)", sut.ToString());
            IsTrue(e.MoveNext());
            sut.Clear();
            e.Current.Accept(sut);
            AreEqual("2", sut.ToString());
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_call_stack()
        {
            var func = new Func<string, string>(x => "a" + x);
            var items = new object[] { func, "b" };
            var invocations = items.AsFunctionsTree(1);
            var e = invocations.GetEnumerator();
            var sut = new TextInvocationReader();
            IsTrue(e.MoveNext());
            sut.Clear();
            e.Current.Accept(sut);
            AreEqual("foo(foo(\"b\"))", sut.ToString());
            IsTrue(e.MoveNext());
            sut.Clear();
            e.Current.Accept(sut);
            AreEqual("foo(\"b\")", sut.ToString());
            IsTrue(e.MoveNext());
            sut.Clear();
            e.Current.Accept(sut);
            AreEqual("\"b\"", sut.ToString());
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_call_stack_and_two_functions()
        {
            var func1 = new Func<string, string>(x => "a" + x);
            var func2 = new Func<string>(() => "b");
            var items = new object[] { func1, func2, "c" };
            var invocations = items.AsFunctionsTree(1);
            var e = invocations.GetEnumerator();
            var sut = new TextInvocationReader();
            IsTrue(e.MoveNext());
            sut.Clear();
            e.Current.Accept(sut);
            AreEqual("foo(foo(\"c\"))", sut.ToString());
            IsTrue(e.MoveNext());
            sut.Clear();
            e.Current.Accept(sut);
            AreEqual("foo(bar())", sut.ToString());
            IsTrue(e.MoveNext());
            sut.Clear();
            e.Current.Accept(sut);
            AreEqual("foo(\"c\")", sut.ToString());
            IsTrue(e.MoveNext());
            sut.Clear();
            e.Current.Accept(sut);
            AreEqual("bar()", sut.ToString());
            IsTrue(e.MoveNext());
            sut.Clear();
            e.Current.Accept(sut);
            AreEqual("\"c\"", sut.ToString());
            IsFalse(e.MoveNext());
        }
    }
}
