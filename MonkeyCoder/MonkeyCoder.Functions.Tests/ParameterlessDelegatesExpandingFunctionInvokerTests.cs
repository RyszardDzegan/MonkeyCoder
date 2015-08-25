using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace MonkeyCoder.Functions.Tests
{
    using static TestHelpers.StaticExpectedOutputReader;

    [TestClass]
    public class ParameterlessDelegatesExpandingFunctionInvokerTests : BasicFunctionInvokerTests
    {
        internal override IEnumerable<Func<object>> GetInvoker(Delegate function, params object[] possibleArguments) => new ParameterlessDelegatesExpandingFunctionInvoker(function, possibleArguments);

        [TestMethod]
        public void Works_with_function_2_string_and_1_string_function_as_possible_argument()
        {
            var function = new Func<string, string>(x => x);
            var p1 = new Func<string>(() => "a");
            var sut = GetInvoker(function, p1);
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_function_2_string_and_2_string_functions_as_possible_arguments()
        {
            var function = new Func<string, string>(x => x);
            var p1 = new Func<string>(() => "a");
            var p2 = new Func<string>(() => "b");
            var sut = GetInvoker(function, p1, p2);
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_function_2_string_and_2_string_1_int_functions_as_possible_arguments()
        {
            var function = new Func<string, string>(x => x);
            var p1 = new Func<string>(() => "a");
            var p2 = new Func<string>(() => "b");
            var p3 = new Func<int>(() => 1);
            var sut = GetInvoker(function, p1, p2, p3);
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_function_2_string_and_2_string_1_int_functions_and_1_string_as_possible_arguments()
        {
            var function = new Func<string, string>(x => x);
            var p1 = new Func<string>(() => "b");
            var p2 = new Func<string>(() => "c");
            var p3 = new Func<int>(() => 1);
            var sut = GetInvoker(function, p1, p2, p3, "a");
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_function_2_string_and_2_string_1_int_functions_and_1_int_as_possible_arguments()
        {
            var function = new Func<string, string>(x => x);
            var p1 = new Func<string>(() => "a");
            var p2 = new Func<string>(() => "b");
            var p3 = new Func<int>(() => 1);
            var sut = GetInvoker(function, p1, p2, p3, 2);
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_function_2_string_and_2_string_1_int_functions_and_1_null_as_possible_arguments()
        {
            var function = new Func<string, string>(x => x);
            var p1 = new Func<string>(() => "a");
            var p2 = new Func<string>(() => "b");
            var p3 = new Func<int>(() => 1);
            var sut = GetInvoker(function, p1, p2, p3, null);
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_function_3_string_and_2_string_1_int_functions_and_1_string_1_int_as_possible_arguments()
        {
            var function = new Func<string, string, string>((x, y) => x + y);
            var p1 = new Func<string>(() => "b");
            var p2 = new Func<string>(() => "c");
            var p3 = new Func<int>(() => 1);
            var sut = GetInvoker(function, p1, p2, p3, "a", 2);
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_function_1_string_1_func_string_1_string_and_2_string_1_int_functions_and_1_string_1_int_as_possible_arguments()
        {
            var function = new Func<string, Func<string>, string>((x, y) => x + y());
            var p1 = new Func<string>(() => "b");
            var p2 = new Func<string>(() => "c");
            var p3 = new Func<int>(() => 1);
            var sut = GetInvoker(function, p1, p2, p3, "a", 2);
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public override void Works_with_complex_possible_arguments()
        {
            var function = new Func<string, Func<string>, int, int?, Func<int>, Func<int?>, string>(
                (a, b, c, d, e, f) => (a != null ? a : "A") + (b != null ? b() : "B") + c + (d != null ? d.Value.ToString() : "D") + (e != null ? e().ToString() : "E") + (f != null ? f().Value.ToString() : "F"));
            var sut = GetInvoker(function, "a", new Func<string>(() => "b"), 3, 4, new Func<int>(() => 5), new Func<int?>(() => 6), null);
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }
    }
}
