using NUnit.Framework;
using System;
using System.Collections.Generic;

using static NUnit.Framework.Assert;
using static NUnit.Framework.StringAssert;

namespace MonkeyCoder.Functions.Tests
{
    using Internals;
    using System.Linq;
    using static TestHelpers.StaticExpectedOutputReader;

    [TestFixture]
    public class Basic_MultipleTests : CommonTests
    {
        internal override IEnumerable<Func<object>> GetInvoker(Delegate function, params object[] possibleArguments) =>
            new Basic.Multiple(new Delegate[] { function }, possibleArguments).Select(x => x.Function);

        [Test]
        [ExpectedException(typeof(ArgumentException))]
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
        [ExpectedException(typeof(ArgumentException))]
        public void Throws_argument_exception_when_function_is_null()
        {
            try
            {
                GetInvoker(null);
            }
            catch (Exception exception)
            {
                StartsWith("All functions must be not null. Found nulls at positions: 0", exception.Message);
                throw;
            }
        }

        [Test]
        public void Works_with_function_2_int_and_function_2_string()
        {
            var f1 = new Func<int, int>(x => x);
            var f2 = new Func<string, string>(x => x);
            var sut = new Basic.Multiple(new Delegate[] { f1, f2 }).Select(x => x.Function);
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [Test]
        public void Works_with_function_2_int_and_function_2_string_and_1_int_possible_argument()
        {
            var f1 = new Func<int, int>(x => x);
            var f2 = new Func<string, string>(x => x);
            var sut = new Basic.Multiple(new Delegate[] { f1, f2 }, 1).Select(x => x.Function);
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [Test]
        public void Works_with_function_2_int_and_function_2_string_and_1_int_1_string_possible_arguments()
        {
            var f1 = new Func<int, int>(x => x);
            var f2 = new Func<string, string>(x => x);
            var sut = new Basic.Multiple(new Delegate[] { f1, f2 }, 1, "a").Select(x => x.Function);
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [Test]
        public void Works_with_function_2_int_and_function_2_string_and_1_string_1_int_possible_arguments()
        {
            var f1 = new Func<int, int>(x => x);
            var f2 = new Func<string, string>(x => x);
            var sut = new Basic.Multiple(new Delegate[] { f1, f2 }, "a", 1).Select(x => x.Function);
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [Test]
        public void Works_with_function_2_string_and_function_2_int_and_1_int_1_string_possible_arguments()
        {
            var f1 = new Func<string, string>(x => x);
            var f2 = new Func<int, int>(x => x);
            var sut = new Basic.Multiple(new Delegate[] { f1, f2 }, 1, "a").Select(x => x.Function);
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [Test]
        public void Works_with_function_2_string_and_function_2_int_and_1_string_1_int_possible_arguments()
        {
            var f1 = new Func<string, string>(x => x);
            var f2 = new Func<int, int>(x => x);
            var sut = new Basic.Multiple(new Delegate[] { f1, f2 }, "a", 1).Select(x => x.Function);
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [Test]
        public void Works_with_function_2_int_and_function_2_string_and_2_int_2_string_possible_arguments()
        {
            var f1 = new Func<int, int>(x => x);
            var f2 = new Func<string, string>(x => x);
            var sut = new Basic.Multiple(new Delegate[] { f1, f2 }, 1, 2, "a", "b").Select(x => x.Function);
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [Test]
        public void Works_with_function_string_int_string_and_function_2_string_and_2_int_2_string_possible_arguments()
        {
            var f1 = new Func<string, int, string>((x, y) => x + y);
            var f2 = new Func<string, string>(x => x);
            var sut = new Basic.Multiple(new Delegate[] { f1, f2 }, 1, 2, "a", "b").Select(x => x.Function);
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [Test]
        public void Works_with_function_string_int_string_and_function_2_string_and_function_2_int_and_2_int_2_string_possible_arguments()
        {
            var f1 = new Func<string, int, string>((x, y) => x + y);
            var f2 = new Func<string, string>(x => x);
            var f3 = new Func<int, int>(x => x);
            var sut = new Basic.Multiple(new Delegate[] { f1, f2, f3 }, 1, 2, "a", "b").Select(x => x.Function);
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }
    }
}
