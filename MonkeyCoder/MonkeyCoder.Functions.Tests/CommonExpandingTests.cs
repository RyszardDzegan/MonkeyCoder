using System;
using System.Collections.Generic;
using TestHelpers;

using static NUnit.Framework.Assert;

namespace MonkeyCoder.Functions.Tests
{
    using static StaticExpectedOutputReader;
    
    public abstract class CommonExpandingTests : CommonTests
    {
        internal override IEnumerable<Func<object>> GetInvoker(Delegate function, params object[] possibleArguments) =>
            new Expanding(function, possibleArguments, StackSize);

        internal abstract int StackSize { get; }
        
        public virtual void Works_with_function_1_string()
        {
            var function = new Func<string>(() => "a");
            var sut = GetInvoker(function);
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }
        
        public virtual void Works_with_function_1_string_and_1_func_string_possible_argument()
        {
            var function = new Func<string>(() => "a");
            var a1 = new Func<string>(() => "b");
            var sut = GetInvoker(function, a1);
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }
        
        public virtual void Works_with_function_2_string_and_1_func_string_possible_argument()
        {
            var function = new Func<string, string>(x => x);
            var a1 = new Func<string>(() => "b");
            var sut = GetInvoker(function, a1);
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }
        
        public virtual void Works_with_function_2_string_and_1_func_2_string_2_string_possible_arguments()
        {
            var function = new Func<string, string>(x => "a" + x);
            var a1 = new Func<string, string>(x => "b" + x);
            var a2 = "c";
            var sut = GetInvoker(function, a1, a2);
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }
        
        public virtual void Works_with_function_2_string_and_func_2_string_and_string_and_string_possible_arguments()
        {
            var function = new Func<string, string>(x => "a" + x);
            var a1 = new Func<string, string>(x => "b" + x);
            var a2 = "c";
            var a3 = "d";
            var sut = GetInvoker(function, a1, a2, a3);
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }
        
        public virtual void Works_with_function_2_string_and_func_2_string_and_func_2_string_and_string_possible_arguments()
        {
            var function = new Func<string, string>(x => "a" + x);
            var a1 = new Func<string, string>(x => "b" + x);
            var a2 = new Func<string, string>(x => "c" + x);
            var a3 = "d";
            var sut = GetInvoker(function, a1, a2, a3);
            GenerateOutput(sut);
            AreEqual(GetExpectedTestOutput(), GetActualTestOutput());
        }
    }
}
