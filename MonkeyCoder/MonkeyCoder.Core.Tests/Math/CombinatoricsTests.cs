using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonkeyCoder.Core.Math;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace MonkeyCoder.Core.Tests.Math
{
    [TestClass]
    public class CombinatoricsTests
    {
        [TestMethod]
        public void Cartesian_product_works()
        {
            var sut = new[] { new[] { "a", "b" }, new[] { "A", "B" } }.AsCartesianProduct();
            IsInstanceOfType(sut, typeof(CartesianProduct<string>));
        }

        [TestMethod]
        public void Combinations_works()
        {
            var sut = new[] { 1, 2, 3 }.AsCombinations(1);
            IsInstanceOfType(sut, typeof(Combinations<int>));
        }

        [TestMethod]
        public void Variations_with_repetitions_works()
        {
            var sut = new[] { 1, 2, 3 }.AsVariationsWithRepetitions(1);
            IsInstanceOfType(sut, typeof(VariationsWithRepetitions<int>));
        }
    }
}
