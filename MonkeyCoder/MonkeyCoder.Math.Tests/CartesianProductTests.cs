using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace MonkeyCoder.Math.Tests
{
    using static TestHelpers.StaticExpectedOutputReader;

    [TestClass]
    public class CartesianProductTests : MathTestsBase
    {
        [TestMethod]
        public void Enumerator_of_empty_sequence_has_no_move()
        {
            var emptySequence = Enumerable.Empty<string>();
            var enumerator = emptySequence.GetEnumerator();
            IsFalse(enumerator.MoveNext());
        }

        [TestMethod]
        public void Foreach_will_not_execute_in_case_of_empty_sequence()
        {
            var emptySequence = Enumerable.Empty<string>();
            foreach (var item in emptySequence)
                Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Throws_exception_when_pass_null_to_constructor()
        {
            new CartesianProduct<string>(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Throws_exception_when_one_of_items_is_null()
        {
            new CartesianProduct<string>(Enumerable.Repeat<IReadOnlyCollection<string>>(null, 1));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Throws_exception_when_any_of_items_is_null()
        {
            new CartesianProduct<string>(new[] { new[] { "a" }, null, null });
        }

        [TestMethod]
        public void Works_with_no_items()
        {
            var sut = new CartesianProduct<string>(Enumerable.Empty<IReadOnlyCollection<string>>());
        }

        [TestMethod]
        public void Works_with_one_0_items()
        {
            var sut = new CartesianProduct<string>(Enumerable.Repeat(new string[0], 1));
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_one_1_items()
        {
            var sut = new CartesianProduct<string>(new[] { new[] { "a" } });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_one_2_items()
        {
            var sut = new CartesianProduct<string>(new[] { new[] { "a", "b" } });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_two_0_0_items()
        {
            var sut = new CartesianProduct<string>(Enumerable.Repeat(new string[0], 2));
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_two_0_1_items()
        {
            var sut = new CartesianProduct<string>(new[] { new string[0], new[] { "A" } });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_two_0_2_items()
        {
            var sut = new CartesianProduct<string>(new[] { new string[0], new[] { "A", "B" } });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_two_1_0_items()
        {
            var sut = new CartesianProduct<string>(new[] { new[] { "a" }, new string[0] });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_two_1_1_items()
        {
            var sut = new CartesianProduct<string>(new[] { new[] { "a" }, new[] { "A" } });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }
        
        [TestMethod]
        public void Works_with_two_1_2_items()
        {
            var sut = new CartesianProduct<string>(new[] { new[] { "a" }, new[] { "A", "B" } });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_two_2_0_items()
        {
            var sut = new CartesianProduct<string>(new[] { new[] { "a", "b" }, new string[0] });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_two_2_1_items()
        {
            var sut = new CartesianProduct<string>(new[] { new[] { "a", "b" }, new[] { "A" } });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_two_2_2_items()
        {
            var sut = new CartesianProduct<string>(new[] { new[] { "a", "b" }, new[] { "A", "B" } });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_three_0_0_0_items()
        {
            var sut = new CartesianProduct<string>(Enumerable.Repeat(new string[0], 3));
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_three_0_0_1_items()
        {
            var sut = new CartesianProduct<string>(new[] { new string[0], new string[0], new[] { "1" } });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_three_0_0_2_items()
        {
            var sut = new CartesianProduct<string>(new[] { new string[0], new string[0], new[] { "1", "2" } });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_three_0_1_0_items()
        {
            var sut = new CartesianProduct<string>(new[] { new string[0], new[] { "A" }, new string[0] });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_three_0_1_1_items()
        {
            var sut = new CartesianProduct<string>(new[] { new string[0], new[] { "A" }, new[] { "1" } });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_three_0_1_2_items()
        {
            var sut = new CartesianProduct<string>(new[] { new string[0], new[] { "A" }, new[] { "1", "2" } });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_three_0_2_0_items()
        {
            var sut = new CartesianProduct<string>(new[] { new string[0], new[] { "A", "B" }, new string[0] });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_three_0_2_1_items()
        {
            var sut = new CartesianProduct<string>(new[] { new string[0], new[] { "A", "B" }, new[] { "1" } });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_three_0_2_2_items()
        {
            var sut = new CartesianProduct<string>(new[] { new string[0], new[] { "A", "B" }, new[] { "1", "2" } });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_three_1_0_0_items()
        {
            var sut = new CartesianProduct<string>(new[] { new[] { "a" }, new string[0], new string[0] });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_three_1_0_1_items()
        {
            var sut = new CartesianProduct<string>(new[] { new[] { "a" }, new string[0], new[] { "1" } });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_three_1_0_2_items()
        {
            var sut = new CartesianProduct<string>(new[] { new[] { "a" }, new string[0], new[] { "1", "2" } });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_three_1_1_0_items()
        {
            var sut = new CartesianProduct<string>(new[] { new[] { "a" }, new[] { "A" }, new string[0] });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_three_1_1_1_items()
        {
            var sut = new CartesianProduct<string>(new[] { new[] { "a" }, new[] { "A" }, new[] { "1" } });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_three_1_1_2_items()
        {
            var sut = new CartesianProduct<string>(new[] { new[] { "a" }, new[] { "A" }, new[] { "1", "2" } });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_three_1_2_0_items()
        {
            var sut = new CartesianProduct<string>(new[] { new[] { "a" }, new[] { "A", "B" }, new string[0] });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_three_1_2_1_items()
        {
            var sut = new CartesianProduct<string>(new[] { new[] { "a" }, new[] { "A", "B" }, new[] { "1" } });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_three_1_2_2_items()
        {
            var sut = new CartesianProduct<string>(new[] { new[] { "a" }, new[] { "A", "B" }, new[] { "1", "2" } });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_three_2_0_0_items()
        {
            var sut = new CartesianProduct<string>(new[] { new[] { "a", "b" }, new string[0], new string[0] });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_three_2_0_1_items()
        {
            var sut = new CartesianProduct<string>(new[] { new[] { "a", "b" }, new string[0], new[] { "1" } });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_three_2_0_2_items()
        {
            var sut = new CartesianProduct<string>(new[] { new[] { "a", "b" }, new string[0], new[] { "1", "2" } });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_three_2_1_0_items()
        {
            var sut = new CartesianProduct<string>(new[] { new[] { "a", "b" }, new[] { "A" }, new string[0] });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_three_2_1_1_items()
        {
            var sut = new CartesianProduct<string>(new[] { new[] { "a", "b" }, new[] { "A" }, new[] { "1" } });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_three_2_1_2_items()
        {
            var sut = new CartesianProduct<string>(new[] { new[] { "a", "b" }, new[] { "A" }, new[] { "1", "2" } });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_three_2_2_0_items()
        {
            var sut = new CartesianProduct<string>(new[] { new[] { "a", "b" }, new[] { "A", "B" }, new string[0] });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_three_2_2_1_items()
        {
            var sut = new CartesianProduct<string>(new[] { new[] { "a", "b" }, new[] { "A", "B" }, new[] { "1" } });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_three_2_2_2_items()
        {
            var sut = new CartesianProduct<string>(new[] { new[] { "a", "b" }, new[] { "A", "B" }, new[] { "1", "2" } });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_when_iterated_two_times()
        {
            var sut = new CartesianProduct<string>(new[] { new[] { "a", "b" }, new[] { "A", "B" } });
            GenerateOutput(sut);
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_when_updated_sequention_prior_to_next_iteration()
        {
            var seq1 = new[] { "a", "b" };
            var seq2 = new[] { "A", "B" };
            var sut = new CartesianProduct<string>(new[] { seq1, seq2 });
            GenerateOutput(sut);
            seq1[0] = "1";
            seq1[1] = "2";
            seq2[0] = "+";
            seq2[1] = "-";
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }
    }
}
