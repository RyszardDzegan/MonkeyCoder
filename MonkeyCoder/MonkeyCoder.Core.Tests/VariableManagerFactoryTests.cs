using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace MonkeyCoder.Core.Tests
{
    [TestClass]
    public class VariableManagerFactoryTests
    {
        class Foo0 { }

        class Foo1
        {
            public int X { get; set; }
        }

        class Foo2
        {
            public int X { get; set; }
            public int Y { get; set; }
        }
        
        [TestMethod]
        public void Returns_single_variable_manager_when_argument_is_a_params_array_of_values()
        {
            var vm = VariableManagerFactory.Create<Foo1>(1, 2, 3);
            IsInstanceOfType(vm, typeof(TypeSafeSingleVariableManager<Foo1>));
        }

        [TestMethod]
        public void Returns_single_variable_manager_when_argument_is_an_array_of_values()
        {
            var vm = VariableManagerFactory.Create<Foo1>(new[] { 1, 2, 3 });
            IsInstanceOfType(vm, typeof(TypeSafeSingleVariableManager<Foo1>));
        }

        [TestMethod]
        public void Returns_single_variable_manager_when_argument_is_an_enumerable_of_values()
        {
            var vm = VariableManagerFactory.Create<Foo1>(Enumerable.Range(0, 3));
            IsInstanceOfType(vm, typeof(TypeSafeSingleVariableManager<Foo1>));
        }

        [TestMethod]
        public void Returns_multiple_variable_manager_when_variable_type_has_zero_properties_and_single_value_is_passed()
        {
            var vm = VariableManagerFactory.Create<Foo0>(1);
            IsInstanceOfType(vm, typeof(TypeSafeMultipleVariableManager<Foo0>));
        }

        [TestMethod]
        public void Returns_single_variable_manager_when_variable_type_has_only_one_property_and_single_value_is_passed()
        {
            var vm = VariableManagerFactory.Create<Foo1>(1);
            IsInstanceOfType(vm, typeof(TypeSafeSingleVariableManager<Foo1>));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Returns_multiple_variable_manager_when_variable_type_has_two_properties_and_single_value_is_passed()
        {
            var vm = VariableManagerFactory.Create<Foo2>(1);
            IsInstanceOfType(vm, typeof(TypeSafeMultipleVariableManager<Foo2>));
        }
    }
}
