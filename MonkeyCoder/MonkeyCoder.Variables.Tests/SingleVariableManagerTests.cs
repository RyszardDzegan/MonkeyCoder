using NUnit.Framework;
using System;
using static NUnit.Framework.Assert;
using static NUnit.Framework.StringAssert;

namespace MonkeyCoder.Variables.Tests
{
    [TestFixture]
    public class SingleVariableManagerTests
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
        
        [Test]
        [ExpectedException(typeof(Exception))]
        public void Throws_exception_when_type_has_more_than_one_property()
        {
            try
            {
                new SingleVariableManager<Foo2>();
            }
            catch (Exception exception)
            {
                StartsWith(exception.Message, "Expected one property in Foo2 but found 2.");
                throw;
            }
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void Throws_exception_when_type_has_less_than_one_property()
        {
            try
            {
                new SingleVariableManager<Foo0>();
            }
            catch (Exception exception)
            {
                StartsWith(exception.Message, "Expected one property in Foo0 but found 0.");
                throw;
            }
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void Throws_exception_when_property_is_readonly()
        {
            try
            {
                new SingleVariableManager<NoSetterType>();
            }
            catch (Exception exception)
            {
                StartsWith("Property X must have setter.", exception.Message);
                throw;
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Throws_exception_when_possible_values_are_null()
        {
            try
            {
                new SingleVariableManager<Foo1>(null);
            }
            catch (Exception exception)
            {
                StartsWith("Possible values cannot be null.", exception.Message);
                throw;
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Throws_exception_when_possible_values_cannot_be_assigned_to_property()
        {
            try
            {
                new SingleVariableManager<Foo1>("a", 1, 2.345, "b");
            }
            catch (Exception exception)
            {
                StartsWith("Cannot assign the following values to X due to type mismatch: 1, 2,345.", exception.Message);
                throw;
            }
        }

        [Test]
        public void Doesnt_enter_into_loop_when_there_are_no_possible_values()
        {
            var vm = new SingleVariableManager<Foo1>();
            var e = vm.GetEnumerator();
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Enters_into_loop_one_time_when_possible_values_count_equals_one()
        {
            var vm = new SingleVariableManager<Foo1>("a");
            var e = vm.GetEnumerator();
            IsTrue(e.MoveNext());
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Enters_into_loop_two_times_when_possible_values_count_equals_two()
        {
            var vm = new SingleVariableManager<Foo1>("a", "b");
            var e = vm.GetEnumerator();
            IsTrue(e.MoveNext());
            IsTrue(e.MoveNext());
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_for_3_values()
        {
            var vm = new SingleVariableManager<Foo1>("a", "b", "c");
            var e = vm.GetEnumerator();
            IsTrue(e.MoveNext());
            AreEqual("a", e.Current.X);
            IsTrue(e.MoveNext());
            AreEqual("b", e.Current.X);
            IsTrue(e.MoveNext());
            AreEqual("c", e.Current.X);
            IsFalse(e.MoveNext());
        }

        [Test]
        public void Works_for_3_dynamic_values()
        {
            var vm = new SingleVariableManager<Foo1Dynamic>("a", 1, 2.3);
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
