using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using TestHelpers;

namespace MonkeyCoder.Functions.Tests
{
    public abstract class TestsBase
    {
        private StringWriter ActualTestOutput { get; } = new StringWriter();
        protected string GetActualTestOutput() => ActualTestOutput.ToString();

        protected void GenerateOutput(IEnumerable<Func<object>> items)
        {
            foreach (var item in items)
            {
                var result = item();
                Console.WriteLine(result);
            }
        }

        protected void GenerateOutput<T>(IEnumerable<Func<object>> items, ref T result)
        {
            foreach (var item in items)
            {
                item();
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
