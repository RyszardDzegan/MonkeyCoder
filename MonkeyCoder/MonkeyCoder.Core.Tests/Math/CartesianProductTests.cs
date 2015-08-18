using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonkeyCoder.Core.Math;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using static System.Console;

namespace MonkeyCoder.Core.Tests.Math
{
    using static CartesianProductTests.CartesianProductExpectedOutputReader;

    [TestClass]
    public class CartesianProductTests
    {
        internal static class CartesianProductExpectedOutputReader
        {
            private static ExpectedOutputReader ExpectedOutputReader { get; } = new ExpectedOutputReader("CartesianProductTestsExpectedOutputs.txt");
            public static string GetExpectedTestOutput([CallerMemberName] string testMethodName = "") => ExpectedOutputReader.GetExpectedTestOutput(testMethodName);
        }

        private StringWriter ActualTestOutput { get; } = new StringWriter();
        private string GetActualTestOutput() => ActualTestOutput.ToString();

        private void GenerateOutput<T>(CartesianProduct<T> items)
        {
            foreach (var item in items)
            {
                var result = string.Join("", item);
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
            new CartesianProduct<string>(Enumerable.Repeat<IEnumerable<string>>(null, 1));
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
            var sut = new CartesianProduct<string>(Enumerable.Empty<IEnumerable<string>>());
        }

        [TestMethod]
        public void Works_with_one_0_items()
        {
            var sut = new CartesianProduct<string>(Enumerable.Repeat(Enumerable.Empty<string>(), 1));
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
            var sut = new CartesianProduct<string>(Enumerable.Repeat(Enumerable.Empty<string>(), 2));
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_two_0_1_items()
        {
            var sut = new CartesianProduct<string>(new[] { Enumerable.Empty<string>(), new[] { "A" } });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_two_0_2_items()
        {
            var sut = new CartesianProduct<string>(new[] { Enumerable.Empty<string>(), new[] { "A", "B" } });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_two_1_0_items()
        {
            var sut = new CartesianProduct<string>(new[] { new[] { "a" }, Enumerable.Empty<string>() });
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
            var sut = new CartesianProduct<string>(new[] { new[] { "a", "b" }, Enumerable.Empty<string>() });
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
            var sut = new CartesianProduct<string>(Enumerable.Repeat(Enumerable.Empty<string>(), 3));
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_three_0_0_1_items()
        {
            var sut = new CartesianProduct<string>(new[] { Enumerable.Empty<string>(), Enumerable.Empty<string>(), new[] { "1" } });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_three_0_0_2_items()
        {
            var sut = new CartesianProduct<string>(new[] { Enumerable.Empty<string>(), Enumerable.Empty<string>(), new[] { "1", "2" } });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_three_0_1_0_items()
        {
            var sut = new CartesianProduct<string>(new[] { Enumerable.Empty<string>(), new[] { "A" }, Enumerable.Empty<string>() });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_three_0_1_1_items()
        {
            var sut = new CartesianProduct<string>(new[] { Enumerable.Empty<string>(), new[] { "A" }, new[] { "1" } });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_three_0_1_2_items()
        {
            var sut = new CartesianProduct<string>(new[] { Enumerable.Empty<string>(), new[] { "A" }, new[] { "1", "2" } });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_three_0_2_0_items()
        {
            var sut = new CartesianProduct<string>(new[] { Enumerable.Empty<string>(), new[] { "A", "B" }, Enumerable.Empty<string>() });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_three_0_2_1_items()
        {
            var sut = new CartesianProduct<string>(new[] { Enumerable.Empty<string>(), new[] { "A", "B" }, new[] { "1" } });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_three_0_2_2_items()
        {
            var sut = new CartesianProduct<string>(new[] { Enumerable.Empty<string>(), new[] { "A", "B" }, new[] { "1", "2" } });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_three_1_0_0_items()
        {
            var sut = new CartesianProduct<string>(new[] { new[] { "a" }, Enumerable.Empty<string>(), Enumerable.Empty<string>() });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_three_1_0_1_items()
        {
            var sut = new CartesianProduct<string>(new[] { new[] { "a" }, Enumerable.Empty<string>(), new[] { "1" } });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_three_1_0_2_items()
        {
            var sut = new CartesianProduct<string>(new[] { new[] { "a" }, Enumerable.Empty<string>(), new[] { "1", "2" } });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_three_1_1_0_items()
        {
            var sut = new CartesianProduct<string>(new[] { new[] { "a" }, new[] { "A" }, Enumerable.Empty<string>() });
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
            var sut = new CartesianProduct<string>(new[] { new[] { "a" }, new[] { "A", "B" }, Enumerable.Empty<string>() });
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
            var sut = new CartesianProduct<string>(new[] { new[] { "a", "b" }, Enumerable.Empty<string>(), Enumerable.Empty<string>() });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_three_2_0_1_items()
        {
            var sut = new CartesianProduct<string>(new[] { new[] { "a", "b" }, Enumerable.Empty<string>(), new[] { "1" } });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_three_2_0_2_items()
        {
            var sut = new CartesianProduct<string>(new[] { new[] { "a", "b" }, Enumerable.Empty<string>(), new[] { "1", "2" } });
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }

        [TestMethod]
        public void Works_with_three_2_1_0_items()
        {
            var sut = new CartesianProduct<string>(new[] { new[] { "a", "b" }, new[] { "A" }, Enumerable.Empty<string>() });
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
            var sut = new CartesianProduct<string>(new[] { new[] { "a", "b" }, new[] { "A", "B" }, Enumerable.Empty<string>() });
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
