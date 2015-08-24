using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using static Microsoft.VisualStudio.TestTools.UnitTesting.StringAssert;

namespace MonkeyCoder.Functions.Tests
{
    using static TestHelpers.StaticExpectedOutputReader;

    [TestClass]
    public class MultipleFunctionInvokerTests : CommonSingleFunctionInvokerTests
    {
        internal override ISingleFunctionInvoker GetInvoker(Delegate function, params object[] possibleArguments) => new MultipleFunctionInvoker(new Delegate[] { function }, possibleArguments);

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
            var sut = new MultipleFunctionInvoker(new Delegate[] { f1, f2 });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_function_2_int_and_function_2_string_and_1_int_possible_argument()
        {
            var f1 = new Func<int, int>(x => x);
            var f2 = new Func<string, string>(x => x);
            var sut = new MultipleFunctionInvoker(new Delegate[] { f1, f2 }, 1);
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_function_2_int_and_function_2_string_and_1_int_1_string_possible_arguments()
        {
            var f1 = new Func<int, int>(x => x);
            var f2 = new Func<string, string>(x => x);
            var sut = new MultipleFunctionInvoker(new Delegate[] { f1, f2 }, 1, "a");
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_function_2_int_and_function_2_string_and_1_string_1_int_possible_arguments()
        {
            var f1 = new Func<int, int>(x => x);
            var f2 = new Func<string, string>(x => x);
            var sut = new MultipleFunctionInvoker(new Delegate[] { f1, f2 }, "a", 1);
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_function_2_string_and_function_2_int_and_1_int_1_string_possible_arguments()
        {
            var f1 = new Func<string, string>(x => x);
            var f2 = new Func<int, int>(x => x);
            var sut = new MultipleFunctionInvoker(new Delegate[] { f1, f2 }, 1, "a");
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_function_2_string_and_function_2_int_and_1_string_1_int_possible_arguments()
        {
            var f1 = new Func<string, string>(x => x);
            var f2 = new Func<int, int>(x => x);
            var sut = new MultipleFunctionInvoker(new Delegate[] { f1, f2 }, "a", 1);
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_function_2_int_and_function_2_string_and_2_int_2_string_possible_arguments()
        {
            var f1 = new Func<int, int>(x => x);
            var f2 = new Func<string, string>(x => x);
            var sut = new MultipleFunctionInvoker(new Delegate[] { f1, f2 }, 1, 2, "a", "b");
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_function_string_int_string_and_function_2_string_and_2_int_2_string_possible_arguments()
        {
            var f1 = new Func<string, int, string>((x, y) => x + y);
            var f2 = new Func<string, string>(x => x);
            var sut = new MultipleFunctionInvoker(new Delegate[] { f1, f2 }, 1, 2, "a", "b");
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_function_string_int_string_and_function_2_string_and_function_2_int_and_2_int_2_string_possible_arguments()
        {
            var f1 = new Func<string, int, string>((x, y) => x + y);
            var f2 = new Func<string, string>(x => x);
            var f3 = new Func<int, int>(x => x);
            var sut = new MultipleFunctionInvoker(new Delegate[] { f1, f2, f3 }, 1, 2, "a", "b");
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }
    }
}
