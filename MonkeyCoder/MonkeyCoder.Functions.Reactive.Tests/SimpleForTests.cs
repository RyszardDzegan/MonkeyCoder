using MonkeyCoder.Functions.Reactive;
using NUnit.Framework;
using System.Reactive.Linq;
using static NUnit.Framework.Assert;

namespace MonkeyCoder.Functions.Tests
{
    [TestFixture]
    public class SimpleForTests : FactoryTestsBase
    {
        [Test]
        public void Works_with_number_and_less_than_factories_and_1_then_2_then_3_as_data_source()
        {
            var factoryProviders = GetFactoryProvidersSource(
                new DefaultFactoryProvider<NumberFactory>(),
                new DefaultFactoryProvider<LessThanFactory>());
            var dataSource = GetDataSource(1, 2, 3);

            var factory = new SimpleForFactory { FactoryProvidersSource = factoryProviders, DataSource = dataSource, StackSize = 5 };
            var enumerator = factory.GetEnumerator();

            Assert(enumerator, "for (i = 0; (i<1); i++) { 1 }");
            Assert(enumerator, "for (i = 0; (i<1); i++) { 0 }");
            Assert(enumerator, "for (i = 0; (i<0); i++) { 1 }");
            Assert(enumerator, "for (i = 0; (i<0); i++) { 0 }");
            Assert(enumerator, "for (i = 0; (i<2); i++) { 2 }");
            Assert(enumerator, "for (i = 0; (i<2); i++) { 1 }");
            Assert(enumerator, "for (i = 0; (i<2); i++) { 0 }");
            Assert(enumerator, "for (i = 0; (i<1); i++) { 2 }");
            Assert(enumerator, "for (i = 0; (i<1); i++) { 1 }");
            Assert(enumerator, "for (i = 0; (i<1); i++) { 0 }");
            Assert(enumerator, "for (i = 0; (i<0); i++) { 2 }");
            Assert(enumerator, "for (i = 0; (i<0); i++) { 1 }");
            Assert(enumerator, "for (i = 0; (i<0); i++) { 0 }");
            Assert(enumerator, "for (i = 0; (i<3); i++) { 3 }");
            Assert(enumerator, "for (i = 0; (i<3); i++) { 2 }");
            Assert(enumerator, "for (i = 0; (i<3); i++) { 1 }");
            Assert(enumerator, "for (i = 0; (i<3); i++) { 0 }");
            Assert(enumerator, "for (i = 0; (i<2); i++) { 3 }");
            Assert(enumerator, "for (i = 0; (i<2); i++) { 2 }");
            Assert(enumerator, "for (i = 0; (i<2); i++) { 1 }");
            Assert(enumerator, "for (i = 0; (i<2); i++) { 0 }");
            Assert(enumerator, "for (i = 0; (i<1); i++) { 3 }");
            Assert(enumerator, "for (i = 0; (i<1); i++) { 2 }");
            Assert(enumerator, "for (i = 0; (i<1); i++) { 1 }");
            Assert(enumerator, "for (i = 0; (i<1); i++) { 0 }");
            Assert(enumerator, "for (i = 0; (i<0); i++) { 3 }");
            Assert(enumerator, "for (i = 0; (i<0); i++) { 2 }");
            Assert(enumerator, "for (i = 0; (i<0); i++) { 1 }");
            Assert(enumerator, "for (i = 0; (i<0); i++) { 0 }");
            IsFalse(enumerator.MoveNext());
        }
    }
}
