using NUnit.Framework;
using static NUnit.Framework.Assert;

namespace MonkeyCoder.Math.Tests
{
    [TestFixture]
    public class CombinatoricsTests
    {
        [Test]
        public void Cartesian_product_works()
        {
            var sut = new[] { new[] { "a", "b" }, new[] { "A", "B" } }.AsCartesianProduct();
            IsInstanceOf<CartesianProduct<string>>(sut);
        }

        [Test]
        public void Combinations_works()
        {
            var sut = new[] { 1, 2, 3 }.AsCombinations(1);
            IsInstanceOf<Combinations<int>>(sut);
        }

        [Test]
        public void Variations_with_repetitions_works()
        {
            var sut = new[] { 1, 2, 3 }.AsVariationsWithRepetitions(1);
            IsInstanceOf<VariationsWithRepetitions<int>>(sut);
        }

        [Test]
        public void Variations_without_repetitions_works()
        {
            var sut = new[] { 1, 2, 3 }.AsVariationsWithoutRepetitions(1);
            IsInstanceOf<VariationsWithoutRepetitions<int>>(sut);
        }

        [Test]
        public void Power_set_works()
        {
            var sut = new[] { 1, 2, 3 }.AsPowerSet();
            IsInstanceOf<PowerSet<int>>(sut);
        }
    }
}
