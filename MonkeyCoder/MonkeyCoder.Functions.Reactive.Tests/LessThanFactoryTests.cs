using MonkeyCoder.Functions.Reactive;
using NUnit.Framework;
using System.Reactive.Linq;
using static NUnit.Framework.Assert;

namespace MonkeyCoder.Functions.Tests
{
    [TestFixture]
    public class LessThanFactoryTests : FactoryTestsBase
    {
        [Test]
        public void Works_with_number_and_sum_and_multiplication_and_equality_factories_and_1_then_2_then_3_as_data_source()
        {
            var factoryProviders = GetFactoryProvidersSource(
                new DefaultFactoryProvider<NumberFactory>(),
                new DefaultFactoryProvider<SumFactory>(),
                new DefaultFactoryProvider<MultiplicationFactory>(),
                new DefaultFactoryProvider<EqualityFactory>());
            var dataSource = GetDataSource(1, 2);

            var factory = new LessThanFactory { FactoryProvidersSource = factoryProviders, DataSource = dataSource, StackSize = 3 };
            var enumerator = factory.GetEnumerator();

            Assert(enumerator, "(1<(1*(1+1)))");
            Assert(enumerator, "(1<(1+1))");
            Assert(enumerator, "(1<(1+(1+1)))");
            Assert(enumerator, "(1<(1+(1*1)))");
            Assert(enumerator, "(2<(2*2))");
            Assert(enumerator, "(2<(2*(2+2)))");
            Assert(enumerator, "(2<(2*(2*2)))");
            Assert(enumerator, "(2<(1*(2+2)))");
            Assert(enumerator, "(2<(1*(2+1)))");
            Assert(enumerator, "(2<(1*(2*2)))");
            Assert(enumerator, "(2<(2+2))");
            Assert(enumerator, "(2<(2+(2+2)))");
            Assert(enumerator, "(2<(2+(2*2)))");
            Assert(enumerator, "(2<(1+2))");
            Assert(enumerator, "(2<(1+(1+1)))");
            Assert(enumerator, "(2<(1+(2+2)))");
            Assert(enumerator, "(2<(1+(2+1)))");
            Assert(enumerator, "(2<(1+(2*2)))");
            Assert(enumerator, "(2<(1+(2*1)))");
            IsFalse(enumerator.MoveNext());
        }
    }
}
