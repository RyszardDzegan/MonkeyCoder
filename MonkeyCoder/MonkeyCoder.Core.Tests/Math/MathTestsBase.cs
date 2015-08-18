using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace MonkeyCoder.Core.Tests.Math
{
    public abstract class MathTestsBase
    {
        private class DoubleOutput : TextWriter
        {
            public TextWriter Output1 { get; }
            public TextWriter Output2 { get; }

            public DoubleOutput(TextWriter output1, TextWriter output2)
            {
                Output1 = output1;
                Output2 = output2;
            }

            public override Encoding Encoding
            {
                get { return Output1.Encoding; }
            }

            public override void Write(char value)
            {
                Output1.Write(value);
                Output2.Write(value);
            }
        }

        private class ExpectedOutputReader
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

        internal static class StaticExpectedOutputReader
        {
            private static string ExpectedOutputsFileName { get;  set;}
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
                    ExpectedOutputReader = new ExpectedOutputReader(ExpectedOutputsFileName);
                }

                return ExpectedOutputReader.GetExpectedTestOutput(testMethodName);
            }
        }

        private StringWriter ActualTestOutput { get; } = new StringWriter();
        protected string GetActualTestOutput() => ActualTestOutput.ToString();

        protected void GenerateOutput<T>(IEnumerable<IEnumerable<T>> items)
        {
            foreach (var item in items)
            {
                var result = string.Join("", item);
                Console.WriteLine(result);
            }
        }

        [TestInitialize]
        public void Setup()
        {
            var doubleOutput = new DoubleOutput(Console.Out, ActualTestOutput);
            Console.SetOut(doubleOutput);
        }
    }
}
