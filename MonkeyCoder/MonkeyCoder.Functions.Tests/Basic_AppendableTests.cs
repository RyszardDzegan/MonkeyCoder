using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using static NUnit.Framework.Assert;

namespace MonkeyCoder.Functions.Tests
{
    [TestFixture]
    public class Basic_AppendableTests : CommonTests
    {
        internal override IEnumerable<Func<object>> GetInvoker(Delegate function, params object[] possibleArguments) =>
            new Basic.Appendable(function, possibleArguments);

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public override void Throws_exception_when_function_is_null()
        {
            base.Throws_exception_when_function_is_null();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public override void Throws_exception_when_possible_arguments_are_null()
        {
            base.Throws_exception_when_possible_arguments_are_null();
        }

        [Test]
        public override void Works_with_empty_action()
        {
            base.Works_with_empty_action();
        }

        [Test]
        public override void Works_with_1_int_action()
        {
            base.Works_with_1_int_action();
        }

        [Test]
        public override void Works_with_1_int_action_and_2_null_possible_arguments()
        {
            base.Works_with_1_int_action_and_2_null_possible_arguments();
        }

        [Test]
        public override void Works_with_1_string_action_and_2_null_possible_arguments()
        {
            base.Works_with_1_string_action_and_2_null_possible_arguments();
        }

        [Test]
        public override void Works_with_1_string_1_int_action_and_2_null_1_int_possible_arguments()
        {
            base.Works_with_1_string_1_int_action_and_2_null_1_int_possible_arguments();
        }

        [Test]
        public override void Works_with_1_string_1_nullable_int_action_and_2_null_1_int_possible_arguments()
        {
            base.Works_with_1_string_1_nullable_int_action_and_2_null_1_int_possible_arguments();
        }

        [Test]
        public override void Works_with_1_int_action_and_1_int_possible_argument()
        {
            base.Works_with_1_int_action_and_1_int_possible_argument();
        }

        [Test]
        public override void Works_with_1_int_action_and_2_int_possible_arguments()
        {
            base.Works_with_1_int_action_and_2_int_possible_arguments();
        }

        [Test]
        public override void Works_with_1_int_action_and_2_int_1_string_possible_arguments()
        {
            base.Works_with_1_int_action_and_2_int_1_string_possible_arguments();
        }

        [Test]
        public override void Works_with_1_int_func()
        {
            base.Works_with_1_int_func();
        }

        [Test]
        public override void Works_with_1_int_func_and_1_int_possible_argument()
        {
            base.Works_with_1_int_func_and_1_int_possible_argument();
        }

        [Test]
        public override void Works_with_1_int_func_and_2_int_possible_arguments()
        {
            base.Works_with_1_int_func_and_2_int_possible_arguments();
        }

        [Test]
        public override void Works_with_1_int_func_and_2_int_1_string_possible_arguments()
        {
            base.Works_with_1_int_func_and_2_int_1_string_possible_arguments();
        }

        [Test]
        public override void Works_with_2_int_func()
        {
            base.Works_with_2_int_func();
        }

        [Test]
        public override void Works_with_2_int_func_and_1_int_possible_argument()
        {
            base.Works_with_2_int_func_and_1_int_possible_argument();
        }

        [Test]
        public override void Works_with_2_string_func_and_2_string_possible_arguments()
        {
            base.Works_with_2_string_func_and_2_string_possible_arguments();
        }

        [Test]
        public override void Works_with_1_int_1_string_func()
        {
            base.Works_with_1_int_1_string_func();
        }

        [Test]
        public override void Works_with_1_int_1_string_func_and_1_int_possible_argument()
        {
            base.Works_with_1_int_1_string_func_and_1_int_possible_argument();
        }

        [Test]
        public override void Works_with_1_int_1_string_func_and_1_string_possible_argument()
        {
            base.Works_with_1_int_1_string_func_and_1_string_possible_argument();
        }

        [Test]
        public override void Works_with_1_int_1_string_func_and_1_int_1_string_possible_arguments()
        {
            base.Works_with_1_int_1_string_func_and_1_int_1_string_possible_arguments();
        }

        [Test]
        public override void Works_with_1_int_1_string_func_and_2_int_1_string_possible_arguments()
        {
            base.Works_with_1_int_1_string_func_and_2_int_1_string_possible_arguments();
        }

        [Test]
        public override void Works_with_1_int_1_string_func_and_1_int_2_string_possible_arguments()
        {
            base.Works_with_1_int_1_string_func_and_1_int_2_string_possible_arguments();
        }

        [Test]
        public override void Works_with_1_int_1_string_func_and_2_int_2_string_possible_arguments()
        {
            base.Works_with_1_int_1_string_func_and_2_int_2_string_possible_arguments();
        }

        [Test]
        public override void Works_with_1_int_1_string_func_and_2_int_2_string_messed_possible_arguments()
        {
            base.Works_with_1_int_1_string_func_and_2_int_2_string_messed_possible_arguments();
        }

        [Test]
        public override void Works_with_1_func_int_1_string_func_and_2_int_2_string_possible_arguments()
        {
            base.Works_with_1_func_int_1_string_func_and_2_int_2_string_possible_arguments();
        }

        [Test]
        public override void Works_with_1_func_int_1_string_func_and_1_int_1_func_int_2_string_possible_arguments()
        {
            base.Works_with_1_func_int_1_string_func_and_1_int_1_func_int_2_string_possible_arguments();
        }

        [Test]
        public override void Works_with_1_func_int_1_string_func_and_2_func_int_2_string_possible_arguments()
        {
            base.Works_with_1_func_int_1_string_func_and_2_func_int_2_string_possible_arguments();
        }

        [Test]
        public override void Works_with_1_func_int_1_func_string_func_and_2_func_int_2_func_string_possible_arguments()
        {
            base.Works_with_1_func_int_1_func_string_func_and_2_func_int_2_func_string_possible_arguments();
        }

        [Test]
        public override void Works_with_complex_possible_arguments()
        {
            base.Works_with_complex_possible_arguments();
        }

        [Test]
        public override void Works_with_for_loop()
        {
            base.Works_with_for_loop();
        }

        [Test]
        public override void Works_with_for_loop_and_arguments_as_functions()
        {
            base.Works_with_for_loop_and_arguments_as_functions();
        }

        [Test]
        public override void Works_with_for_loop_and_inner_function()
        {
            base.Works_with_for_loop_and_inner_function();
        }

        [Test]
        public override void Works_with_two_enumerators()
        {
            base.Works_with_two_enumerators();
        }

        [Test]
        public void Works_with_two_consecutive_iterations()
        {
            var function = new Func<string, string, string>((x, y) => x + y);
            var sut = new Basic.Appendable(function, "a", "b");
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

        [Test]
        public void Works_when_add_argument_during_iteration()
        {
            var function = new Func<string, string, string>((x, y) => x + y);
            var sut = new Basic.Appendable(function, "a", "b");
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

        [Test]
        public void Works_when_adding_argument_during_consuming_iteration()
        {
            var function = new Func<string, string, string>((x, y) => x + y);
            var sut = new Basic.Appendable(function, "a", "b");
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

        [Test]
        public void Works_when_adding_two_arguments_during_consuming_iteration()
        {
            var function = new Func<string, string, string>((x, y) => x + y);
            var sut = new Basic.Appendable(function, "a", "b");
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

        [Test]
        public void Adding_argument_when_invoker_is_completed_has_no_effect()
        {
            var function = new Func<string, string, string>((x, y) => x + y);
            var sut = new Basic.Appendable(function, "a", "b");
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

        [Test]
        [ExpectedException(typeof(OperationCanceledException))]
        public void Cancelling_works()
        {
            var function = new Func<string, string, string>((x, y) => x + y);
            var sut = new Basic.Appendable(function, "a", "b");
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

        [Test]
        public void Works_when_adding_argument_from_other_thread_during_consuming_iteration()
        {
            var function = new Func<string, string, string>((x, y) => x + y);
            var sut = new Basic.Appendable(function, "a", "b");
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
