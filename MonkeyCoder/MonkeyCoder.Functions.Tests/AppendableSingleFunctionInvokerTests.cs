using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace MonkeyCoder.Functions.Tests
{
    [TestClass]
    public class AppendableSingleFunctionInvokerTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Throws_exception_when_function_is_null()
        {
            new AppendableSingleFunctionInvoker(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Throws_exception_when_possible_arguments_are_null()
        {
            var function = new Action<int>(x => { });
            new AppendableSingleFunctionInvoker(function, null);
        }

        [TestMethod]
        public void Works_with_empty_action()
        {
            var function = new Action(() => { });
            var sut = new AppendableSingleFunctionInvoker(function);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            IsFalse(e.Current.Method.GetParameters().Any());
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_int_action()
        {
            var function = new Action<int>(x => { });
            var sut = new AppendableSingleFunctionInvoker(function);
            var e = sut.GetEnumerator();
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_int_action_and_2_null_possible_arguments()
        {
            var function = new Action<int>(x => { });
            var sut = new AppendableSingleFunctionInvoker(function, null, null);
            var e = sut.GetEnumerator();
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_string_action_and_2_null_possible_arguments()
        {
            var result = "";
            var function = new Action<string>(x => { result = x; });
            var sut = new AppendableSingleFunctionInvoker(function, null, null);
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
            var sut = new AppendableSingleFunctionInvoker(function, null, null, 1);
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
        public void Works_with_1_string_1_nullable_int_action_and_2_null_1_int_possible_arguments()
        {
            var result = "";
            var function = new Action<int?, string>((x, y) => { result = y + x; });
            var sut = new AppendableSingleFunctionInvoker(function, null, null, 1);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            e.Current.DynamicInvoke();
            AreEqual("", result);
            IsTrue(e.MoveNext());
            e.Current.DynamicInvoke();
            AreEqual("", result);
            IsTrue(e.MoveNext());
            e.Current.DynamicInvoke();
            AreEqual("", result);
            IsTrue(e.MoveNext());
            e.Current.DynamicInvoke();
            AreEqual("", result);
            IsTrue(e.MoveNext());
            e.Current.DynamicInvoke();
            AreEqual("1", result);
            IsTrue(e.MoveNext());
            e.Current.DynamicInvoke();
            AreEqual("1", result);
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_int_action_and_1_int_possible_argument()
        {
            var result = -1;
            var function = new Action<int>(x => { result = x; });
            var sut = new AppendableSingleFunctionInvoker(function, 1);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            e.Current.DynamicInvoke();
            AreEqual(1, result);
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_int_action_and_2_int_possible_arguments()
        {
            var result = -1;
            var function = new Action<int>(x => { result = x; });
            var sut = new AppendableSingleFunctionInvoker(function, 1, 2);
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
        public void Works_with_1_int_action_and_2_int_1_string_possible_arguments()
        {
            object result = -1;
            var function = new Action<int>(x => { result = x; });
            var sut = new AppendableSingleFunctionInvoker(function, 1, 2, "a");
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
        public void Works_with_1_int_func()
        {
            var function = new Func<int, int>(x => x);
            var sut = new AppendableSingleFunctionInvoker(function);
            var e = sut.GetEnumerator();
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_int_func_and_1_int_possible_argument()
        {
            var function = new Func<int, int>(x => x);
            var sut = new AppendableSingleFunctionInvoker(function, 1);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual(1, e.Current.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_int_func_and_2_int_possible_arguments()
        {
            var function = new Func<int, int>(x => x);
            var sut = new AppendableSingleFunctionInvoker(function, 1, 2);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual(1, e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual(2, e.Current.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_int_func_and_2_int_1_string_possible_arguments()
        {
            var function = new Func<int, int>(x => x);
            var sut = new AppendableSingleFunctionInvoker(function, 1, 2, "a");
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual(1, e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual(2, e.Current.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_2_int_func()
        {
            var function = new Func<int, int, int>((x, y) => x + y);
            var sut = new AppendableSingleFunctionInvoker(function);
            var e = sut.GetEnumerator();
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_2_int_func_and_1_int_possible_argument()
        {
            var function = new Func<int, int, int>((x, y) => x + y);
            var sut = new AppendableSingleFunctionInvoker(function, 1);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual(2, e.Current.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_2_string_func_and_2_string_possible_arguments()
        {
            var function = new Func<string, string, string>((x, y) => x + y);
            var sut = new AppendableSingleFunctionInvoker(function, "a", "b");
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
        public void Works_with_1_int_1_string_func()
        {
            var function = new Func<int, string, string>((x, y) => y + x);
            var sut = new AppendableSingleFunctionInvoker(function);
            var e = sut.GetEnumerator();
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_int_1_string_func_and_1_int_possible_argument()
        {
            var function = new Func<int, string, string>((x, y) => y + x);
            var sut = new AppendableSingleFunctionInvoker(function, 1);
            var e = sut.GetEnumerator();
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_int_1_string_func_and_1_string_possible_argument()
        {
            var function = new Func<int, string, string>((x, y) => y + x);
            var sut = new AppendableSingleFunctionInvoker(function, "a");
            var e = sut.GetEnumerator();
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_int_1_string_func_and_1_int_1_string_possible_arguments()
        {
            var function = new Func<int, string, string>((x, y) => y + x);
            var sut = new AppendableSingleFunctionInvoker(function, 1, "a");
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("a1", e.Current.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_int_1_string_func_and_2_int_1_string_possible_arguments()
        {
            var function = new Func<int, string, string>((x, y) => y + x);
            var sut = new AppendableSingleFunctionInvoker(function, 1, 2, "a");
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("a1", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("a2", e.Current.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_int_1_string_func_and_1_int_2_string_possible_arguments()
        {
            var function = new Func<int, string, string>((x, y) => y + x);
            var sut = new AppendableSingleFunctionInvoker(function, 1, "a", "b");
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("a1", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("b1", e.Current.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_1_int_1_string_func_and_2_int_2_string_possible_arguments()
        {
            var function = new Func<int, string, string>((x, y) => y + x);
            var sut = new AppendableSingleFunctionInvoker(function, 1, 2, "a", "b");
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
        public void Works_with_1_int_1_string_func_and_2_int_2_string_messed_possible_arguments()
        {
            var function = new Func<int, string, string>((x, y) => y + x);
            var sut = new AppendableSingleFunctionInvoker(function, 1, "a", 2, "b");
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
            var sut = new AppendableSingleFunctionInvoker(function, 0, 1, 4, 6, 8);
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
            var sut = new AppendableSingleFunctionInvoker(function, possibleArguments);
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
            var sut = new AppendableSingleFunctionInvoker(function, 0, 1, 3, new Func<int, int>(x => x * x), new Func<int, int>(x => x + x));
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

        [TestMethod]
        public void Works_with_two_consecutive_iterations()
        {
            var function = new Func<string, string, string>((x, y) => x + y);
            var sut = new AppendableSingleFunctionInvoker(function, "a", "b");
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
            e = sut.GetEnumerator();
            sut.Add("c");
            IsTrue(e.MoveNext());
            AreEqual("aa", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("ab", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("ba", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("bb", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("ca", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("cb", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("ac", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("bc", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("cc", e.Current.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_when_add_argument_during_iteration()
        {
            var function = new Func<string, string, string>((x, y) => x + y);
            var sut = new AppendableSingleFunctionInvoker(function, "a", "b");
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("aa", e.Current.DynamicInvoke());
            sut.Add("c");
            IsTrue(e.MoveNext());
            AreEqual("ab", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("ba", e.Current.DynamicInvoke());
            sut.Add("d");
            IsTrue(e.MoveNext());
            AreEqual("bb", e.Current.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_with_two_enumerators()
        {
            var function = new Func<string, string, string>((x, y) => x + y);
            var sut = new AppendableSingleFunctionInvoker(function, "a", "b");
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

        [TestMethod]
        public void Works_when_adding_argument_during_consuming_iteration()
        {
            var function = new Func<string, string, string>((x, y) => x + y);
            var sut = new AppendableSingleFunctionInvoker(function, "a", "b");
            var e = sut.GetConsumingEnumerable().GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("aa", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("ab", e.Current.DynamicInvoke());
            sut.Add("c");
            sut.Complete();
            IsTrue(e.MoveNext());
            AreEqual("ba", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("bb", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("ca", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("cb", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("ac", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("bc", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("cc", e.Current.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_when_adding_two_arguments_during_consuming_iteration()
        {
            var function = new Func<string, string, string>((x, y) => x + y);
            var sut = new AppendableSingleFunctionInvoker(function, "a", "b");
            var e = sut.GetConsumingEnumerable().GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("aa", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("ab", e.Current.DynamicInvoke());
            sut.Add("c");
            sut.Add("d");
            sut.Complete();
            IsTrue(e.MoveNext());
            AreEqual("ba", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("bb", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("ca", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("cb", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("ac", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("bc", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("cc", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("da", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("db", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("dc", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("ad", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("bd", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("cd", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("dd", e.Current.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Adding_argument_when_invoker_is_completed_has_no_effect()
        {
            var function = new Func<string, string, string>((x, y) => x + y);
            var sut = new AppendableSingleFunctionInvoker(function, "a", "b");
            var e = sut.GetConsumingEnumerable().GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("aa", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("ab", e.Current.DynamicInvoke());
            sut.Add("c");
            sut.Complete();
            sut.Add("d");
            sut.Add("e");
            IsTrue(e.MoveNext());
            AreEqual("ba", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("bb", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("ca", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("cb", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("ac", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("bc", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("cc", e.Current.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        [ExpectedException(typeof(OperationCanceledException))]
        public void Cancelling_works()
        {
            var function = new Func<string, string, string>((x, y) => x + y);
            var sut = new AppendableSingleFunctionInvoker(function, "a", "b");
            var cts = new CancellationTokenSource();
            var e = sut.GetConsumingEnumerable(cts.Token).GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("aa", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("ab", e.Current.DynamicInvoke());
            sut.Add("c");
            IsTrue(e.MoveNext());
            AreEqual("ba", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("bb", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("ca", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("cb", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("ac", e.Current.DynamicInvoke());
            cts.Cancel();
            IsTrue(e.MoveNext());
            AreEqual("bc", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("cc", e.Current.DynamicInvoke());
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_when_adding_argument_from_other_thread_during_consuming_iteration()
        {
            var function = new Func<string, string, string>((x, y) => x + y);
            var sut = new AppendableSingleFunctionInvoker(function, "a", "b");
            var wh = new ManualResetEventSlim();
            var e = sut.GetConsumingEnumerable().GetEnumerator();

            IsTrue(e.MoveNext());
            AreEqual("aa", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("ab", e.Current.DynamicInvoke());

            Task.Run(() =>
            {
                sut.Add("c");
                sut.Complete();
                wh.Set();
            });

            if (!wh.Wait(500))
                throw new TimeoutException();

            IsTrue(e.MoveNext());
            AreEqual("ba", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("bb", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("ca", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("cb", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("ac", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("bc", e.Current.DynamicInvoke());
            IsTrue(e.MoveNext());
            AreEqual("cc", e.Current.DynamicInvoke());
            IsFalse(e.MoveNext());
        }
    }
}
