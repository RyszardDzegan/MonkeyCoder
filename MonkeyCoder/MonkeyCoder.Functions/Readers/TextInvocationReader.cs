using MonkeyCoder.Functions.Helpers.Arguments;
using MonkeyCoder.Functions.Helpers.Invocations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MonkeyCoder.Functions.Readers
{
    /// <summary>
    /// Formats informations regarding the information tree into a simple line of text.
    /// </summary>
    public class TextInvocationReader : IInvocationReader
    {
        private StringWriter TextWriter { get; set; }

        private Queue<string> StandardFunctionNames { get; } =
            new Queue<string>(new[] { "foo", "bar", "alpha", "beta", "delta", "gamma" });

        private Dictionary<object, string> KnownFunctions { get; } =
            new Dictionary<object, string>();

        private int FunctionNameIndex = -1;

        public void Visit(IInvocation invocation)
        {
            TextWriter = new StringWriter();
            InternalVisit(invocation);
        }

        private void InternalVisit(IInvocation invocation)
        {
            if (invocation is DelegateInvocation)
                Visit((DelegateInvocation)invocation);
            else if (invocation is ValueInvocation)
                Visit((ValueInvocation)invocation);
        }

        private void Visit(DelegateInvocation invocation)
        {
            TextWriter.Write(FormatValue(invocation.Delegate) + "(");

            var lastArgument = invocation.Arguments.LastOrDefault();
            foreach (var argument in invocation.Arguments)
            {
                if (argument is FunctionEvaluable)
                    InternalVisit(((FunctionEvaluable)argument).Invocation);
                else if (argument is ParameterlessArgument)
                    TextWriter.Write(FormatValue(((ParameterlessArgument)argument).Value) + "()");
                else
                    TextWriter.Write(FormatValue(((IEvaluable)argument).Evaluate()));

                if (argument != lastArgument)
                    TextWriter.Write(",");
            }

            TextWriter.Write(")");
        }

        private void Visit(ValueInvocation invocation)
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
