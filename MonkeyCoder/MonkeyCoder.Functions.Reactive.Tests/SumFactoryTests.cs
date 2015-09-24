using MonkeyCoder.Functions.Reactive;
using NUnit.Framework;
using System.Reactive.Linq;
using static NUnit.Framework.Assert;

namespace MonkeyCoder.Functions.Tests
{
    [TestFixture]
    public class SumFactoryTests : FactoryTestsBase
    {
        [Test]
        public void Works_with_number_factory_and_matching_0_as_data_source()
        {
            var factoryProviders = GetFactoryProvidersSource(new NumberFactoryProvider<NumberFactory>());
            var dataSource = GetDataSource(0);

            var factory = new SumFactory { FactoryProvidersSource = factoryProviders, DataSource = dataSource, Expected = new Number(0), StackSize = 3 };
            var enumerator = factory.GetEnumerator();

            Assert(enumerator, "(0+0)");
            IsFalse(enumerator.MoveNext());
        }

        [Test]
        public void Works_with_number_factory_and_1_as_expected_and_0_then_1_as_data_source()
        {
            var factoryProviders = GetFactoryProvidersSource(new NumberFactoryProvider<NumberFactory>());
            var dataSource = GetDataSource(0, 1);

            var factory = new SumFactory { FactoryProvidersSource = factoryProviders, DataSource = dataSource, Expected = new Number(1), StackSize = 3 };
            var enumerator = factory.GetEnumerator();

            Assert(enumerator, "(1+0)");
            IsFalse(enumerator.MoveNext());
        }

        [Test]
        public void Works_with_number_factory_and_2_as_expected_and_0_then_1_then_2_as_data_source()
        {
            var factoryProviders = GetFactoryProvidersSource(new NumberFactoryProvider<NumberFactory>());
            var dataSource = GetDataSource(0, 1, 2);

            var factory = new SumFactory { FactoryProvidersSource = factoryProviders, DataSource = dataSource, Expected = new Number(2), StackSize = 3 };
            var enumerator = factory.GetEnumerator();
            
            Assert(enumerator, "(1+1)");
            Assert(enumerator, "(2+0)");
            IsFalse(enumerator.MoveNext());
        }

        [Test]
        public void Works_with_sum_factory_and_0_as_expected_and_0_as_data_source()
        {
            var factoryProviders = GetFactoryProvidersSource(new NumberFactoryProvider<SumFactory>());
            var dataSource = GetDataSource(0);

            var factory = new SumFactory { FactoryProvidersSource = factoryProviders, DataSource = dataSource, Expected = new Number(0), StackSize = 3 };
            var enumerator = factory.GetEnumerator();
            
            IsFalse(enumerator.MoveNext());
        }

        [Test]
        public void Works_with_sum_factory_and_0_as_expected_and_0_then_1_as_data_source()
        {
            var factoryProviders = GetFactoryProvidersSource(new NumberFactoryProvider<SumFactory>());
            var dataSource = GetDataSource(0, 1);

            var factory = new SumFactory { FactoryProvidersSource = factoryProviders, DataSource = dataSource, Expected = new Number(0), StackSize = 3 };
            var enumerator = factory.GetEnumerator();

            IsFalse(enumerator.MoveNext());
        }

        [Test]
        public void Works_with_sum_factory_and_1_as_expected_and_0_then_1_as_data_source()
        {
            var factoryProviders = GetFactoryProvidersSource(new NumberFactoryProvider<SumFactory>());
            var dataSource = GetDataSource(0, 1);

            var factory = new SumFactory { FactoryProvidersSource = factoryProviders, DataSource = dataSource, Expected = new Number(1), StackSize = 3 };
            var enumerator = factory.GetEnumerator();

            IsFalse(enumerator.MoveNext());
        }

