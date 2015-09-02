using NUnit.Framework;
using System;
using TestHelpers;

namespace MonkeyCoder.Functions.Tests
{
    [TestFixture]
    public class ExpandingStack0Tests : CommonExpandingTests
    {
        internal override int StackSize => 0;

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
        public override void Works_with_function_1_string()
        {
            base.Works_with_function_1_string();
        }

        [Test]
        public override void Works_with_function_1_string_and_1_func_string_possible_argument()
        {
            base.Works_with_function_1_string_and_1_func_string_possible_argument();
        }

        [Test]
        [ExpectedOutput]
        public override void Works_with_function_2_string_and_1_func_string_possible_argument()
        {
            base.Works_with_function_2_string_and_1_func_string_possible_argument();
        }

        [Test]
        [ExpectedOutput]
        public override void Works_with_function_2_string_and_1_func_2_string_2_string_possible_arguments()
        {
            base.Works_with_function_2_string_and_1_func_2_string_2_string_possible_arguments();
        }

        [Test]
        [ExpectedOutput]
        public override void Works_with_function_2_string_and_func_2_string_and_string_and_string_possible_arguments()
        {
            base.Works_with_function_2_string_and_func_2_string_and_string_and_string_possible_arguments();
        }

        [Test]
        [ExpectedOutput]
        public override void Works_with_function_2_string_and_func_2_string_and_func_2_string_and_string_possible_arguments()
        {
            base.Works_with_function_2_string_and_func_2_string_and_func_2_string_and_string_possible_arguments();
        }
    }
}
