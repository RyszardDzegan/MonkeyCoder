using MonkeyCoder.Functions.Reactive;
using NUnit.Framework;
using System.Reactive.Linq;
using static NUnit.Framework.Assert;

namespace MonkeyCoder.Functions.Tests
{
    [TestFixture]
    public class EquFactoryTests : FactoryTestsBase
    {
        [Test]
        public void Works_with_number_and_sum_factory_and_true_as_expected_and_1_then_2_then_3_as_data_source()
        {
            var factoryProviders = GetFactoryProvidersSource(
                new NumberFactoryProvider<NumberFactory>(),
                new NumberFactoryProvider<SumFactory>());
            var dataSource = GetDataSource(1, 2, 3);

            var factory = new EquFactory { FactoryProvidersSource = factoryProviders, DataSource = dataSource, Expected = new Boolean(true), StackSize = 5 };
            var enumerator = factory.GetEnumerator();

            Assert(enumerator, "(1==1)");
            Assert(enumerator, "(2==(1+1))");
            Assert(enumerator, "(2==2)");
            Assert(enumerator, "(3==(1+2))");
            Assert(enumerator, "(3==(1+(1+1)))");
            Assert(enumerator, "(3==3)");
            IsFalse(enumerator.MoveNext());
        }
    }
}
