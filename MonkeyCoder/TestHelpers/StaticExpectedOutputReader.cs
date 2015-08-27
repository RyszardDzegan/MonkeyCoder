using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace TestHelpers
{
    public static class StaticExpectedOutputReader
    {
        private static string ExpectedOutputsFileName { get; set; }
        private static ExpectedOutputReader ExpectedOutputReader { get; set; }

        public static string GetExpectedTestOutput()
        {
            StackFrame stackFrame = null;
            MethodBase callingMethod = null;
            string testMethodName = null;
            Type reflectedType = null;
            string expectedOutputsFileName = null;

            for (var i = 1; ;i++)
            {
                stackFrame = new StackFrame(i);
                callingMethod = stackFrame.GetMethod();
                testMethodName = callingMethod.Name;
                reflectedType = callingMethod.ReflectedType;

                if (callingMethod.MethodImplementationFlags != MethodImplAttributes.IL)
                    break;

                var expectedOutputAttribute = (ExpectedOutputAttribute)callingMethod.GetCustomAttributes(typeof(ExpectedOutputAttribute), false).LastOrDefault();

                if (expectedOutputAttribute != null)
                {
                    expectedOutputsFileName = expectedOutputAttribute.FileName;
                    break;
                }
            }

            if (expectedOutputsFileName == null)
            {
                stackFrame = new StackFrame(1);
                callingMethod = stackFrame.GetMethod();
                testMethodName = callingMethod.Name;
                reflectedType = callingMethod.ReflectedType;
                expectedOutputsFileName = reflectedType.Name + "ExpectedOutputs.txt";
            }

            if (ExpectedOutputsFileName != expectedOutputsFileName)
            {
                ExpectedOutputsFileName = expectedOutputsFileName;
                ExpectedOutputReader = new ExpectedOutputReader(ExpectedOutputsFileName, reflectedType);
            }

            return ExpectedOutputReader.GetExpectedTestOutput(testMethodName);
        }
    }
}
