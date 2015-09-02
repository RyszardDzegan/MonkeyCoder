using NUnit.Framework;
using System;
using System.Linq;
using static NUnit.Framework.Assert;

namespace MonkeyCoder.Variables.Tests
{
    [TestFixture]
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

        [Test]
        public void Returns_empty_enumerable_when_there_are_no_arguments()
        {
            var vm = VariableManagerFactory.Create<Foo1>();
            IsNotInstanceOf<SingleVariableManager<Foo1>>(vm);
            IsNotInstanceOf<MultipleVariableManager<Foo1>>(vm);
            IsFalse(vm.Any());
        }

        [Test]
        public void Returns_single_variable_manager_when_argument_is_a_params_array_of_values()
        {
            var vm = VariableManagerFactory.Create<Foo1>(1, 2, 3);
            IsInstanceOf<SingleVariableManager<Foo1>>(vm);
        }

        [Test]
        public void Returns_single_variable_manager_when_argument_is_an_array_of_values()
        {
            var vm = VariableManagerFactory.Create<Foo1>(new[] { 1, 2, 3 });
            IsInstanceOf<SingleVariableManager<Foo1>>(vm);
        }

        [Test]
        public void Returns_single_variable_manager_when_argument_is_an_enumerable_of_values()
        {
            var vm = VariableManagerFactory.Create<Foo1>(Enumerable.Range(0, 3));
            IsInstanceOf<SingleVariableManager<Foo1>>(vm);
        }

        [Test]
        public void Returns_multiple_variable_manager_when_variable_type_has_zero_properties_and_single_value_is_passed()
        {
            var vm = VariableManagerFactory.Create<Foo0>(1);
            IsInstanceOf<MultipleVariableManager<Foo0>>(vm);
        }

        [Test]
        public void Returns_single_variable_manager_when_variable_type_has_only_one_property_and_single_value_is_passed()
        {
            var vm = VariableManagerFactory.Create<Foo1>(1);
            IsInstanceOf<SingleVariableManager<Foo1>>(vm);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void Returns_multiple_variable_manager_when_variable_type_has_two_properties_and_single_value_is_passed()
        {
            var vm = VariableManagerFactory.Create<Foo2>(1);
            IsInstanceOf<MultipleVariableManager<Foo1>>(vm);
        }
    }
}
