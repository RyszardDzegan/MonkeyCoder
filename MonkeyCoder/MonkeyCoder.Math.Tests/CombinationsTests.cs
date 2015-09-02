using NUnit.Framework;
using System;
using System.Linq;
using static NUnit.Framework.Assert;
using static NUnit.Framework.StringAssert;

namespace MonkeyCoder.Math.Tests
{
    using static TestHelpers.StaticExpectedOutputReader;

    [TestFixture]
    public class CombinationsTests : MathTestsBase
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Throws_exception_when_pass_null_to_constructor()
        {
            new Combinations<string>(null, 0);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Throws_exception_when_k_is_less_than_zero()
        {
            try
            {
                new Combinations<string>(Enumerable.Empty<string>(), -1);
            }
            catch (Exception exception)
            {
                StartsWith("k must be greater or equal 0, but is -1.", exception.Message);
                throw;
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Throws_exception_when_k_is_greater_than_items_size()
        {
            try
            {
                new Combinations<string>(new[] { "a" }, 2);
            }
            catch (Exception exception)
            {
                StartsWith("k must be less or equal items count, but is 2.", exception.Message);
                throw;
            }
        }

        [Test]
        public void Works_with_no_items_and_k_0()
        {
            var variations = new Combinations<string>(Enumerable.Empty<string>(), 0);
            GenerateOutput(variations);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [Test]
        public void Works_with_1_item_and_k_0()
        {
            var variations = new Combinations<string>(new[] { "a" }, 0);
            GenerateOutput(variations);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [Test]
        public void Works_with_1_item_and_k_1()
        {
            var variations = new Combinations<string>(new[] { "a" }, 1);
            GenerateOutput(variations);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [Test]
        public void Works_with_2_items_and_k_0()
        {
            var variations = new Combinations<string>(new[] { "a", "b" }, 0);
            GenerateOutput(variations);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [Test]
        public void Works_with_2_items_and_k_1()
        {
            var variations = new Combinations<string>(new[] { "a", "b" }, 1);
            GenerateOutput(variations);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [Test]
        public void Works_with_2_items_and_k_2()
        {
            var variations = new Combinations<string>(new[] { "a", "b" }, 2);
            GenerateOutput(variations);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [Test]
        public void Works_with_3_items_and_k_0()
        {
            var variations = new Combinations<string>(new[] { "a", "b", "c" }, 0);
            GenerateOutput(variations);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [Test]
        public void Works_with_3_items_and_k_1()
        {
            var variations = new Combinations<string>(new[] { "a", "b", "c" }, 1);
            GenerateOutput(variations);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [Test]
        public void Works_with_3_items_and_k_2()
        {
            var variations = new Combinations<string>(new[] { "a", "b", "c" }, 2);
            GenerateOutput(variations);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [Test]
        public void Works_with_3_items_and_k_3()
        {
            var variations = new Combinations<string>(new[] { "a", "b", "c" }, 3);
            GenerateOutput(variations);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [Test]
        public void Works_with_4_items_and_k_0()
        {
            var variations = new Combinations<string>(new[] { "a", "b", "c", "d" }, 0);
            GenerateOutput(variations);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [Test]
        public void Works_with_4_items_and_k_1()
        {
            var variations = new Combinations<string>(new[] { "a", "b", "c", "d" }, 1);
            GenerateOutput(variations);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [Test]
        public void Works_with_4_items_and_k_2()
        {
            var variations = new Combinations<string>(new[] { "a", "b", "c", "d" }, 2);
            GenerateOutput(variations);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [Test]
        public void Works_with_4_items_and_k_3()
        {
            var variations = new Combinations<string>(new[] { "a", "b", "c", "d" }, 3);
            GenerateOutput(variations);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [Test]
        public void Works_with_4_items_and_k_4()
        {
            var variations = new Combinations<string>(new[] { "a", "b", "c", "d" }, 4);
            GenerateOutput(variations);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }
    }
}
