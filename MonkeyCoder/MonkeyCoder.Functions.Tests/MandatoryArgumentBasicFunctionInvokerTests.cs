using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace MonkeyCoder.Functions.Tests
{
    [TestClass]
    public class MandatoryArgumentBasicFunctionInvokerTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Throws_exception_when_function_is_null()
        {
            new MandatoryArgumentBasicFunctionInvoker(null, null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Throws_exception_when_possible_arguments_are_null()
        {
            var function = new Action<int>(x => { });
            new MandatoryArgumentBasicFunctionInvoker(function, null, null);
        }

        [TestMethod]
        public void Works_with_empty_action_and_1_string_mandatory_argument()
        {
            var function = new Action(() => { });
            var sut = new MandatoryArgumentBasicFunctionInvoker(function, new object[0], "x");
            var e = sut.GetEnumerator();
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_int_action_and_1_string_mandatory_argument()
        {
            var function = new Action<int>(x => { });
            var sut = new MandatoryArgumentBasicFunctionInvoker(function, new object[0], "x");
            var e = sut.GetEnumerator();
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_string_action_and_1_string_mandatory_argument()
        {
            var result = "";
            var function = new Action<string>(x => { result = x; });
            var sut = new MandatoryArgumentBasicFunctionInvoker(function, new object[0], "x");
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            e.Current.DynamicInvoke();
            AreEqual("x", result);
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_int_action_and_2_null_possible_arguments_and_1_string_mandatory_argument()
        {
            var function = new Action<int>(x => { });
            var sut = new MandatoryArgumentBasicFunctionInvoker(function, new object[] { null, null }, "x");
            var e = sut.GetEnumerator();
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_nullable_int_action_and_2_null_possible_arguments_and_1_string_mandatory_argument()
        {
            var result = "";
            var function = new Action<int?>(x => { result = Convert.ToString(x); });
            var sut = new MandatoryArgumentBasicFunctionInvoker(function, new object[] { null, null }, "x");
            var e = sut.GetEnumerator();
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_nullable_int_action_and_2_null_possible_arguments_and_1_int_mandatory_argument()
        {
            var result = "";
            var function = new Action<int?>(x => { result = Convert.ToString(x); });
            var sut = new MandatoryArgumentBasicFunctionInvoker(function, new object[] { null, null }, 1);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            e.Current.DynamicInvoke();
            AreEqual("1", result);
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_string_action_and_2_null_possible_arguments_and_1_string_mandatory_argument()
        {
            var result = "";
            var function = new Action<string>(x => { result = x; });
            var sut = new MandatoryArgumentBasicFunctionInvoker(function, new object[] { null, null }, "x");
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            e.Current.DynamicInvoke();
            AreEqual("x", result);
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_string_1_int_action_and_2_null_1_int_possible_arguments_and_1_string_mandatory_argument()
        {
            var result = "";
            var function = new Action<int, string>((x, y) => { result = y + x; });
            var sut = new MandatoryArgumentBasicFunctionInvoker(function, new object[] { null, null, 1 }, "x");
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            e.Current.DynamicInvoke();
            AreEqual("x1", result);
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_string_1_nullable_int_action_and_2_null_1_int_possible_arguments_and_1_string_mandatory_argument()
        {
            var result = "";
            var function = new Action<int?, string>((x, y) => { result = y + x; });
            var sut = new MandatoryArgumentBasicFunctionInvoker(function, new object[] { null, null, 1 }, "x");
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            e.Current.DynamicInvoke();
            AreEqual("x", result);// 1null x
            IsTrue(e.MoveNext());
            e.Current.DynamicInvoke();
            AreEqual("x", result); // 2null x
            IsTrue(e.MoveNext());
            e.Current.DynamicInvoke();
            AreEqual("x1", result); // 1 x
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_int_action_and_1_int_possible_argument_and_1_int_mandatory_argument()
        {
            var result = -1;
            var function = new Action<int>(x => { result = x; });
            var sut = new MandatoryArgumentBasicFunctionInvoker(function, new object[] { 1 }, 2);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            e.Current.DynamicInvoke();
            AreEqual(2, result);
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_int_action_and_2_int_possible_arguments_and_1_int_mandatory_argument()
        {
            var result = -1;
            var function = new Action<int>(x => { result = x; });
            var sut = new MandatoryArgumentBasicFunctionInvoker(function, new object[] { 1, 2 }, 3);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            e.Current.DynamicInvoke();
            AreEqual(3, result);
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_int_action_and_2_int_1_string_possible_arguments_and_1_int_mandatory_argument()
        {
            object result = -1;
            var function = new Action<int>(x => { result = x; });
            var sut = new MandatoryArgumentBasicFunctionInvoker(function, new object[] { 1, 2, "a" }, 3);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            e.Current.DynamicInvoke();
            AreEqual(3, result);
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_int_func_and_1_string_mandatory_argument()
        {
            var function = new Func<int, int>(x => x);
            var sut = new MandatoryArgumentBasicFunctionInvoker(function, new object[0], "x");
            var e = sut.GetEnumerator();
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_int_func_and_1_int_mandatory_argument()
        {
            var function = new Func<int, int>(x => x);
            var sut = new MandatoryArgumentBasicFunctionInvoker(function, new object[0], 1);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual(1, e.Current.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_int_func_and_1_int_possible_argument_and_1_int_mandatory_argument()
        {
            var function = new Func<int, int>(x => x);
            var sut = new MandatoryArgumentBasicFunctionInvoker(function, new object[] { 1 }, 2);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual(2, e.Current.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_int_func_and_2_int_possible_arguments_and_1_int_mandatory_argument()
        {
            var function = new Func<int, int>(x => x);
            var sut = new MandatoryArgumentBasicFunctionInvoker(function, new object[] { 1, 2 }, 3);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual(3, e.Current.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_int_func_and_2_int_1_string_possible_arguments_and_1_int_mandatory_argument()
        {
            var function = new Func<int, int>(x => x);
            var sut = new MandatoryArgumentBasicFunctionInvoker(function, new object[] { 1, 2, "a" }, 3);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual(3, e.Current.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_2_int_func_and_1_int_mandatory_argument()
        {
            var function = new Func<int, int, int>((x, y) => x + y);
            var sut = new MandatoryArgumentBasicFunctionInvoker(function, new object[0], 1);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual(2, e.Current.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_2_int_func_and_1_int_possible_argument_and_1_int_mandatory_argument()
        {
            var function = new Func<int, int, int>((x, y) => x + y);
            var sut = new MandatoryArgumentBasicFunctionInvoker(function, new object[] { 1 }, 2);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual(3, e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual(3, e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual(4, e.Current.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_2_string_func_and_2_string_possible_arguments_and_1_int_mandatory_argument()
        {
            var function = new Func<string, string, string>((x, y) => x + y);
            var sut = new MandatoryArgumentBasicFunctionInvoker(function, new object[] { "a", "b" }, 1);
            var e = sut.GetEnumerator();
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_2_string_func_and_2_string_possible_arguments_and_1_string_mandatory_argument()
        {
            var function = new Func<string, string, string>((x, y) => x + y);
            var sut = new MandatoryArgumentBasicFunctionInvoker(function, new object[] { "a", "b" }, "x");
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("xa", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("xb", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("ax", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("bx", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("xx", e.Current.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_int_1_string_func_and_1_int_mandatory_argument()
        {
            var function = new Func<int, string, string>((x, y) => y + x);
            var sut = new MandatoryArgumentBasicFunctionInvoker(function, new object[0], 1);
            var e = sut.GetEnumerator();
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_int_1_string_func_and_1_string_mandatory_argument()
        {
            var function = new Func<int, string, string>((x, y) => y + x);
            var sut = new MandatoryArgumentBasicFunctionInvoker(function, new object[0], "x");
            var e = sut.GetEnumerator();
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_int_1_string_func_and_1_int_possible_argument_and_1_int_mandatory_argument()
        {
            var function = new Func<int, string, string>((x, y) => y + x);
            var sut = new MandatoryArgumentBasicFunctionInvoker(function, new object[] { 1 }, 2);
            var e = sut.GetEnumerator();
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_int_1_string_func_and_1_string_possible_argument_and_1_int_mandatory_argument()
        {
            var function = new Func<int, string, string>((x, y) => y + x);
            var sut = new MandatoryArgumentBasicFunctionInvoker(function, new object[] { "a" }, 1);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("a1", e.Current.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_int_1_string_func_and_1_int_1_string_possible_arguments_and_1_int_mandatory_argument()
        {
            var function = new Func<int, string, string>((x, y) => y + x);
            var sut = new MandatoryArgumentBasicFunctionInvoker(function, new object[] { 1, "a" }, 2);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("a2", e.Current.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_int_1_string_func_and_1_int_1_string_possible_arguments_and_1_string_mandatory_argument()
        {
            var function = new Func<int, string, string>((x, y) => y + x);
            var sut = new MandatoryArgumentBasicFunctionInvoker(function, new object[] { 1, "a" }, "x");
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("x1", e.Current.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_int_1_string_func_and_2_int_1_string_possible_argument_and_1_int_mandatory_argument()
        {
            var function = new Func<int, string, string>((x, y) => y + x);
            var sut = new MandatoryArgumentBasicFunctionInvoker(function, new object[] { 1, 2, "a" }, 3);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("a3", e.Current.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_int_1_string_func_and_2_int_1_string_possible_argument_and_1_string_mandatory_argument()
        {
            var function = new Func<int, string, string>((x, y) => y + x);
            var sut = new MandatoryArgumentBasicFunctionInvoker(function, new object[] { 1, 2, "a" }, "x");
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("x1", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("x2", e.Current.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_int_1_string_func_and_1_int_2_string_possible_arguments_and_1_int_mandatory_argument()
        {
            var function = new Func<int, string, string>((x, y) => y + x);
            var sut = new MandatoryArgumentBasicFunctionInvoker(function, new object[] { 1, "a", "b" }, 2);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("a2", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("b2", e.Current.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_int_1_string_func_and_1_int_2_string_possible_arguments_and_1_string_mandatory_argument()
        {
            var function = new Func<int, string, string>((x, y) => y + x);
            var sut = new MandatoryArgumentBasicFunctionInvoker(function, new object[] { 1, "a", "b" }, "x");
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("x1", e.Current.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_int_1_string_func_and_2_int_2_string_possible_arguments_and_1_int_mandatory_argument()
        {
            var function = new Func<int, string, string>((x, y) => y + x);
            var sut = new MandatoryArgumentBasicFunctionInvoker(function, new object[] { 1, 2, "a", "b" }, 3);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("a3", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("b3", e.Current.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_int_1_string_func_and_2_int_2_string_possible_arguments_and_1_string_mandatory_argument()
        {
            var function = new Func<int, string, string>((x, y) => y + x);
            var sut = new MandatoryArgumentBasicFunctionInvoker(function, new object[] { 1, 2, "a", "b" }, "x");
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("x1", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("x2", e.Current.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_2_int_1_string_func_and_2_int_2_string_possible_arguments_and_1_int_mandatory_argument()
        {
            var function = new Func<int, int, string, string>((x, y, z) => z + y + x);
            var sut = new MandatoryArgumentBasicFunctionInvoker(function, new object[] { 1, 2, "a", "b" }, 3);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("a13", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("b13", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("a23", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("b23", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("a31", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("b31", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("a32", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("b32", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("a33", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("b33", e.Current.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_3_string_func_and_2_int_2_string_possible_arguments_and_1_string_mandatory_argument()
        {
            var function = new Func<string, string, string, string>((x, y, z) => z + y + x);
            var sut = new MandatoryArgumentBasicFunctionInvoker(function, new object[] { 1, 2, "a", "b" }, "x");
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("aax", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("bax", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("abx", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("bbx", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("axa", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("bxa", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("axb", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("bxb", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("xaa", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("xba", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("xab", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("xbb", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("axx", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("bxx", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("xax", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("xbx", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("xxa", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("xxb", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("xxx", e.Current.DynamicInvoke());
            IsFalse(e.MoveNext());
        }
    }
}
