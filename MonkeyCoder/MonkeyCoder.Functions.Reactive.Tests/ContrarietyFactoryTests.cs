using MonkeyCoder.Functions.Reactive;
using NUnit.Framework;
using System.Reactive.Linq;
using static NUnit.Framework.Assert;

namespace MonkeyCoder.Functions.Tests
{
    [TestFixture]
    public class ContrarietyFactoryTests : FactoryTestsBase
    {
        [Test]
        public void Works_with_1_then_2_as_data_source()
        {
            var factoryProviders = GetFactoryProvidersSource();
            var dataSource = GetDataSource(1, 2);

            var factory = new ContrarietyFactory { FactoryProvidersSource = factoryProviders, DataSource = dataSource, StackSize = 3 };
            var enumerator = factory.GetEnumerator();

            Assert(enumerator, "(-1)");
            Assert(enumerator, "(-2)");
            IsFalse(enumerator.MoveNext());
        }
    }
}
