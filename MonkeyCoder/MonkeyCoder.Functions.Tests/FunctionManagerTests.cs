using NUnit.Framework;
using System;
using System.Collections.ObjectModel;
using static NUnit.Framework.Assert;

namespace MonkeyCoder.Functions.Tests
{
    [TestFixture]
    public class FunctionManagerTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Throws_exception_when_argument_are_null()
        {
            new FunctionManager(null);
        }

        [Test]
        public void Works_with_empty_arguments()
        {
            var sut = new FunctionManager(new ReadOnlyCollection<object>(new object[] { }));
            var e = sut.GetEnumerator();
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_int()
        {
            var sut = new FunctionManager(new ReadOnlyCollection<object>(new object[] { 1 }));
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual(1, e.Current.Function());
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_2_ints()
        {
            var sut = new FunctionManager(new ReadOnlyCollection<object>(new object[] { 1, 2 }));
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual(1, e.Current.Function());
            IsTrue(e.MoveNext());
            AreEqual(2, e.Current.Function());
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_2_ints_1_string()
        {
            var sut = new FunctionManager(new ReadOnlyCollection<object>(new object[] { 1, 2, "a" }));
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual(1, e.Current.Function());
            IsTrue(e.MoveNext());
            AreEqual(2, e.Current.Function());
            IsTrue(e.MoveNext());
            AreEqual("a", e.Current.Function());
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_action()
        {
            var x = 0;
            var action = new Action(() => { x = 1; });
            var sut = new FunctionManager(new ReadOnlyCollection<object>(new object[] { action }));
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual(0, x);
            e.Current.Function();
            AreEqual(1, x);
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_2_actions()
        {
            var x = 0;
            var action1 = new Action(() => { x = 1; });
            var action2 = new Action(() => { x = 2; });
            var sut = new FunctionManager(new ReadOnlyCollection<object>(new object[] { action1, action2 }));
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual(0, x);
            e.Current.Function();
            AreEqual(1, x);
            IsTrue(e.MoveNext());
            AreEqual(1, x);
            e.Current.Function();
            AreEqual(2, x);
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_function()
        {
            var function = new Func<int>(() => 1);
            var sut = new FunctionManager(new ReadOnlyCollection<object>(new object[] { function }));
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual(1, e.Current.Function());
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_2_functions()
        {
            var function1 = new Func<int>(() => 1);
            var function2 = new Func<int>(() => 2);
            var sut = new FunctionManager(new ReadOnlyCollection<object>(new object[] { function1, function2 }));
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual(1, e.Current.Function());
            IsTrue(e.MoveNext());
            AreEqual(2, e.Current.Function());
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_action_function_int()
        {
            var x = 0;
            var action = new Action(() => { x = 1; });
            var function = new Func<int>(() => 2);
            var sut = new FunctionManager(new ReadOnlyCollection<object>(new object[] { action, function, 3 }));
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual(0, x);
            e.Current.Function();
            AreEqual(1, x);
            IsTrue(e.MoveNext());
            AreEqual(2, e.Current.Function());
            IsTrue(e.MoveNext());
            AreEqual(3, e.Current.Function());
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_action_taking_1_parameter_but_without_providing_any_matching_arguments()
        {
            var x = 0;
            var action = new Action<int>(y => { x = y; });
            var sut = new FunctionManager(new ReadOnlyCollection<object>(new object[] { action }));
            var e = sut.GetEnumerator();
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_action_taking_1_parameter_and_one_matching_argument()
        {
            var x = 0;
            var action = new Action<int>(y => { x = y; });
            var sut = new FunctionManager(new ReadOnlyCollection<object>(new object[] { action, 1 }));
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual(0, x);
            e.Current.Function();
            AreEqual(1, x);
            IsTrue(e.MoveNext());
            AreEqual(1, e.Current.Function());
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_action_taking_1_parameter_and_two_matching_arguments()
        {
            var x = 0;
            var action = new Action<int>(y => { x = y; });
            var sut = new FunctionManager(new ReadOnlyCollection<object>(new object[] { action, 1, 2 }));
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual(0, x);
            e.Current.Function();
            AreEqual(1, x);
            IsTrue(e.MoveNext());
            AreEqual(1, x);
            e.Current.Function();
            AreEqual(2, x);
            IsTrue(e.MoveNext());
            AreEqual(1, e.Current.Function());
            IsTrue(e.MoveNext());
            AreEqual(2, e.Current.Function());
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_function_taking_1_parameter_and_one_matching_argument()
        {
            var function = new Func<int, string>(x => x + "b");
            var sut = new FunctionManager(new ReadOnlyCollection<object>(new object[] { function, 1 }));
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("1b", e.Current.Function());
            IsTrue(e.MoveNext());
            AreEqual(1, e.Current.Function());
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_function_taking_1_parameter_and_two_matching_arguments()
        {
            var function = new Func<int, string>(x => x + "b");
            var sut = new FunctionManager(new ReadOnlyCollection<object>(new object[] { function, 1, 2 }));
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("1b", e.Current.Function());
            IsTrue(e.MoveNext());
            AreEqual("2b", e.Current.Function());
            IsTrue(e.MoveNext());
            AreEqual(1, e.Current.Function());
            IsTrue(e.MoveNext());
            AreEqual(2, e.Current.Function());
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_function_taking_1_parameter_and_two_matching_arguments_where_one_argument_is_another_function_and_stack_size_is_0()
        {
            var function1 = new Func<int, string>(x => x + "b");
            var function2 = new Func<int>(() => 1);
            var sut = new FunctionManager(new ReadOnlyCollection<object>(new object[] { function1, function2, 2 }), 0);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("2b", e.Current.Function());
            IsTrue(e.MoveNext());
            AreEqual(1, e.Current.Function());
            IsTrue(e.MoveNext());
            AreEqual(2, e.Current.Function());
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_function_taking_1_parameter_and_two_matching_arguments_where_one_argument_is_another_function_and_stack_size_is_1()
        {
            var function1 = new Func<int, string>(x => x + "b");
            var function2 = new Func<int>(() => 1);
            var sut = new FunctionManager(new ReadOnlyCollection<object>(new object[] { function1, function2, 2 }), 1);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("1b", e.Current.Function());
            IsTrue(e.MoveNext());
            AreEqual("2b", e.Current.Function());
            IsTrue(e.MoveNext());
            AreEqual(1, e.Current.Function());
            IsTrue(e.MoveNext());
            AreEqual(2, e.Current.Function());
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_2_functions_taking_1_parameter_where_stack_size_is_0()
        {
            var function1 = new Func<string, string>(x => "a" + x);
            var function2 = new Func<string, string>(x => "b" + x);
            var sut = new FunctionManager(new ReadOnlyCollection<object>(new object[] { function1, function2, "c" }), 0);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("ac", e.Current.Function());
            IsTrue(e.MoveNext());
            AreEqual("bc", e.Current.Function());
            IsTrue(e.MoveNext());
            AreEqual("c", e.Current.Function());
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_2_functions_taking_1_parameter_where_stack_size_is_1()
        {
            var function1 = new Func<string, string>(x => "a" + x);
            var function2 = new Func<string, string>(x => "b" + x);
            var sut = new FunctionManager(new ReadOnlyCollection<object>(new object[] { function1, function2, "c" }), 1);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("aac", e.Current.Function());
            IsTrue(e.MoveNext());
            AreEqual("abc", e.Current.Function());
            IsTrue(e.MoveNext());
            AreEqual("ac", e.Current.Function());
            IsTrue(e.MoveNext());
            AreEqual("bac", e.Current.Function());
            IsTrue(e.MoveNext());
            AreEqual("bbc", e.Current.Function());
            IsTrue(e.MoveNext());
            AreEqual("bc", e.Current.Function());
            IsTrue(e.MoveNext());
            AreEqual("c", e.Current.Function());
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_with_2_functions_taking_1_parameter_where_stack_size_is_2()
        {
            var function1 = new Func<string, string>(x => "a" + x);
            var function2 = new Func<string, string>(x => "b" + x);
            var sut = new FunctionManager(new ReadOnlyCollection<object>(new object[] { function1, function2, "c" }), 2);
            var e = sut.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("aaac", e.Current.Function());
            IsTrue(e.MoveNext());
            AreEqual("aabc", e.Current.Function());
            IsTrue(e.MoveNext());
            AreEqual("aac", e.Current.Function());
            IsTrue(e.MoveNext());
            AreEqual("abac", e.Current.Function());
            IsTrue(e.MoveNext());
            AreEqual("abbc", e.Current.Function());
            IsTrue(e.MoveNext());
            AreEqual("abc", e.Current.Function());
            IsTrue(e.MoveNext());
            AreEqual("ac", e.Current.Function());
            IsTrue(e.MoveNext());
            AreEqual("baac", e.Current.Function());
            IsTrue(e.MoveNext());
            AreEqual("babc", e.Current.Function());
            IsTrue(e.MoveNext());
            AreEqual("bac", e.Current.Function());
            IsTrue(e.MoveNext());
            AreEqual("bbac", e.Current.Function());
            IsTrue(e.MoveNext());
            AreEqual("bbbc", e.Current.Function());
            IsTrue(e.MoveNext());
            AreEqual("bbc", e.Current.Function());
            IsTrue(e.MoveNext());
            AreEqual("bc", e.Current.Function());
            IsTrue(e.MoveNext());
            AreEqual("c", e.Current.Function());
            IsFalse(e.MoveNext());
        }
    }
}
