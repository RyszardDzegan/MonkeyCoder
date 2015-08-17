using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using static Microsoft.VisualStudio.TestTools.UnitTesting.StringAssert;

namespace MonkeyCoder.Core.Tests
{
    [TestClass]
    public class TypeSafeMultipleVariableManagerTests
    {
        class Foo0 { }

        class Foo1
        {
            public string X { get; set; }
        }

        class Foo2
        {
            public string X { get; set; }
            public int Y { get; set; }
        }

        class Foo3
        {
            public string X { get; set; }
            public int Y { get; set; }
            public long Z { get; set; }
        }

        class FooValues
        {
            public string[] X { get; } = new[] { "a", "b", "c" };
            public int[] Y { get; } = new[] { 1, 2 };
        }

        class NoSetters
        {
            public string X { get; set; }
            public int Y { get; }
            public double Z { get; }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Throws_exception_when_possible_values_are_null()
        {
            try
            {
                new TypeSafeMultipleVariableManager<Foo1>(null);
            }
            catch (Exception exception)
            {
                StartsWith(exception.Message, "Possible values cannot be null.");
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Throws_exception_when_variables_dont_have_setters()
        {
            try
            {
                new TypeSafeMultipleVariableManager<NoSetters>(null);
            }
            catch (Exception exception)
            {
                StartsWith(exception.Message, "Variable properties must have setters. The following properties don't have setters: Y, Z.");
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Throws_exception_when_missing_possible_values_placeholders_for_variables()
        {
            try
            {
                new TypeSafeMultipleVariableManager<Foo3>(new { Y = new[] { 1, 2, 3 } });
            }
            catch (Exception exception)
            {
                StartsWith(exception.Message, "All variable properties must have same named possible values placeholder properties. The following possible values placeholders are missing: X, Z.");
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Throws_exception_when_single_possible_value_has_incompatible_type()
        {
            try
            {
                new TypeSafeMultipleVariableManager<Foo3>(new { X = "a", Y = "B", Z = 1.0 });
            }
            catch (Exception exception)
            {
                StartsWith(exception.Message, "Variable must be assignable from a single value or collection of values. The following possible values don't have valid typ: Y, Z.");
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Throws_exception_when_multiple_possible_values_have_incompatible_types()
        {
            try
            {
                new TypeSafeMultipleVariableManager<Foo3>(new { X = new[] { "a" }, Y = new[] { "B" }, Z = new[] { 1.0 } });
            }
            catch (Exception exception)
            {
                StartsWith(exception.Message, "Variable must be assignable from a single value or collection of values. The following possible values don't have valid typ: Y, Z.");
                throw;
            }
        }

        [TestMethod]
        public void Works_when_possible_values_are_null_but_variables_are_empty()
        {
            var vm = new TypeSafeMultipleVariableManager<Foo0>(null);
            var e = vm.GetEnumerator();
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_when_there_are_more_properties_than_required_for_holding_possible_values()
        {
            var vm = new TypeSafeMultipleVariableManager<Foo1>(new { X = new[] { "a" }, Y = new[] { 1 }, Z = new[] { 1L } });
            var e = vm.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("a", e.Current.X);
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_for_single_possible_value()
        {
            var vm = new TypeSafeMultipleVariableManager<Foo1>(new { X = "a" });
            var e = vm.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("a", e.Current.X);
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_for_single_possible_value_which_is_null()
        {
            var vm = new TypeSafeMultipleVariableManager<Foo1>(new { X = (string)null });
            var e = vm.GetEnumerator();
            IsTrue(e.MoveNext());
            IsNull(e.Current.X);
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_for_multiple_possible_values()
        {
            var vm = new TypeSafeMultipleVariableManager<Foo1>(new { X = new[] { "a" } });
            var e = vm.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("a", e.Current.X);
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_for_empty_multiple_possible_values()
        {
            var vm = new TypeSafeMultipleVariableManager<Foo1>(new { X = new string[] { } });
            var e = vm.GetEnumerator();
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_for_zero_variables()
        {
            var vm = new TypeSafeMultipleVariableManager<Foo0>(new { });
            var e = vm.GetEnumerator();
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_for_one_variable_and_3_values()
        {
            var vm = new TypeSafeMultipleVariableManager<Foo1>(new { X = new[] { "a", "b", "c" } });
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
        public void Works_for_one_variable_and_3_values_where_one_of_them_is_null()
        {
            var vm = new TypeSafeMultipleVariableManager<Foo1>(new { X = new[] { "a", null, "c" } });
            var e = vm.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("a", e.Current.X);
            IsTrue(e.MoveNext());
            AreEqual(null, e.Current.X);
            IsTrue(e.MoveNext());
            AreEqual("c", e.Current.X);
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_for_two_variables_and_3_2_values()
        {
            var vm = new TypeSafeMultipleVariableManager<Foo2>(new FooValues());
            var e = vm.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("a", e.Current.X);
            AreEqual(1, e.Current.Y);
            IsTrue(e.MoveNext());
            AreEqual("a", e.Current.X);
            AreEqual(2, e.Current.Y);
            IsTrue(e.MoveNext());
            AreEqual("b", e.Current.X);
            AreEqual(1, e.Current.Y);
            IsTrue(e.MoveNext());
            AreEqual("b", e.Current.X);
            AreEqual(2, e.Current.Y);
            IsTrue(e.MoveNext());
            AreEqual("c", e.Current.X);
            AreEqual(1, e.Current.Y);
            IsTrue(e.MoveNext());
            AreEqual("c", e.Current.X);
            AreEqual(2, e.Current.Y);
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_for_two_variables_and_3_2_values_provided_in_anonymous_object()
        {
            var vm = new TypeSafeMultipleVariableManager<Foo2>(new { X = new[] { "a", "b", "c" }, Y = new[] { 1, 2 } });
            var e = vm.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("a", e.Current.X);
            AreEqual(1, e.Current.Y);
            IsTrue(e.MoveNext());
            AreEqual("a", e.Current.X);
            AreEqual(2, e.Current.Y);
            IsTrue(e.MoveNext());
            AreEqual("b", e.Current.X);
            AreEqual(1, e.Current.Y);
            IsTrue(e.MoveNext());
            AreEqual("b", e.Current.X);
            AreEqual(2, e.Current.Y);
            IsTrue(e.MoveNext());
            AreEqual("c", e.Current.X);
            AreEqual(1, e.Current.Y);
            IsTrue(e.MoveNext());
            AreEqual("c", e.Current.X);
            AreEqual(2, e.Current.Y);
            IsFalse(e.MoveNext());
        }
    }
}
