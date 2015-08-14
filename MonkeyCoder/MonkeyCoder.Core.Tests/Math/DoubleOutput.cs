using System.IO;
using System.Text;

namespace MonkeyCoder.Core.Tests.Math
{
    class DoubleOutput : TextWriter
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
}
