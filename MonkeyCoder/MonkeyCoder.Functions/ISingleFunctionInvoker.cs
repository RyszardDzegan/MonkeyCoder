using System;
using System.Collections.Generic;

namespace MonkeyCoder.Functions
{
    internal interface ISingleFunctionInvoker : IEnumerable<Func<object>>
    {
        Delegate Function { get; }
        IReadOnlyCollection<object> PossibleArguments { get; }
    }
}
