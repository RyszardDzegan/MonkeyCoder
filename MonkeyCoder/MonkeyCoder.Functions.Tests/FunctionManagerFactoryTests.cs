using NUnit.Framework;
using static NUnit.Framework.Assert;

namespace MonkeyCoder.Functions.Tests
{
    [TestFixture]
    public class FunctionManagerFactoryTests
    {
        [Test]
        public void Returns_function_manager()
        {
            var items = new object[] { 1, 2, 3 };
            var tree = items.AsFunctionsTree();
            IsInstanceOf<FunctionManager>(tree);
        }
    }
}
