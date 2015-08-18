using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using static Microsoft.VisualStudio.TestTools.UnitTesting.StringAssert;

namespace MonkeyCoder.Math.Tests
{
    using static MathTestsBase.StaticExpectedOutputReader;

    [TestClass]
    public class VariationsWithRepetitionsTests : MathTestsBase
    {
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
