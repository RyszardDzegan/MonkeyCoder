using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace MonkeyCoder.Math.Tests
{
    using static MathTestsBase.StaticExpectedOutputReader;

    [TestClass]
    public class PowerSetTests : MathTestsBase
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Throws_exception_when_pass_null_to_constructor()
        {
            new PowerSet<string>(null);
        }

        [TestMethod]
        public void Works_with_0_items()
        {
            var sut = new PowerSet<string>(new string[0]);
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }
        
        [TestMethod]
        public void Works_with_1_item()
        {
            var sut = new PowerSet<string>(new[] { "a" });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_2_items()
        {
            var sut = new PowerSet<string>(new[] { "a", "b" });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_3_items()
        {
            var sut = new PowerSet<string>(new[] { "a", "b", "c" });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_4_items()
        {
            var sut = new PowerSet<string>(new[] { "a", "b", "c", "d" });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_5_items()
        {
            var sut = new PowerSet<string>(new[] { "a", "b", "c", "d", "e" });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }
    }
}
