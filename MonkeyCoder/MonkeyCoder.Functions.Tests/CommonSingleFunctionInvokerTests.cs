using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace MonkeyCoder.Functions.Tests
{
    using static TestHelpers.StaticExpectedOutputReader;

    public abstract class CommonSingleFunctionInvokerTests : FunctionInvokerTestsBase
    {
        internal abstract ISingleFunctionInvoker GetInvoker(Delegate function, params object[] possibleArguments);
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Throws_exception_when_possible_arguments_are_null()
        {
            var function = new Action<int>(x => { });
            GetInvoker(function, null);
        }

        [TestMethod]
        public void Works_with_empty_action()
        {
            var function = new Action(() => { });
            var sut = GetInvoker(function);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            IsFalse(e.Current.Method.GetParameters().Any());
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_int_action()
        {
            var function = new Action<int>(x => { });
            var sut = GetInvoker(function);
            var e = sut.GetEnumerator();
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_int_action_and_2_null_possible_arguments()
        {
            var function = new Action<int>(x => { });
            var sut = GetInvoker(function);
            var e = sut.GetEnumerator();
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_string_action_and_2_null_possible_arguments()
        {
            object result = -1;
            var function = new Action<string>(x => { result = x; });
            var sut = GetInvoker(function, null, null);
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
        public void Works_with_1_string_1_int_action_and_2_null_1_int_possible_arguments()
        {
            var result = "";
            var function = new Action<int, string>((x, y) => { result = y + x; });
            var sut = GetInvoker(function, null, null, 1);
            GenerateOutput(sut, ref result);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_1_string_1_nullable_int_action_and_2_null_1_int_possible_arguments()
        {
            var result = "";
            var function = new Action<int?, string>((x, y) => { result = y + x; });
            var sut = GetInvoker(function, null, null, 1);
            GenerateOutput(sut, ref result);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_1_int_action_and_1_int_possible_argument()
        {
            var result = -1;
            var function = new Action<int>(x => { result = x; });
            var sut = GetInvoker(function, 1);
            GenerateOutput(sut, ref result);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_1_int_action_and_2_int_possible_arguments()
        {
            var result = -1;
            var function = new Action<int>(x => { result = x; });
            var sut = GetInvoker(function, 1, 2);
            GenerateOutput(sut, ref result);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_1_int_action_and_2_int_1_string_possible_arguments()
        {
            object result = -1;
            var function = new Action<int>(x => { result = x; });
            var sut = GetInvoker(function, 1, 2, "a");
            GenerateOutput(sut, ref result);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_1_int_func()
        {
            var function = new Func<int, int>(x => x);
            var sut = GetInvoker(function);
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_1_int_func_and_1_int_possible_argument()
        {
            var function = new Func<int, int>(x => x);
            var sut = GetInvoker(function, 1);
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_1_int_func_and_2_int_possible_arguments()
        {
            var function = new Func<int, int>(x => x);
            var sut = GetInvoker(function, 1, 2);
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_1_int_func_and_2_int_1_string_possible_arguments()
        {
            var function = new Func<int, int>(x => x);
            var sut = GetInvoker(function, 1, 2, "a");
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_2_int_func()
        {
            var function = new Func<int, int, int>((x, y) => x + y);
            var sut = GetInvoker(function);
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_2_int_func_and_1_int_possible_argument()
        {
            var function = new Func<int, int, int>((x, y) => x + y);
            var sut = GetInvoker(function, 1);
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_2_string_func_and_2_string_possible_arguments()
        {
            var function = new Func<string, string, string>((x, y) => x + y);
            var sut = GetInvoker(function, "a", "b");
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_1_int_1_string_func()
        {
            var function = new Func<int, string, string>((x, y) => y + x);
            var sut = GetInvoker(function);
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_1_int_1_string_func_and_1_int_possible_argument()
        {
            var function = new Func<int, string, string>((x, y) => y + x);
            var sut = GetInvoker(function, 1);
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_1_int_1_string_func_and_1_string_possible_argument()
        {
            var function = new Func<int, string, string>((x, y) => y + x);
            var sut = GetInvoker(function, "a");
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_1_int_1_string_func_and_1_int_1_string_possible_arguments()
        {
            var function = new Func<int, string, string>((x, y) => y + x);
            var sut = GetInvoker(function, 1, "a");
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_1_int_1_string_func_and_2_int_1_string_possible_arguments()
        {
            var function = new Func<int, string, string>((x, y) => y + x);
            var sut = GetInvoker(function, 1, 2, "a");
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_1_int_1_string_func_and_1_int_2_string_possible_arguments()
        {
            var function = new Func<int, string, string>((x, y) => y + x);
            var sut = GetInvoker(function, 1, "a", "b");
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_1_int_1_string_func_and_2_int_2_string_possible_arguments()
        {
            var function = new Func<int, string, string>((x, y) => y + x);
            var sut = GetInvoker(function, 1, 2, "a", "b");
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_1_int_1_string_func_and_2_int_2_string_messed_possible_arguments()
        {
            var function = new Func<int, string, string>((x, y) => y + x);
            var sut = GetInvoker(function, 1, "a", 2, "b");
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_1_func_int_1_string_func_and_2_int_2_string_possible_arguments()
        {
            var function = new Func<Func<int>, string, string>((x, y) => y + x());
            var sut = GetInvoker(function, 1, 2, "a", "b");
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_1_func_int_1_string_func_and_1_int_1_func_int_2_string_possible_arguments()
        {
            var function = new Func<Func<int>, string, string>((x, y) => y + x());
            var sut = GetInvoker(function, 1, new Func<int>(() => 2), "a", "b");
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_1_func_int_1_string_func_and_2_func_int_2_string_possible_arguments()
        {
            var function = new Func<Func<int>, string, string>((x, y) => y + x());
            var sut = GetInvoker(function, new Func<int>(() => 1), new Func<int>(() => 2), "a", "b");
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_1_func_int_1_func_string_func_and_2_func_int_2_func_string_possible_arguments()
        {
            var function = new Func<Func<int>, Func<string>, string>((x, y) => y() + x());
            var sut = GetInvoker(function, new Func<int>(() => 1), new Func<int>(() => 2), new Func<string>(() => "a"), new Func<string>(() => "b"));
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public virtual void Works_with_complex_possible_arguments()
        {
            var function = new Func<string, Func<string>, int, int?, Func<int>, Func<int?>, string>(
                (a, b, c, d, e, f) => (a != null ? a : "A") + (b != null ? b() : "B") + c + (d != null ? d.Value.ToString() : "D") + (e != null ? e().ToString() : "E") + (f != null ? f().Value.ToString() : "F"));
            var sut = GetInvoker(function, "a", new Func<string>(() => "b"), 3, 4, new Func<int>(() => 5), new Func<int?>(() => 6), null);
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_for_loop()
        {
            var function = new Func<int, string>(count => string.Join("", Enumerable.Range(0, count)));
            var sut = GetInvoker(function, 0, 1, 4, 6, 8);
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
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
            var sut = GetInvoker(function, possibleArguments);
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_for_loop_and_inner_function()
        {
            var function = new Func<int, Func<int, int>, string>((x, f) => string.Join("", Enumerable.Range(0, f(x))));
            var sut = GetInvoker(function, 0, 1, 3, new Func<int, int>(x => x * x), new Func<int, int>(x => x + x));
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_two_enumerators()
        {
            var function = new Func<string, string, string>((x, y) => x + y);
            var sut = GetInvoker(function, "a", "b");
            var e1 = sut.GetEnumerator();
            var e2 = sut.GetEnumerator();
            IsTrue(e1.MoveNext());
            IsTrue(e2.MoveNext());
            AreEqual("aa", e1.Current.DynamicInvoke());
            AreEqual("aa", e2.Current.DynamicInvoke());
            IsTrue(e1.MoveNext());
            IsTrue(e2.MoveNext());
            AreEqual("ab", e1.Current.DynamicInvoke());
            AreEqual("ab", e2.Current.DynamicInvoke());
            IsTrue(e1.MoveNext());
            IsTrue(e2.MoveNext());
            AreEqual("ba", e1.Current.DynamicInvoke());
            AreEqual("ba", e2.Current.DynamicInvoke());
            IsTrue(e1.MoveNext());
            IsTrue(e2.MoveNext());
            AreEqual("bb", e1.Current.DynamicInvoke());
            AreEqual("bb", e2.Current.DynamicInvoke());
            IsFalse(e1.MoveNext());
            IsFalse(e2.MoveNext());
        }
    }
}
