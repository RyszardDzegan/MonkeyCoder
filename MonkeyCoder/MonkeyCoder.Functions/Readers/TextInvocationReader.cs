using MonkeyCoder.Functions.Invocations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MonkeyCoder.Functions.Readers
{
    /// <summary>
    /// Formats informations regarding the information tree into a simple line of text.
    /// </summary>
    public class TextInvocationReader : IInvocationVisitor
    {
        private StringWriter TextWriter { get; set; } = new StringWriter();

        private Queue<string> StandardFunctionNames { get; } =
            new Queue<string>(new[] { "foo", "bar", "alpha", "beta", "delta", "gamma" });

        private Dictionary<object, string> KnownFunctions { get; } =
            new Dictionary<object, string>();

        private int FunctionNameIndex = -1;

        public void Clear() =>
            TextWriter = new StringWriter();

        public void Visit(FunctionInvocation invocation)
        {
            TextWriter.Write(FormatValue(invocation.Delegate) + "(");

            var lastArgument = invocation.Arguments.LastOrDefault();
            foreach (var argument in invocation.Arguments)
            {
                argument.Accept(this);

                if (argument != lastArgument)
                    TextWriter.Write(",");
            }

            TextWriter.Write(")");
        }

        public void Visit(ValueInvocation invocation)
        {
            if (invocation.Value is string)
                TextWriter.Write("\"" + invocation.Value + "\"");
            else if (invocation.Value is char)
                TextWriter.Write("'" + invocation.Value + "'");
            else
                TextWriter.Write(invocation.Value);
        }

        private string FormatValue(object value)
        {
            if (value is string)
                return "\"" + value + "\"";

            if (value is char)
                return "'" + value + "'";

            if (value is Delegate)
            {
                if (!KnownFunctions.ContainsKey(value))
                    if (StandardFunctionNames.Any())
                        KnownFunctions.Add(value, StandardFunctionNames.Any() ? StandardFunctionNames.Dequeue() : "f" + ++FunctionNameIndex);
                return KnownFunctions[value];
            }

            return Convert.ToString(value);
        }

        public override string ToString() =>
            TextWriter?.GetStringBuilder().ToString();
    }
}
