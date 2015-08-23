using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace MonkeyCoder.Functions.Tests
{
    [TestClass]
    public class AppendableSingleFunctionInvokerTests : CommonSingleFunctionInvokerTests
    {
        internal override ISingleFunctionInvoker GetInvoker(Delegate function, params object[] possibleArguments) => new AppendableSingleFunctionInvoker(function, possibleArguments);

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Throws_argument_null_exception_when_function_is_null()
        {
            GetInvoker(null);
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
