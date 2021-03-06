﻿using MonkeyCoder.Functions.Reactive;
using NUnit.Framework;
using System.Reactive.Linq;
using static NUnit.Framework.Assert;

namespace MonkeyCoder.Functions.Tests
{
    [TestFixture]
    public class IfElseFactoryTests : FactoryTestsBase
    {
        [Test]
        public void Works_with_number_and_less_than_factories_and_1_then_2_then_3_as_data_source()
        {
            var factoryProviders = GetFactoryProvidersSource(
                new DefaultFactoryProvider<NumberFactory>(),
                new DefaultFactoryProvider<LessThanFactory>());
            var dataSource = GetDataSource(1, 2, 3);

            var factory = new IfElseFactory { FactoryProvidersSource = factoryProviders, DataSource = dataSource, StackSize = 5 };
            var enumerator = factory.GetEnumerator();

            Assert(enumerator, "((1<2)?2:1)");
            Assert(enumerator, "((1<2)?1:2)");
            Assert(enumerator, "((2<3)?3:2)");
            Assert(enumerator, "((2<3)?3:1)");
            Assert(enumerator, "((2<3)?2:3)");
            Assert(enumerator, "((2<3)?2:1)");
            Assert(enumerator, "((2<3)?1:3)");
            Assert(enumerator, "((2<3)?1:2)");
            Assert(enumerator, "((1<2)?3:2)");
            Assert(enumerator, "((1<2)?3:1)");
            Assert(enumerator, "((1<2)?2:3)");
            Assert(enumerator, "((1<2)?2:1)");
            Assert(enumerator, "((1<2)?1:3)");
            Assert(enumerator, "((1<2)?1:2)");
            Assert(enumerator, "((1<3)?3:2)");
            Assert(enumerator, "((1<3)?3:1)");
            Assert(enumerator, "((1<3)?2:3)");
            Assert(enumerator, "((1<3)?2:1)");
            Assert(enumerator, "((1<3)?1:3)");
            Assert(enumerator, "((1<3)?1:2)");
            IsFalse(enumerator.MoveNext());
        }
    }
}