        [Test]
        public void Works_with_number_and_sum_factory_and_0_as_expected_and_0_as_data_source_and_3_as_stack_size()
        {
            var factoryProviders = GetFactoryProvidersSource(
                new NumberFactoryProvider<NumberFactory>(),
                new NumberFactoryProvider<SumFactory>());
            var dataSource = GetDataSource(0);

            var factory = new SumFactory { FactoryProvidersSource = factoryProviders, DataSource = dataSource, Expected = new Number(0), StackSize = 3 };
            var enumerator = factory.GetEnumerator();

            Assert(enumerator, "(0+(0+0))");
            Assert(enumerator, "(0+(0+(0+0)))");
            Assert(enumerator, "(0+0)");
            IsFalse(enumerator.MoveNext());
        }

        [Test]
        public void Works_with_number_and_sum_factory_and_0_as_expected_and_0_as_data_source_and_2_as_stack_size()
        {
            var factoryProviders = GetFactoryProvidersSource(
                new NumberFactoryProvider<NumberFactory>(),
                new NumberFactoryProvider<SumFactory>());
            var dataSource = GetDataSource(0);

            var factory = new SumFactory { FactoryProvidersSource = factoryProviders, DataSource = dataSource, Expected = new Number(0), StackSize = 2 };
            var enumerator = factory.GetEnumerator();

            Assert(enumerator, "(0+(0+0))");
            Assert(enumerator, "(0+0)");
            IsFalse(enumerator.MoveNext());
        }

        [Test]
        public void Works_with_number_and_sum_factory_and_1_as_expected_and_0_then_1_as_data_source()
        {
            var factoryProviders = GetFactoryProvidersSource(
                new NumberFactoryProvider<NumberFactory>(),
                new NumberFactoryProvider<SumFactory>());
            var dataSource = GetDataSource(0, 1);

            var factory = new SumFactory { FactoryProvidersSource = factoryProviders, DataSource = dataSource, Expected = new Number(1), StackSize = 3 };
            var enumerator = factory.GetEnumerator();

            Assert(enumerator, "(1+(0+0))");
            Assert(enumerator, "(1+(0+(0+0)))");
            Assert(enumerator, "(1+0)");
            IsFalse(enumerator.MoveNext());
        }

        [Test]
        public void Works_with_number_and_sum_factory_and_3_as_expected_and_1_then_2_as_data_source()
        {
            var factoryProviders = GetFactoryProvidersSource(
                new NumberFactoryProvider<NumberFactory>(),
                new NumberFactoryProvider<SumFactory>());
            var dataSource = GetDataSource(1, 2);

            var factory = new SumFactory { FactoryProvidersSource = factoryProviders, DataSource = dataSource, Expected = new Number(3), StackSize = 3 };
            var enumerator = factory.GetEnumerator();

            Assert(enumerator, "(1+(1+1))");
            Assert(enumerator, "(2+1)");
            IsFalse(enumerator.MoveNext());
        }

        [Test]
        public void Works_with_number_and_sum_factory_and_6_as_expected_and_1_then_2_then_3_as_data_source()
        {
            var factoryProviders = GetFactoryProvidersSource(
                new NumberFactoryProvider<NumberFactory>(),
                new NumberFactoryProvider<SumFactory>());
            var dataSource = GetDataSource(1, 2, 3);

            var factory = new SumFactory { FactoryProvidersSource = factoryProviders, DataSource = dataSource, Expected = new Number(6), StackSize = 5 };
            var enumerator = factory.GetEnumerator();

            Assert(enumerator, "(1+(1+(1+(1+(1+1)))))");
            Assert(enumerator, "(2+(2+2))");
            Assert(enumerator, "(2+(1+(1+(1+1))))");
            Assert(enumerator, "(2+(1+(2+1)))");
            Assert(enumerator, "(3+(1+2))");
            Assert(enumerator, "(3+(1+(1+1)))");
            Assert(enumerator, "(3+3)");
            IsFalse(enumerator.MoveNext());
        }
    }
}
