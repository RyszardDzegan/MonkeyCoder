using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace MonkeyCoder.Core.Tests
{
    [TestClass]
    public class SingleVariableManagerTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Throws_exception_when_possible_values_are_null()
        {
            new SingleVariableManager("x", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Throws_exception_when_variable_name_is_null()
        {
            new SingleVariableManager(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Throws_exception_when_variable_name_is_empty_string()
        {
            new SingleVariableManager("", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Throws_exception_when_variable_name_consists_of_white_spaces()
        {
            new SingleVariableManager(" ", null);
        }

        [TestMethod]
        public void Trims_variable_name()
        {
            var vm = new SingleVariableManager("x");

            var loopCount = 0;
            foreach (var vb in vm)
            {
                IsNull(vb.x);
                loopCount++;
            }

            AreEqual(1, loopCount);
        }

        [TestMethod]
        public void Enters_into_loop_one_time_when_there_are_no_possible_values()
        {
            var vm = new SingleVariableManager("x");

            var loopCount = 0;
            foreach (var vb in vm)
                loopCount++;

            AreEqual(1, loopCount);
        }

        [TestMethod]
        public void Enters_into_loop_one_time_when_possible_values_count_equals_one()
        {
            var vm = new SingleVariableManager("x", 1);

            var loopCount = 0;
            foreach (var vb in vm)
                loopCount++;

            AreEqual(1, loopCount);
        }

        [TestMethod]
        public void Enters_into_loop_two_times_when_possible_values_count_equals_two()
        {
            var vm = new SingleVariableManager("x", 1, 2);

            var loopCount = 0;
            foreach (var vb in vm)
                loopCount++;

            AreEqual(2, loopCount);
        }

        [TestMethod]
        public void Variable_is_uninitialized_when_there_are_no_possible_values()
        {
            var vm = new SingleVariableManager("x");

            var loopCount = 0;
            foreach (var vb in vm)
            {
                IsNull(vb.x);
                loopCount++;
            }

            AreEqual(1, loopCount);
        }

        [TestMethod]
        public void Returns_correct_values()
        {
            var vm = new SingleVariableManager("x", 1, "a", 2.3);
            var e = vm.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual(1, e.Current.x);
            IsTrue(e.MoveNext());
            AreEqual("a", e.Current.x);
            IsTrue(e.MoveNext());
            AreEqual(2.3, e.Current.x);
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Throws_an_exception_when_access_variable_that_not_exists()
        {
            var vm = new SingleVariableManager("x", 1);

            foreach (var vb in vm)
                AreEqual(vb.y, 1);
        }
    }
}
