using NUnit.Framework;
using System;
using static NUnit.Framework.Assert;

namespace MonkeyCoder.Math.Tests
{
    using static TestHelpers.StaticExpectedOutputReader;

    [TestFixture]
    public class PowerSetTests : MathTestsBase
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Throws_exception_when_pass_null_to_constructor()
        {
            new PowerSet<string>(null);
        }

        [Test]
        public void Works_with_0_items()
        {
            var sut = new PowerSet<string>(new string[0]);
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }
        
        [Test]
        public void Works_with_1_item()
        {
            var sut = new PowerSet<string>(new[] { "a" });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [Test]
        public void Works_with_2_items()
        {
            var sut = new PowerSet<string>(new[] { "a", "b" });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [Test]
        public void Works_with_3_items()
        {
            var sut = new PowerSet<string>(new[] { "a", "b", "c" });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [Test]
        public void Works_with_4_items()
        {
            var sut = new PowerSet<string>(new[] { "a", "b", "c", "d" });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [Test]
        public void Works_with_5_items()
        {
            var sut = new PowerSet<string>(new[] { "a", "b", "c", "d", "e" });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }
    }
}
