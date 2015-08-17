using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace MonkeyCoder.Core.Tests.Math
{
    internal class ExpectedOutputReader
    {
        public string ExpectedOutputsFileName { get; }
        private string ExpectedOutputs { get; }

        public ExpectedOutputReader(string expectedOutputsFileName)
        {
            ExpectedOutputsFileName = expectedOutputsFileName;

            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "MonkeyCoder.Core.Tests.Math." + expectedOutputsFileName;

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                ExpectedOutputs = reader.ReadToEnd();
            }
        }

        public string GetExpectedTestOutput(string testMethodName)
        {
            var testMethodOutputStartIndex = ExpectedOutputs.IndexOf(testMethodName);
            if (testMethodOutputStartIndex == -1)
                throw new ArgumentException($"Couldn't find test method named \"{testMethodName}\" in {ExpectedOutputsFileName}", nameof(testMethodName));

            var outputStartIndex = testMethodOutputStartIndex + testMethodName.Length + Environment.NewLine.Length;
            var outputEndIndex = ExpectedOutputs.IndexOf('$', outputStartIndex);
            if (outputEndIndex == -1)
                throw new ArgumentException($"Failed to find '$' which indicates end of expected output in test method named \"{testMethodName}\"", nameof(testMethodName));

            var outputLength = outputEndIndex - outputStartIndex;
            var output = ExpectedOutputs.Substring(outputStartIndex, outputLength);

            return output;
        }
    }
}
