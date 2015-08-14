using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace MonkeyCoder.Core.Tests
{
    [TestClass]
    public class EmptyVariableManagerTests
    {
        [TestMethod]
        public void Enters_into_loop_once()
        {
            var vm = new EmptyVariableManager();

            var loopCount = 0;
            foreach (var vb in vm)
                loopCount++;

            AreEqual(1, loopCount);
        }
    }
}
