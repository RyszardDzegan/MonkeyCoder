using MonkeyCoder.Functions.Internals;
using NUnit.Framework;
using System;
using static NUnit.Framework.Assert;

namespace MonkeyCoder.Functions.Tests
{
    [TestFixture]
    public class MandatoryTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Throws_exception_when_function_is_null()
        {
            new Mandatory(null, null, null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Throws_exception_when_possible_arguments_are_null()
        {
            var function = new Action<int>(x => { });
            new Mandatory(function, null, null);
        }

        [Test]
        public void Works_with_empty_action_and_1_string_mandatory_argument()
        {
            var function = new Action(() => { });
            var sut = new Mandatory(function, new object[0], "x");
            var e = sut.GetEnumerator();
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_1_int_action_and_1_string_mandatory_argument()
        {
            var function = new Action<int>(x => { });
            var sut = new Mandatory(function, new object[0], "x");
            var e = sut.GetEnumerator();
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_1_string_action_and_1_string_mandatory_argument()
        {
            var result = "";
            var function = new Action<string>(x => { result = x; });
            var sut = new Mandatory(function, new object[0], "x");
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            e.Current.Function.DynamicInvoke();
            AreEqual("x", result);
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_1_int_action_and_2_null_possible_arguments_and_1_string_mandatory_argument()
        {
            var function = new Action<int>(x => { });
            var sut = new Mandatory(function, new object[] { null, null }, "x");
            var e = sut.GetEnumerator();
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_1_nullable_int_action_and_2_null_possible_arguments_and_1_string_mandatory_argument()
        {
            var result = "";
            var function = new Action<int?>(x => { result = Convert.ToString(x); });
            var sut = new Mandatory(function, new object[] { null, null }, "x");
            var e = sut.GetEnumerator();
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_1_nullable_int_action_and_2_null_possible_arguments_and_1_int_mandatory_argument()
        {
            var result = "";
            var function = new Action<int?>(x => { result = Convert.ToString(x); });
            var sut = new Mandatory(function, new object[] { null, null }, 1);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            e.Current.Function.DynamicInvoke();
            AreEqual("1", result);
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_1_string_action_and_2_null_possible_arguments_and_1_string_mandatory_argument()
        {
            var result = "";
            var function = new Action<string>(x => { result = x; });
            var sut = new Mandatory(function, new object[] { null, null }, "x");
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            e.Current.Function.DynamicInvoke();
            AreEqual("x", result);
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_1_string_1_int_action_and_2_null_1_int_possible_arguments_and_1_string_mandatory_argument()
        {
            var result = "";
            var function = new Action<int, string>((x, y) => { result = y + x; });
            var sut = new Mandatory(function, new object[] { null, null, 1 }, "x");
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            e.Current.Function.DynamicInvoke();
            AreEqual("x1", result);
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_1_string_1_nullable_int_action_and_2_null_1_int_possible_arguments_and_1_string_mandatory_argument()
        {
            var result = "";
            var function = new Action<int?, string>((x, y) => { result = y + x; });
            var sut = new Mandatory(function, new object[] { null, null, 1 }, "x");
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            e.Current.Function.DynamicInvoke();
            AreEqual("x", result);// 1null x
            IsTrue(e.MoveNext());
            e.Current.Function.DynamicInvoke();
            AreEqual("x", result); // 2null x
            IsTrue(e.MoveNext());
            e.Current.Function.DynamicInvoke();
            AreEqual("x1", result); // 1 x
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_1_int_action_and_1_int_possible_argument_and_1_int_mandatory_argument()
        {
            var result = -1;
            var function = new Action<int>(x => { result = x; });
            var sut = new Mandatory(function, new object[] { 1 }, 2);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            e.Current.Function.DynamicInvoke();
            AreEqual(2, result);
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_1_int_action_and_2_int_possible_arguments_and_1_int_mandatory_argument()
        {
            var result = -1;
            var function = new Action<int>(x => { result = x; });
            var sut = new Mandatory(function, new object[] { 1, 2 }, 3);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            e.Current.Function.DynamicInvoke();
            AreEqual(3, result);
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_1_int_action_and_2_int_1_string_possible_arguments_and_1_int_mandatory_argument()
        {
            object result = -1;
            var function = new Action<int>(x => { result = x; });
            var sut = new Mandatory(function, new object[] { 1, 2, "a" }, 3);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            e.Current.Function.DynamicInvoke();
            AreEqual(3, result);
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_1_int_func_and_1_string_mandatory_argument()
        {
            var function = new Func<int, int>(x => x);
            var sut = new Mandatory(function, new object[0], "x");
            var e = sut.GetEnumerator();
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_1_int_func_and_1_int_mandatory_argument()
        {
            var function = new Func<int, int>(x => x);
            var sut = new Mandatory(function, new object[0], 1);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual(1, e.Current.Function.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_1_int_func_and_1_int_possible_argument_and_1_int_mandatory_argument()
        {
            var function = new Func<int, int>(x => x);
            var sut = new Mandatory(function, new object[] { 1 }, 2);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual(2, e.Current.Function.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_1_int_func_and_2_int_possible_arguments_and_1_int_mandatory_argument()
        {
            var function = new Func<int, int>(x => x);
            var sut = new Mandatory(function, new object[] { 1, 2 }, 3);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual(3, e.Current.Function.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_1_int_func_and_2_int_1_string_possible_arguments_and_1_int_mandatory_argument()
        {
            var function = new Func<int, int>(x => x);
            var sut = new Mandatory(function, new object[] { 1, 2, "a" }, 3);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual(3, e.Current.Function.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_2_int_func_and_1_int_mandatory_argument()
        {
            var function = new Func<int, int, int>((x, y) => x + y);
            var sut = new Mandatory(function, new object[0], 1);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual(2, e.Current.Function.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_2_int_func_and_1_int_possible_argument_and_1_int_mandatory_argument()
        {
            var function = new Func<int, int, int>((x, y) => x + y);
            var sut = new Mandatory(function, new object[] { 1 }, 2);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual(3, e.Current.Function.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual(3, e.Current.Function.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual(4, e.Current.Function.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_2_string_func_and_2_string_possible_arguments_and_1_int_mandatory_argument()
        {
            var function = new Func<string, string, string>((x, y) => x + y);
            var sut = new Mandatory(function, new object[] { "a", "b" }, 1);
            var e = sut.GetEnumerator();
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_2_string_func_and_2_string_possible_arguments_and_1_string_mandatory_argument()
        {
            var function = new Func<string, string, string>((x, y) => x + y);
            var sut = new Mandatory(function, new object[] { "a", "b" }, "x");
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("xa", e.Current.Function.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("xb", e.Current.Function.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("ax", e.Current.Function.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("bx", e.Current.Function.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("xx", e.Current.Function.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_1_int_1_string_func_and_1_int_mandatory_argument()
        {
            var function = new Func<int, string, string>((x, y) => y + x);
            var sut = new Mandatory(function, new object[0], 1);
            var e = sut.GetEnumerator();
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_1_int_1_string_func_and_1_string_mandatory_argument()
        {
            var function = new Func<int, string, string>((x, y) => y + x);
            var sut = new Mandatory(function, new object[0], "x");
            var e = sut.GetEnumerator();
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_1_int_1_string_func_and_1_int_possible_argument_and_1_int_mandatory_argument()
        {
            var function = new Func<int, string, string>((x, y) => y + x);
            var sut = new Mandatory(function, new object[] { 1 }, 2);
            var e = sut.GetEnumerator();
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_1_int_1_string_func_and_1_string_possible_argument_and_1_int_mandatory_argument()
        {
            var function = new Func<int, string, string>((x, y) => y + x);
            var sut = new Mandatory(function, new object[] { "a" }, 1);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("a1", e.Current.Function.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_1_int_1_string_func_and_1_int_1_string_possible_arguments_and_1_int_mandatory_argument()
        {
            var function = new Func<int, string, string>((x, y) => y + x);
            var sut = new Mandatory(function, new object[] { 1, "a" }, 2);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("a2", e.Current.Function.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_1_int_1_string_func_and_1_int_1_string_possible_arguments_and_1_string_mandatory_argument()
        {
            var function = new Func<int, string, string>((x, y) => y + x);
            var sut = new Mandatory(function, new object[] { 1, "a" }, "x");
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("x1", e.Current.Function.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_1_int_1_string_func_and_2_int_1_string_possible_argument_and_1_int_mandatory_argument()
        {
            var function = new Func<int, string, string>((x, y) => y + x);
            var sut = new Mandatory(function, new object[] { 1, 2, "a" }, 3);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("a3", e.Current.Function.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_1_int_1_string_func_and_2_int_1_string_possible_argument_and_1_string_mandatory_argument()
        {
            var function = new Func<int, string, string>((x, y) => y + x);
            var sut = new Mandatory(function, new object[] { 1, 2, "a" }, "x");
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("x1", e.Current.Function.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("x2", e.Current.Function.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_1_int_1_string_func_and_1_int_2_string_possible_arguments_and_1_int_mandatory_argument()
        {
            var function = new Func<int, string, string>((x, y) => y + x);
            var sut = new Mandatory(function, new object[] { 1, "a", "b" }, 2);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("a2", e.Current.Function.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("b2", e.Current.Function.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_1_int_1_string_func_and_1_int_2_string_possible_arguments_and_1_string_mandatory_argument()
        {
            var function = new Func<int, string, string>((x, y) => y + x);
            var sut = new Mandatory(function, new object[] { 1, "a", "b" }, "x");
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("x1", e.Current.Function.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_1_int_1_string_func_and_2_int_2_string_possible_arguments_and_1_int_mandatory_argument()
        {
            var function = new Func<int, string, string>((x, y) => y + x);
            var sut = new Mandatory(function, new object[] { 1, 2, "a", "b" }, 3);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("a3", e.Current.Function.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("b3", e.Current.Function.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_1_int_1_string_func_and_2_int_2_string_possible_arguments_and_1_string_mandatory_argument()
        {
            var function = new Func<int, string, string>((x, y) => y + x);
            var sut = new Mandatory(function, new object[] { 1, 2, "a", "b" }, "x");
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("x1", e.Current.Function.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("x2", e.Current.Function.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_2_int_1_string_func_and_2_int_2_string_possible_arguments_and_1_int_mandatory_argument()
        {
            var function = new Func<int, int, string, string>((x, y, z) => z + y + x);
            var sut = new Mandatory(function, new object[] { 1, 2, "a", "b" }, 3);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("a13", e.Current.Function.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("b13", e.Current.Function.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("a23", e.Current.Function.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("b23", e.Current.Function.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("a31", e.Current.Function.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("b31", e.Current.Function.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("a32", e.Current.Function.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("b32", e.Current.Function.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("a33", e.Current.Function.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("b33", e.Current.Function.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_3_string_func_and_2_int_2_string_possible_arguments_and_1_string_mandatory_argument()
        {
            var function = new Func<string, string, string, string>((x, y, z) => z + y + x);
            var sut = new Mandatory(function, new object[] { 1, 2, "a", "b" }, "x");
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("aax", e.Current.Function.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("bax", e.Current.Function.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("abx", e.Current.Function.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("bbx", e.Current.Function.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("axa", e.Current.Function.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("bxa", e.Current.Function.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("axb", e.Current.Function.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("bxb", e.Current.Function.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("xaa", e.Current.Function.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("xba", e.Current.Function.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("xab", e.Current.Function.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("xbb", e.Current.Function.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("axx", e.Current.Function.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("bxx", e.Current.Function.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("xax", e.Current.Function.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("xbx", e.Current.Function.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("xxa", e.Current.Function.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("xxb", e.Current.Function.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("xxx", e.Current.Function.DynamicInvoke());
            IsFalse(e.MoveNext());
        }
    }
}
