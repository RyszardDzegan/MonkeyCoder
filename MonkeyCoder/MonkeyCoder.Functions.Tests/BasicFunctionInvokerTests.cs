using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace MonkeyCoder.Functions.Tests
{
    [TestClass]
    public class BasicFunctionInvokerTests : CommonSingleFunctionInvokerTests
    {
        internal override IEnumerable<Func<object>> GetInvoker(Delegate function, params object[] possibleArguments) => new BasicFunctionInvoker(function, possibleArguments);

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Throws_argument_null_exception_when_function_is_null()
        {
            GetInvoker(null);
        }
    }
}
