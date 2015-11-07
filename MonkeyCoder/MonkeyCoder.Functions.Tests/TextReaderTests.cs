using MonkeyCoder.Functions.Helpers.Arguments;
using MonkeyCoder.Functions.Helpers.Invocations;
using MonkeyCoder.Functions.Readers;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
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
            sut.Visit(e.Current);
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
            sut.Visit(e.Current);
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
            sut.Visit(e.Current);
            AreEqual("1", sut.ToString());
            IsTrue(e.MoveNext());
            sut.Visit(e.Current);
            AreEqual("foo(1)", sut.ToString());
            IsTrue(e.MoveNext());
            sut.Visit(e.Current);
            AreEqual("foo(2)", sut.ToString());
            IsTrue(e.MoveNext());
            sut.Visit(e.Current);
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
            sut.Visit(e.Current);
            AreEqual("foo(foo(\"b\"))", sut.ToString());
            IsTrue(e.MoveNext());
            sut.Visit(e.Current);
            AreEqual("foo(\"b\")", sut.ToString());
            IsTrue(e.MoveNext());
            sut.Visit(e.Current);
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
            sut.Visit(e.Current);
            AreEqual("foo(foo(\"c\"))", sut.ToString());
            IsTrue(e.MoveNext());
            sut.Visit(e.Current);
            AreEqual("foo(bar())", sut.ToString());
            IsTrue(e.MoveNext());
            sut.Visit(e.Current);
            AreEqual("foo(\"c\")", sut.ToString());
            IsTrue(e.MoveNext());
            sut.Visit(e.Current);
            AreEqual("bar()", sut.ToString());
            IsTrue(e.MoveNext());
            sut.Visit(e.Current);
            AreEqual("\"c\"", sut.ToString());
            IsFalse(e.MoveNext());
        }
    }
}
