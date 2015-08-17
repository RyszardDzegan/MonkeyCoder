using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using static Microsoft.VisualStudio.TestTools.UnitTesting.StringAssert;

namespace MonkeyCoder.Core.Tests
{
    [TestClass]
    public class TypeSafeSingleVariableManagerTests
    {
        class Foo0 { }

        class Foo1
        {
            public string X { get; set; }
        }

        class Foo2
        {
            public string X { get; set; }
            public string Y { get; set; }
        }

        class Foo1Dynamic
        {
            public dynamic X { get; set; }
        }
        
        class NoSetterType
        {
            public string X { get; }
        }
        
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Throws_exception_when_type_has_more_than_one_property()
        {
            try
            {
                new TypeSafeSingleVariableManager<Foo2>();
            }
            catch (Exception exception)
            {
                StartsWith(exception.Message, "Expected one property in Foo2 but found 2.");
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Throws_exception_when_type_has_less_than_one_property()
        {
            try
            {
                new TypeSafeSingleVariableManager<Foo0>();
            }
            catch (Exception exception)
            {
                StartsWith(exception.Message, "Expected one property in Foo0 but found 0.");
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Throws_exception_when_property_is_readonly()
        {
            try
            {
                new TypeSafeSingleVariableManager<NoSetterType>();
            }
            catch (Exception exception)
            {
                StartsWith(exception.Message, "Property X must have setter.");
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Throws_exception_when_possible_values_are_null()
        {
            try
            {
                new TypeSafeSingleVariableManager<Foo1>(null);
            }
            catch (Exception exception)
            {
                StartsWith(exception.Message, "Possible values cannot be null.");
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Throws_exception_when_possible_values_cannot_be_assigned_to_property()
        {
            try
            {
                new TypeSafeSingleVariableManager<Foo1>("a", 1, 2.345, "b");
            }
            catch (Exception exception)
            {
                StartsWith(exception.Message, "Cannot assign the following values to X due to type mismatch: 1, 2,345.");
                throw;
            }
        }

        [TestMethod]
        public void Doesnt_enter_into_loop_when_there_are_no_possible_values()
        {
            var vm = new TypeSafeSingleVariableManager<Foo1>();
            var e = vm.GetEnumerator();
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Enters_into_loop_one_time_when_possible_values_count_equals_one()
        {
            var vm = new TypeSafeSingleVariableManager<Foo1>("a");
            var e = vm.GetEnumerator();
            IsTrue(e.MoveNext());
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Enters_into_loop_two_times_when_possible_values_count_equals_two()
        {
            var vm = new TypeSafeSingleVariableManager<Foo1>("a", "b");
            var e = vm.GetEnumerator();
            IsTrue(e.MoveNext());
            IsTrue(e.MoveNext());
            IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Works_for_3_values()
        {
            var vm = new TypeSafeSingleVariableManager<Foo1>("a", "b", "c");
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
        public void Works_for_3_dynamic_values()
        {
            var vm = new TypeSafeSingleVariableManager<Foo1Dynamic>("a", 1, 2.3);
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
