using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonkeyCoder.Core.Math;
using System;

namespace MonkeyCoder.Core.Tests
{
    [TestClass]
    public class VariationsTests
    {
        [TestMethod]
        public void Works_with_3_2_items()
        {
            var variations = new Variations<string>(new[] { new[] { "a", "b", "c" }, new[] { "A", "B" } });
            foreach (var variation in variations)
            {
                var result = string.Join("", variation);
                Console.WriteLine(result);
            }
        }
    }
}
