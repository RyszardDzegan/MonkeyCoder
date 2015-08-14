using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace MonkeyCoder.Core.Tests.Math
{
    class VariationsTestsExpectedOutputReader
    {
        private const string VariationsTestsExpectedOutputsFileName = "VariationsTestsExpectedOutputs.txt";
        private static string VariationsTestsExpectedOutputs { get; }

        static VariationsTestsExpectedOutputReader()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "MonkeyCoder.Core.Tests.Math." + VariationsTestsExpectedOutputsFileName;

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                VariationsTestsExpectedOutputs = reader.ReadToEnd();
            }
        }

        public static string GetExpectedTestOutput([CallerMemberName] string testMethodName = "")
        {
            var testMethodOutputStartIndex = VariationsTestsExpectedOutputs.IndexOf(testMethodName);
            if (testMethodOutputStartIndex == -1)
                throw new ArgumentException($"Couldn't find test method named \"{testMethodName}\" in {VariationsTestsExpectedOutputsFileName}", nameof(testMethodName));

            var outputStartIndex = testMethodOutputStartIndex + testMethodName.Length + Environment.NewLine.Length;
            var outputEndIndex = VariationsTestsExpectedOutputs.IndexOf('$', outputStartIndex);
            if (outputEndIndex == -1)
                throw new ArgumentException($"Failed to find '$' which indicates end of expected output in test method named \"{testMethodName}\"", nameof(testMethodName));

            var outputLength = outputEndIndex - outputStartIndex;
            var output = VariationsTestsExpectedOutputs.Substring(outputStartIndex, outputLength);

            return output;
        }
    }
}
