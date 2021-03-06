﻿using MonkeyCoder.Functions.Reactive;
using NUnit.Framework;
using System.Reactive.Linq;
using static NUnit.Framework.Assert;

namespace MonkeyCoder.Functions.Tests
{
    [TestFixture]
    public class EqualityFactoryTests : FactoryTestsBase
    {
        [Test]
        public void Works_with_number_and_sum_factories_and_1_then_2_then_3_as_data_source()
        {
            var factoryProviders = GetFactoryProvidersSource(
                new DefaultFactoryProvider<NumberFactory>(),
                new DefaultFactoryProvider<SumFactory>());
            var dataSource = GetDataSource(1, 2, 3);

            var factory = new EqualityFactory { FactoryProvidersSource = factoryProviders, DataSource = dataSource, StackSize = 5 };
            var enumerator = factory.GetEnumerator();
            
            Assert(enumerator, "(2=(1+1))");
            Assert(enumerator, "(3=(1+2))");
            Assert(enumerator, "(3=(1+(1+1)))");
            IsFalse(enumerator.MoveNext());
        }
    }
}
