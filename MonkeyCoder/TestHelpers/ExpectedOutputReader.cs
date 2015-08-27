using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace TestHelpers
{
    public class ExpectedOutputReader
    {
        public string ExpectedOutputsFileName { get; }
        private string ExpectedOutputs { get; }

        public ExpectedOutputReader(string expectedOutputsFileName, Type type)
        {
            ExpectedOutputsFileName = expectedOutputsFileName;

            var assembly = Assembly.GetAssembly(type);
            var resourceName = type.Namespace + "." + ExpectedOutputsFileName;

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                try
                {
                    using (var reader = new StreamReader(stream))
                    {
                        ExpectedOutputs = reader.ReadToEnd();
                    }
                }
                catch (ArgumentNullException exception)
                {
                    throw new Exception($"Could not find expected output file \"{ExpectedOutputsFileName}\". You can use {nameof(ExpectedOutputAttribute)} to change the file name.", exception);
                }
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
