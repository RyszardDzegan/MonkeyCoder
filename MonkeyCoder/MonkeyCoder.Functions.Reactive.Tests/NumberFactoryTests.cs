using MonkeyCoder.Functions.Reactive;
using NUnit.Framework;
using System.Reactive.Linq;
using static NUnit.Framework.Assert;

namespace MonkeyCoder.Functions.Tests
{
    [TestFixture]
    public class NumberFactoryTests : FactoryTestsBase
    {
        [Test]
        public void Works_with_matching_0_as_data_source()
        {
            var factoryProviders = GetFactoryProvidersSource(new NumberFactoryProvider<NumberFactory>());
            var dataSource = GetDataSource(0);

            var factory = new NumberFactory { FactoryProvidersSource = factoryProviders, DataSource = dataSource, Expected = new Number(0) };
            var enumerator = factory.GetEnumerator();

            Assert(enumerator, "0");
            IsFalse(enumerator.MoveNext());
        }

        [Test]
        public void Works_with_not_matching_0_as_data_source()
        {
            var factoryProviders = GetFactoryProvidersSource(new NumberFactoryProvider<NumberFactory>());
            var dataSource = GetDataSource(0);

            var factory = new NumberFactory { FactoryProvidersSource = factoryProviders, DataSource = dataSource, Expected = new Number(1) };
            var enumerator = factory.GetEnumerator();
            
            IsFalse(enumerator.MoveNext());
        }

        [Test]
        public void Works_with_expected_1_and_data_source_0_then_1()
        {
            var factoryProviders = GetFactoryProvidersSource(new NumberFactoryProvider<NumberFactory>());
            var dataSource = GetDataSource(0, 1);

            var factory = new NumberFactory { FactoryProvidersSource = factoryProviders, DataSource = dataSource, Expected = new Number(1) };
            var enumerator = factory.GetEnumerator();

            Assert(enumerator, "1");
            IsFalse(enumerator.MoveNext());
        }
    }
}
