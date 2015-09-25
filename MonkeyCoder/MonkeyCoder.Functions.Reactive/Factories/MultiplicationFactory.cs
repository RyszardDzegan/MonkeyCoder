using System.Collections.Generic;

namespace MonkeyCoder.Functions.Reactive
{
    internal class MultiplicationFactory : BinaryNumberFactory<Multiplication>
    {
        protected override IEnumerable<dynamic> ProhibitedValues => new dynamic[] { 0 };
    }
}
