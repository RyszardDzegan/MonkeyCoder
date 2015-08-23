using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using TestHelpers;

namespace MonkeyCoder.Math.Tests
{
    public abstract class MathTestsBase
    {
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
