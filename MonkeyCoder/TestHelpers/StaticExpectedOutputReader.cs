using System.Diagnostics;

namespace TestHelpers
{
    public static class StaticExpectedOutputReader
    {
        private static string ExpectedOutputsFileName { get; set; }
        private static ExpectedOutputReader ExpectedOutputReader { get; set; }

        public static string GetExpectedTestOutput()
        {
            var stackFrame = new StackFrame(1);
            var callingMethod = stackFrame.GetMethod();
            var testMethodName = callingMethod.Name;
            var reflectedType = callingMethod.ReflectedType;
            var expectedOutputsFileName = reflectedType.Name + "ExpectedOutputs.txt";

            if (ExpectedOutputsFileName != expectedOutputsFileName)
            {
                ExpectedOutputsFileName = expectedOutputsFileName;
                ExpectedOutputReader = new ExpectedOutputReader(ExpectedOutputsFileName, reflectedType);
            }

            return ExpectedOutputReader.GetExpectedTestOutput(testMethodName);
        }
    }
}
