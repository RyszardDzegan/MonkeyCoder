using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using static Microsoft.VisualStudio.TestTools.UnitTesting.StringAssert;

namespace MonkeyCoder.Functions.Tests
{
    using static TestHelpers.StaticExpectedOutputReader;

    [TestClass]
    public class Basic_MultipleTests : CommonTests
    {
        internal override IEnumerable<Func<object>> GetInvoker(Delegate function, params object[] possibleArguments) => new Basic.Multiple(new Delegate[] { function }, possibleArguments);

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public override void Throws_exception_when_function_is_null()
        {
            base.Throws_exception_when_function_is_null();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public override void Throws_exception_when_possible_arguments_are_null()
        {
            base.Throws_exception_when_possible_arguments_are_null();
        }

        [TestMethod]
        public override void Works_with_empty_action()
        {
            base.Works_with_empty_action();
        }

        [TestMethod]
        public override void Works_with_1_int_action()
        {
            base.Works_with_1_int_action();
        }

        [TestMethod]
        public override void Works_with_1_int_action_and_2_null_possible_arguments()
        {
            base.Works_with_1_int_action_and_2_null_possible_arguments();
        }

        [TestMethod]
        public override void Works_with_1_string_action_and_2_null_possible_arguments()
        {
            base.Works_with_1_string_action_and_2_null_possible_arguments();
        }

        [TestMethod]
        public override void Works_with_1_string_1_int_action_and_2_null_1_int_possible_arguments()
        {
            base.Works_with_1_string_1_int_action_and_2_null_1_int_possible_arguments();
        }

        [TestMethod]
        public override void Works_with_1_string_1_nullable_int_action_and_2_null_1_int_possible_arguments()
        {
            base.Works_with_1_string_1_nullable_int_action_and_2_null_1_int_possible_arguments();
        }

        [TestMethod]
        public override void Works_with_1_int_action_and_1_int_possible_argument()
        {
            base.Works_with_1_int_action_and_1_int_possible_argument();
        }

        [TestMethod]
        public override void Works_with_1_int_action_and_2_int_possible_arguments()
        {
            base.Works_with_1_int_action_and_2_int_possible_arguments();
        }

        [TestMethod]
        public override void Works_with_1_int_action_and_2_int_1_string_possible_arguments()
        {
            base.Works_with_1_int_action_and_2_int_1_string_possible_arguments();
        }

        [TestMethod]
        public override void Works_with_1_int_func()
        {
            base.Works_with_1_int_func();
        }

        [TestMethod]
        public override void Works_with_1_int_func_and_1_int_possible_argument()
        {
            base.Works_with_1_int_func_and_1_int_possible_argument();
        }

        [TestMethod]
        public override void Works_with_1_int_func_and_2_int_possible_arguments()
        {
            base.Works_with_1_int_func_and_2_int_possible_arguments();
        }

        [TestMethod]
        public override void Works_with_1_int_func_and_2_int_1_string_possible_arguments()
        {
            base.Works_with_1_int_func_and_2_int_1_string_possible_arguments();
        }

        [TestMethod]
        public override void Works_with_2_int_func()
        {
            base.Works_with_2_int_func();
        }

        [TestMethod]
        public override void Works_with_2_int_func_and_1_int_possible_argument()
        {
            base.Works_with_2_int_func_and_1_int_possible_argument();
        }

        [TestMethod]
        public override void Works_with_2_string_func_and_2_string_possible_arguments()
        {
            base.Works_with_2_string_func_and_2_string_possible_arguments();
        }

        [TestMethod]
        public override void Works_with_1_int_1_string_func()
        {
            base.Works_with_1_int_1_string_func();
        }

        [TestMethod]
        public override void Works_with_1_int_1_string_func_and_1_int_possible_argument()
        {
            base.Works_with_1_int_1_string_func_and_1_int_possible_argument();
        }

        [TestMethod]
        public override void Works_with_1_int_1_string_func_and_1_string_possible_argument()
        {
            base.Works_with_1_int_1_string_func_and_1_string_possible_argument();
        }

        [TestMethod]
        public override void Works_with_1_int_1_string_func_and_1_int_1_string_possible_arguments()
        {
            base.Works_with_1_int_1_string_func_and_1_int_1_string_possible_arguments();
        }

        [TestMethod]
        public override void Works_with_1_int_1_string_func_and_2_int_1_string_possible_arguments()
        {
            base.Works_with_1_int_1_string_func_and_2_int_1_string_possible_arguments();
        }

        [TestMethod]
        public override void Works_with_1_int_1_string_func_and_1_int_2_string_possible_arguments()
        {
            base.Works_with_1_int_1_string_func_and_1_int_2_string_possible_arguments();
        }

        [TestMethod]
        public override void Works_with_1_int_1_string_func_and_2_int_2_string_possible_arguments()
        {
            base.Works_with_1_int_1_string_func_and_2_int_2_string_possible_arguments();
        }

        [TestMethod]
        public override void Works_with_1_int_1_string_func_and_2_int_2_string_messed_possible_arguments()
        {
            base.Works_with_1_int_1_string_func_and_2_int_2_string_messed_possible_arguments();
        }

        [TestMethod]
        public override void Works_with_1_func_int_1_string_func_and_2_int_2_string_possible_arguments()
        {
            base.Works_with_1_func_int_1_string_func_and_2_int_2_string_possible_arguments();
        }

        [TestMethod]
        public override void Works_with_1_func_int_1_string_func_and_1_int_1_func_int_2_string_possible_arguments()
        {
            base.Works_with_1_func_int_1_string_func_and_1_int_1_func_int_2_string_possible_arguments();
        }

        [TestMethod]
        public override void Works_with_1_func_int_1_string_func_and_2_func_int_2_string_possible_arguments()
        {
            base.Works_with_1_func_int_1_string_func_and_2_func_int_2_string_possible_arguments();
        }

        [TestMethod]
        public override void Works_with_1_func_int_1_func_string_func_and_2_func_int_2_func_string_possible_arguments()
        {
            base.Works_with_1_func_int_1_func_string_func_and_2_func_int_2_func_string_possible_arguments();
        }

        [TestMethod]
        public override void Works_with_complex_possible_arguments()
        {
            base.Works_with_complex_possible_arguments();
        }

        [TestMethod]
        public override void Works_with_for_loop()
        {
            base.Works_with_for_loop();
        }

        [TestMethod]
        public override void Works_with_for_loop_and_arguments_as_functions()
        {
            base.Works_with_for_loop_and_arguments_as_functions();
        }

        [TestMethod]
        public override void Works_with_for_loop_and_inner_function()
        {
            base.Works_with_for_loop_and_inner_function();
        }

        [TestMethod]
        public override void Works_with_two_enumerators()
        {
            base.Works_with_two_enumerators();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Throws_argument_exception_when_function_is_null()
        {
            try
            {
                GetInvoker(null);
            }
            catch (Exception exception)
            {
                StartsWith(exception.Message, "All functions must be not null. Found nulls at positions: 0");
                throw;
            }
        }

        [TestMethod]
        public void Works_with_function_2_int_and_function_2_string()
        {
            var f1 = new Func<int, int>(x => x);
            var f2 = new Func<string, string>(x => x);
            var sut = new Basic.Multiple(new Delegate[] { f1, f2 });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_function_2_int_and_function_2_string_and_1_int_possible_argument()
        {
            var f1 = new Func<int, int>(x => x);
            var f2 = new Func<string, string>(x => x);
            var sut = new Basic.Multiple(new Delegate[] { f1, f2 }, 1);
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_function_2_int_and_function_2_string_and_1_int_1_string_possible_arguments()
        {
            var f1 = new Func<int, int>(x => x);
            var f2 = new Func<string, string>(x => x);
            var sut = new Basic.Multiple(new Delegate[] { f1, f2 }, 1, "a");
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_function_2_int_and_function_2_string_and_1_string_1_int_possible_arguments()
        {
            var f1 = new Func<int, int>(x => x);
            var f2 = new Func<string, string>(x => x);
            var sut = new Basic.Multiple(new Delegate[] { f1, f2 }, "a", 1);
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_function_2_string_and_function_2_int_and_1_int_1_string_possible_arguments()
        {
            var f1 = new Func<string, string>(x => x);
            var f2 = new Func<int, int>(x => x);
            var sut = new Basic.Multiple(new Delegate[] { f1, f2 }, 1, "a");
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_function_2_string_and_function_2_int_and_1_string_1_int_possible_arguments()
        {
            var f1 = new Func<string, string>(x => x);
            var f2 = new Func<int, int>(x => x);
            var sut = new Basic.Multiple(new Delegate[] { f1, f2 }, "a", 1);
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_function_2_int_and_function_2_string_and_2_int_2_string_possible_arguments()
        {
            var f1 = new Func<int, int>(x => x);
            var f2 = new Func<string, string>(x => x);
            var sut = new Basic.Multiple(new Delegate[] { f1, f2 }, 1, 2, "a", "b");
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_function_string_int_string_and_function_2_string_and_2_int_2_string_possible_arguments()
        {
            var f1 = new Func<string, int, string>((x, y) => x + y);
            var f2 = new Func<string, string>(x => x);
            var sut = new Basic.Multiple(new Delegate[] { f1, f2 }, 1, 2, "a", "b");
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_function_string_int_string_and_function_2_string_and_function_2_int_and_2_int_2_string_possible_arguments()
        {
            var f1 = new Func<string, int, string>((x, y) => x + y);
            var f2 = new Func<string, string>(x => x);
            var f3 = new Func<int, int>(x => x);
            var sut = new Basic.Multiple(new Delegate[] { f1, f2, f3 }, 1, 2, "a", "b");
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }
    }
}
