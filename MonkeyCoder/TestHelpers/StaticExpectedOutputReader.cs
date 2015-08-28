using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace TestHelpers
{
    public static class StaticExpectedOutputReader
    {
        private static IDictionary<string, ExpectedOutputReader> ExpectedOutputReaders { get; } =
            new Dictionary<string, ExpectedOutputReader>();

        public static string GetExpectedTestOutput()
        {
            var stackFrame = new StackFrame(1);
            var expectedTestOutput = GetExpectedOutputReaderFromAttribute(stackFrame);
            if (expectedTestOutput != null)
                return expectedTestOutput;

            var callingMethod = stackFrame.GetMethod();
            var reflectedType = callingMethod.ReflectedType;
            var expectedOutputsFileName = reflectedType.Name + "ExpectedOutputs.txt";
            return GetExpectedOutputReader(reflectedType, callingMethod.Name, expectedOutputsFileName);
        }

        private static string GetExpectedOutputReaderFromAttribute(StackFrame stackFrame)
        {
            var i = 3;
            do
            {
                var callingMethod = stackFrame.GetMethod();
                if (callingMethod.MethodImplementationFlags != MethodImplAttributes.IL)
                    return null;

                var expectedOutputAttribute = (ExpectedOutputAttribute)callingMethod.GetCustomAttributes(typeof(ExpectedOutputAttribute), false).LastOrDefault();
                if (expectedOutputAttribute != null)
                {
                    var reflectedType = callingMethod.ReflectedType;

                    var expectedOutputsFileName =
                        string.IsNullOrEmpty(expectedOutputAttribute.FileName) ?
                        reflectedType.Name + "ExpectedOutputs.txt" :
                        expectedOutputAttribute.FileName;

                    return GetExpectedOutputReader(callingMethod.ReflectedType, callingMethod.Name, expectedOutputsFileName);
                }
                
                stackFrame = new StackFrame(i++);
            } while (true);
        }

        private static string GetExpectedOutputReader(Type type, string methodName, string expectedOutputsFileName)
        {
            var assembly = Assembly.GetAssembly(type);
            var resourceName = type.Namespace + "." + expectedOutputsFileName;

            if (ExpectedOutputReaders.ContainsKey(resourceName))
                return ExpectedOutputReaders[resourceName].GetExpectedTestOutput(methodName);

            string expectedOutputs;
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                    throw new Exception($"Could not find expected output file \"{expectedOutputsFileName}\". You can use {nameof(ExpectedOutputAttribute)} to change the file name.");

                using (var reader = new StreamReader(stream))
                    expectedOutputs = reader.ReadToEnd();
            }

            var expectedOutputReader = new ExpectedOutputReader(expectedOutputsFileName, expectedOutputs);
            ExpectedOutputReaders[resourceName] = expectedOutputReader;

            return expectedOutputReader.GetExpectedTestOutput(methodName);
        }
    }
}
