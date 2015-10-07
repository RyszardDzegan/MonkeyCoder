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
        public void Works_with_number_factory_0_as_data_source()
        {
            var factoryProviders = GetFactoryProvidersSource(new DefaultFactoryProvider<NumberFactory>());
            var dataSource = GetDataSource(0);

            var factory = new SumFactory { FactoryProvidersSource = factoryProviders, DataSource = dataSource, StackSize = 3 };
            var enumerator = factory.GetEnumerator();

            Assert(enumerator, "(0+0)");
            IsFalse(enumerator.MoveNext());
        }

        [Test]
        public void Works_with_number_factory_and_0_then_1_as_data_source()
        {
            var factoryProviders = GetFactoryProvidersSource(new DefaultFactoryProvider<NumberFactory>());
            var dataSource = GetDataSource(0, 1);

            var factory = new SumFactory { FactoryProvidersSource = factoryProviders, DataSource = dataSource, StackSize = 3 };
            var enumerator = factory.GetEnumerator();

            Assert(enumerator, "(0+0)");
            Assert(enumerator, "(1+1)");
            Assert(enumerator, "(1+0)");
            IsFalse(enumerator.MoveNext());
        }

        [Test]
        public void Works_with_number_factory_and_0_then_1_then_2_as_data_source()
        {
            var factoryProviders = GetFactoryProvidersSource(new DefaultFactoryProvider<NumberFactory>());
            var dataSource = GetDataSource(0, 1, 2);

            var factory = new SumFactory { FactoryProvidersSource = factoryProviders, DataSource = dataSource, StackSize = 3 };
            var enumerator = factory.GetEnumerator();
            
            Assert(enumerator, "(0+0)");
            Assert(enumerator, "(1+1)");
            Assert(enumerator, "(1+0)");
            Assert(enumerator, "(2+2)");
            Assert(enumerator, "(2+1)");
            Assert(enumerator, "(2+0)");
            IsFalse(enumerator.MoveNext());
        }

        [Test]
        public void Works_with_sum_factory_and_0_as_data_source()
        {
            var factoryProviders = GetFactoryProvidersSource(new DefaultFactoryProvider<SumFactory>());
            var dataSource = GetDataSource(0);

            var factory = new SumFactory { FactoryProvidersSource = factoryProviders, DataSource = dataSource, StackSize = 3 };
            var enumerator = factory.GetEnumerator();
            
            IsFalse(enumerator.MoveNext());
        }

        [Test]
        public void Works_with_sum_factory_and_0_then_1_as_data_source()
        {
            var factoryProviders = GetFactoryProvidersSource(new DefaultFactoryProvider<SumFactory>());
            var dataSource = GetDataSource(0, 1);

            var factory = new SumFactory { FactoryProvidersSource = factoryProviders, DataSource = dataSource, StackSize = 3 };
            var enumerator = factory.GetEnumerator();

            IsFalse(enumerator.MoveNext());
        }

        [Test]
        public void Works_with_number_and_sum_factory_and_0_as_data_source_and_3_as_stack_size()
        {
            var factoryProviders = GetFactoryProvidersSource(
                new DefaultFactoryProvider<NumberFactory>(),
                new DefaultFactoryProvider<SumFactory>());
            var dataSource = GetDataSource(0);

            var factory = new SumFactory { FactoryProvidersSource = factoryProviders, DataSource = dataSource, StackSize = 3 };
            var enumerator = factory.GetEnumerator();

            Assert(enumerator, "(0+(0+0))");
            Assert(enumerator, "(0+(0+(0+0)))");
            Assert(enumerator, "(0+0)");
            IsFalse(enumerator.MoveNext());
        }

        [Test]
        public void Works_with_number_and_sum_factory_and_0_as_data_source_and_2_as_stack_size()
        {
            var factoryProviders = GetFactoryProvidersSource(
                new DefaultFactoryProvider<NumberFactory>(),
                new DefaultFactoryProvider<SumFactory>());
            var dataSource = GetDataSource(0);

            var factory = new SumFactory { FactoryProvidersSource = factoryProviders, DataSource = dataSource, StackSize = 2 };
            var enumerator = factory.GetEnumerator();

            Assert(enumerator, "(0+(0+0))");
            Assert(enumerator, "(0+0)");
            IsFalse(enumerator.MoveNext());
        }

        [Test]
        public void Works_with_number_and_sum_factory_and_0_then_1_as_data_source()
        {
            var factoryProviders = GetFactoryProvidersSource(
                new DefaultFactoryProvider<NumberFactory>(),
                new DefaultFactoryProvider<SumFactory>());
            var dataSource = GetDataSource(0, 1);

            var factory = new SumFactory { FactoryProvidersSource = factoryProviders, DataSource = dataSource, StackSize = 3 };
            var enumerator = factory.GetEnumerator();

            Assert(enumerator, "(0+(0+0))");
            Assert(enumerator, "(0+(0+(0+0)))");
            Assert(enumerator, "(0+0)");
            Assert(enumerator, "(1+(1+1))");
            Assert(enumerator, "(1+(1+(1+1)))");
            Assert(enumerator, "(1+(0+0))");
            Assert(enumerator, "(1+(0+1))");
            Assert(enumerator, "(1+(0+(0+0)))");
            Assert(enumerator, "(1+(0+(1+1)))");
            Assert(enumerator, "(1+(0+(1+0)))");
            Assert(enumerator, "(1+1)");
            Assert(enumerator, "(1+0)");
            IsFalse(enumerator.MoveNext());
        }

        [Test]
        public void Works_with_number_and_sum_factory_and_1_then_2_as_data_source()
        {
            var factoryProviders = GetFactoryProvidersSource(
                new DefaultFactoryProvider<NumberFactory>(),
                new DefaultFactoryProvider<SumFactory>());
            var dataSource = GetDataSource(1, 2);

            var factory = new SumFactory { FactoryProvidersSource = factoryProviders, DataSource = dataSource, StackSize = 3 };
            var enumerator = factory.GetEnumerator();

            Assert(enumerator, "(1+(1+1))");
            Assert(enumerator, "(1+(1+(1+1)))");
            Assert(enumerator, "(1+1)");
            Assert(enumerator, "(2+(2+2))");
            Assert(enumerator, "(2+(2+(2+2)))");
            Assert(enumerator, "(2+(1+1))");
            Assert(enumerator, "(2+(1+2))");
            Assert(enumerator, "(2+(1+(1+1)))");
            Assert(enumerator, "(2+(1+(2+2)))");
            Assert(enumerator, "(2+(1+(2+1)))");
            Assert(enumerator, "(2+2)");
            Assert(enumerator, "(2+1)");
            IsFalse(enumerator.MoveNext());
        }

        [Test]
        public void Works_with_number_and_contrariety_factory_and_1_then_2_as_data_source()
        {
            var factoryProviders = GetFactoryProvidersSource(
                new DefaultFactoryProvider<NumberFactory>(),
                new DefaultFactoryProvider<ContrarietyFactory>());
            var dataSource = GetDataSource(1, 2);

            var factory = new SumFactory { FactoryProvidersSource = factoryProviders, DataSource = dataSource, StackSize = 3 };
            var enumerator = factory.GetEnumerator();

            Assert(enumerator, "(1+(-1))");
            Assert(enumerator, "(1+1)");
            Assert(enumerator, "(2+(-2))");
            Assert(enumerator, "(2+(-1))");
            Assert(enumerator, "(2+2)");
            Assert(enumerator, "(2+1)");
            IsFalse(enumerator.MoveNext());
        }
    }
}
