using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace MonkeyCoder.Core.Tests
{
    [TestClass]
    public class SingleFunctionInvokerTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Throws_exception_when_function_is_null()
        {
            new SingleFunctionInvoker(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Throws_exception_when_possible_arguments_are_null()
        {
            var function = new Action<int>(x => { });
            new SingleFunctionInvoker(function, null);
        }

        [TestMethod]
        public void Works_with_empty_action_and_0_arguments()
        {
            var function = new Action(() => { });
            var sut = new SingleFunctionInvoker(function);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            IsFalse(e.Current.Method.GetParameters().Any());
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_int_action_and_0_arguments()
        {
            var function = new Action<int>(x => { });
            var sut = new SingleFunctionInvoker(function);
            var e = sut.GetEnumerator();
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_int_action_and_2_null_arguments()
        {
            var function = new Action<int>(x => { });
            var sut = new SingleFunctionInvoker(function, null, null);
            var e = sut.GetEnumerator();
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_string_action_and_2_null_arguments()
        {
            var result = "";
            var function = new Action<string>(x => { result = x; });
            var sut = new SingleFunctionInvoker(function, null, null);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            e.Current.DynamicInvoke();
            AreEqual(null, result);
            IsTrue(e.MoveNext());
            e.Current.DynamicInvoke();
            AreEqual(null, result);
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_string_1_int_action_and_2_null_1_int_arguments()
        {
            var result = "";
            var function = new Action<int, string>((x, y) => { result = y + x; });
            var sut = new SingleFunctionInvoker(function, null, null, 1);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            e.Current.DynamicInvoke();
            AreEqual("1", result);
            IsTrue(e.MoveNext());
            e.Current.DynamicInvoke();
            AreEqual("1", result);
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_string_1_nullable_int_action_and_2_null_1_int_arguments()
        {
            var result = "";
            var function = new Action<int?, string>((x, y) => { result = y + x; });
            var sut = new SingleFunctionInvoker(function, null, null, 1);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            e.Current.DynamicInvoke();
            AreEqual("", result);// 1null 1null
            IsTrue(e.MoveNext());
            e.Current.DynamicInvoke();
            AreEqual("", result); // 1null 2null
            IsTrue(e.MoveNext());
            e.Current.DynamicInvoke();
            AreEqual("", result); // 2null 1null
            IsTrue(e.MoveNext());
            e.Current.DynamicInvoke();
            AreEqual("", result); // 2null 2null
            IsTrue(e.MoveNext());
            e.Current.DynamicInvoke();
            AreEqual("1", result); // 11 1null
            IsTrue(e.MoveNext());
            e.Current.DynamicInvoke();
            AreEqual("1", result); // 11 2null;
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_int_action_and_1_int_argument()
        {
            var result = -1;
            var function = new Action<int>(x => { result = x; });
            var sut = new SingleFunctionInvoker(function, 1);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            e.Current.DynamicInvoke();
            AreEqual(1, result);
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_int_action_and_2_int_arguments()
        {
            var result = -1;
            var function = new Action<int>(x => { result = x; });
            var sut = new SingleFunctionInvoker(function, 1, 2);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            e.Current.DynamicInvoke();
            AreEqual(1, result);
            IsTrue(e.MoveNext());
            e.Current.DynamicInvoke();
            AreEqual(2, result);
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_int_action_and_2_int_1_string_arguments()
        {
            object result = -1;
            var function = new Action<int>(x => { result = x; });
            var sut = new SingleFunctionInvoker(function, 1, 2, "a");
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            e.Current.DynamicInvoke();
            AreEqual(1, result);
            IsTrue(e.MoveNext());
            e.Current.DynamicInvoke();
            AreEqual(2, result);
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_int_func_and_0_int_argument()
        {
            var function = new Func<int, int>(x => x);
            var sut = new SingleFunctionInvoker(function);
            var e = sut.GetEnumerator();
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_int_func_and_1_int_argument()
        {
            var function = new Func<int, int>(x => x);
            var sut = new SingleFunctionInvoker(function, 1);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual(1, e.Current.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_int_func_and_2_int_argument()
        {
            var function = new Func<int, int>(x => x);
            var sut = new SingleFunctionInvoker(function, 1, 2);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual(1, e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual(2, e.Current.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_int_func_and_2_int_1_string_argument()
        {
            var function = new Func<int, int>(x => x);
            var sut = new SingleFunctionInvoker(function, 1, 2, "a");
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual(1, e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual(2, e.Current.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_2_int_func_and_0_argument()
        {
            var function = new Func<int, int, int>((x, y) => x + y);
            var sut = new SingleFunctionInvoker(function);
            var e = sut.GetEnumerator();
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_2_int_func_and_1_int_argument()
        {
            var function = new Func<int, int, int>((x, y) => x + y);
            var sut = new SingleFunctionInvoker(function, 1);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual(2, e.Current.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_2_string_func_and_2_string_argument()
        {
            var function = new Func<string, string, string>((x, y) => x + y);
            var sut = new SingleFunctionInvoker(function, "a", "b");
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("aa", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("ab", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("ba", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("bb", e.Current.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_int_1_string_func_and_0_int_argument()
        {
            var function = new Func<int, string, string>((x, y) => y + x);
            var sut = new SingleFunctionInvoker(function);
            var e = sut.GetEnumerator();
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_int_1_string_func_and_1_int_argument()
        {
            var function = new Func<int, string, string>((x, y) => y + x);
            var sut = new SingleFunctionInvoker(function, 1);
            var e = sut.GetEnumerator();
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_int_1_string_func_and_1_string_argument()
        {
            var function = new Func<int, string, string>((x, y) => y + x);
            var sut = new SingleFunctionInvoker(function, "a");
            var e = sut.GetEnumerator();
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_int_1_string_func_and_1_int_1_string_argument()
        {
            var function = new Func<int, string, string>((x, y) => y + x);
            var sut = new SingleFunctionInvoker(function, 1, "a");
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("a1", e.Current.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_int_1_string_func_and_2_int_1_string_argument()
        {
            var function = new Func<int, string, string>((x, y) => y + x);
            var sut = new SingleFunctionInvoker(function, 1, 2, "a");
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("a1", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("a2", e.Current.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_int_1_string_func_and_1_int_2_string_argument()
        {
            var function = new Func<int, string, string>((x, y) => y + x);
            var sut = new SingleFunctionInvoker(function, 1, "a", "b");
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("a1", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("b1", e.Current.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_int_1_string_func_and_2_int_2_string_argument()
        {
            var function = new Func<int, string, string>((x, y) => y + x);
            var sut = new SingleFunctionInvoker(function, 1, 2, "a", "b");
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("a1", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("b1", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("a2", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("b2", e.Current.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_int_1_string_func_and_2_int_2_string_messed_argument()
        {
            var function = new Func<int, string, string>((x, y) => y + x);
            var sut = new SingleFunctionInvoker(function, 1, "a", 2, "b");
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("a1", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("b1", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("a2", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("b2", e.Current.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_for_loop()
        {
            var function = new Func<int, string>(count => string.Join("", Enumerable.Range(0, count)));
            var sut = new SingleFunctionInvoker(function, 0, 1, 4, 6, 8);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("0", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("0123", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("012345", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("01234567", e.Current.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_for_loop_and_arguments_as_functions()
        {
            var function = new Func<Func<int>, string>(count => string.Join("", Enumerable.Range(0, count())));
            var possibleArguments = new Func<int>[]
            {
                () => 0,
                () => 1,
                () => 4,
                () => 6,
                () => 8
            };
            var sut = new SingleFunctionInvoker(function, possibleArguments);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("0", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("0123", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("012345", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("01234567", e.Current.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_for_loop_and_inner_function()
        {
            var function = new Func<int, Func<int, int>, string>((x, f) => string.Join("", Enumerable.Range(0, f(x))));
            var sut = new SingleFunctionInvoker(function, 0, 1, 3, new Func<int, int>(x => x * x), new Func<int, int>(x => x + x));
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("", e.Current.DynamicInvoke()); // 0*0
            IsTrue(e.MoveNext());
            AreEqual("", e.Current.DynamicInvoke()); // 0+0
            IsTrue(e.MoveNext());
            AreEqual("0", e.Current.DynamicInvoke()); // 1*1
            IsTrue(e.MoveNext());
            AreEqual("01", e.Current.DynamicInvoke()); // 1+1
            IsTrue(e.MoveNext());
            AreEqual("012345678", e.Current.DynamicInvoke()); // 3*3
            IsTrue(e.MoveNext());
            AreEqual("012345", e.Current.DynamicInvoke()); // 3+3
            IsFalse(e.MoveNext());
        }
    }
}
