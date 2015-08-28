using System;

namespace TestHelpers
{
    public class ExpectedOutputReader
    {
        private string ExpectedOutputsFileName { get; }
        private string ExpectedOutputs { get; }

        public ExpectedOutputReader(string expectedOutputsFileName, string expectedOutputs)
        {
            ExpectedOutputsFileName = expectedOutputsFileName;
            ExpectedOutputs = expectedOutputs;
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
