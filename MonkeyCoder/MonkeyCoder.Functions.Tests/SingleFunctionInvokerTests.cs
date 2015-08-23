using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MonkeyCoder.Functions.Tests
{
    [TestClass]
    public class SingleFunctionInvokerTests : CommonSingleFunctionInvokerTests
    {
        internal override ISingleFunctionInvoker GetInvoker(Delegate function, params object[] possibleArguments) => new SingleFunctionInvoker(function, possibleArguments);

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Throws_argument_null_exception_when_function_is_null()
        {
            GetInvoker(null);
        }
    }
}
