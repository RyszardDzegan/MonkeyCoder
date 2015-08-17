using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonkeyCoder.Core.Math;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using static Microsoft.VisualStudio.TestTools.UnitTesting.StringAssert;
using static System.Console;

namespace MonkeyCoder.Core.Tests.Math
{
    using System;
    using static VariationsWithRepetitionsTests.VariationsExpectedOutputReader;

    [TestClass]
    public class VariationsWithRepetitionsTests
    {
        internal static class VariationsExpectedOutputReader
        {
            private static ExpectedOutputReader ExpectedOutputReader { get; } = new ExpectedOutputReader("VariationsWithRepetitionsTestsExpectedOutputs.txt");
            public static string GetExpectedTestOutput([CallerMemberName] string testMethodName = "") => ExpectedOutputReader.GetExpectedTestOutput(testMethodName);
        }

        private StringWriter ActualTestOutput { get; } = new StringWriter();
        private string GetActualTestOutput() => ActualTestOutput.ToString();

        private void GenerateOutput<T>(VariationsWithRepetitions<T> variations)
        {
            foreach (var variation in variations)
            {
                var result = string.Join("", variation);
                WriteLine(result);
            }
        }

        [TestInitialize]
        public void Setup()
        {
            var doubleOutput = new DoubleOutput(Out, ActualTestOutput);
            SetOut(doubleOutput);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Throws_exception_when_pass_null_to_constructor()
        {
            new VariationsWithRepetitions<string>(null, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Throws_exception_when_k_is_less_than_zero()
        {
            try
            {
                new VariationsWithRepetitions<string>(Enumerable.Empty<string>(), -1);
            }
            catch (Exception exception)
            {
                StartsWith(exception.Message, "k must be greater or equal 0, but is -1.");
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Throws_exception_when_k_is_greater_than_items_size()
        {
            try
            {
                new VariationsWithRepetitions<string>(new[] { "a" }, 2);
            }
            catch (Exception exception)
            {
                StartsWith(exception.Message, "k must be less or equal items count, but is 2.");
                throw;
            }
        }

        [TestMethod]
        public void Works_with_no_items_and_k_0()
        {
            var variations = new VariationsWithRepetitions<string>(Enumerable.Empty<string>(), 0);
            GenerateOutput(variations);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_1_item_and_k_0()
        {
            var variations = new VariationsWithRepetitions<string>(new[] { "a" }, 0);
            GenerateOutput(variations);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_1_item_and_k_1()
        {
            var variations = new VariationsWithRepetitions<string>(new[] { "a" }, 1);
            GenerateOutput(variations);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_2_items_and_k_0()
        {
            var variations = new VariationsWithRepetitions<string>(new[] { "a", "b" }, 0);
            GenerateOutput(variations);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_2_items_and_k_1()
        {
            var variations = new VariationsWithRepetitions<string>(new[] { "a", "b" }, 1);
            GenerateOutput(variations);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_2_items_and_k_2()
        {
            var variations = new VariationsWithRepetitions<string>(new[] { "a", "b" }, 2);
            GenerateOutput(variations);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_3_items_and_k_0()
        {
            var variations = new VariationsWithRepetitions<string>(new[] { "a", "b", "c" }, 0);
            GenerateOutput(variations);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_3_items_and_k_1()
        {
            var variations = new VariationsWithRepetitions<string>(new[] { "a", "b", "c" }, 1);
            GenerateOutput(variations);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_3_items_and_k_2()
        {
            var variations = new VariationsWithRepetitions<string>(new[] { "a", "b", "c" }, 2);
            GenerateOutput(variations);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_3_items_and_k_3()
        {
            var variations = new VariationsWithRepetitions<string>(new[] { "a", "b", "c" }, 3);
            GenerateOutput(variations);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }
    }
}
