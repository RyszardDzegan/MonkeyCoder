using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace MonkeyCoder.Core.Tests
{
    [TestClass]
    public class TypeSafeSingleVariableManagerTests
    {
        class Foo
        {
            public string X { get; set; }
        }

        class Bar
        {
            public dynamic X { get; set; }
        }

        class MoreThanOnePropertyType
        {
            public string X { get; set; }
            public string Y { get; set; }
        }

        class LessThanOnePropertyType
        {
            public string X { get; set; }
            public string Y { get; set; }
        }

        class ReadonlyPropertyType
        {
            public string X { get; }
        }
        
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Throws_exception_when_type_has_more_than_one_property()
        {
            new TypeSafeSingleVariableManager<MoreThanOnePropertyType>();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Throws_exception_when_type_has_less_than_one_property()
        {
            new TypeSafeSingleVariableManager<LessThanOnePropertyType>();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Throws_exception_when_property_is_readonly()
        {
            new TypeSafeSingleVariableManager<ReadonlyPropertyType>();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Throws_exception_when_possible_values_are_null()
        {
            new TypeSafeSingleVariableManager<Foo>(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Throws_exception_when_possible_values_cannot_be_assigned_to_property()
        {
            new TypeSafeSingleVariableManager<Foo>("a", 1, 2.345, "b");
        }

        [TestMethod]
        public void Enters_into_loop_one_time_when_there_are_no_possible_values()
        {
            var vm = new TypeSafeSingleVariableManager<Foo>();

            var loopCount = 0;
            foreach (var vb in vm)
                loopCount++;

            AreEqual(1, loopCount);
        }

        [TestMethod]
        public void Enters_into_loop_one_time_when_possible_values_count_equals_one()
        {
            var vm = new TypeSafeSingleVariableManager<Foo>("a");

            var loopCount = 0;
            foreach (var vb in vm)
                loopCount++;

            AreEqual(1, loopCount);
        }

        [TestMethod]
        public void Enters_into_loop_two_times_when_possible_values_count_equals_two()
        {
            var vm = new TypeSafeSingleVariableManager<Foo>("a", "b");

            var loopCount = 0;
            foreach (var vb in vm)
                loopCount++;

            AreEqual(2, loopCount);
        }

        [TestMethod]
        public void Variable_is_uninitialized_when_there_are_no_possible_values()
        {
            var vm = new TypeSafeSingleVariableManager<Foo>();

            var loopCount = 0;
            foreach (var vb in vm)
            {
                IsNull(vb.X);
                loopCount++;
            }

            AreEqual(1, loopCount);
        }

        [TestMethod]
        public void Returns_correct_values()
        {
            var vm = new TypeSafeSingleVariableManager<Foo>("a", "b", "c");
            var e = vm.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("a", e.Current.X);
            IsTrue(e.MoveNext());
            AreEqual("b", e.Current.X);
            IsTrue(e.MoveNext());
            AreEqual("c", e.Current.X);
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Returns_correct_values_for_dynamic_values()
        {
            var vm = new TypeSafeSingleVariableManager<Bar>("a", 1, 2.3);
            var e = vm.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("a", e.Current.X);
            IsTrue(e.MoveNext());
            AreEqual(1, e.Current.X);
            IsTrue(e.MoveNext());
            AreEqual(2.3, e.Current.X);
            IsFalse(e.MoveNext());
        }
    }
}
