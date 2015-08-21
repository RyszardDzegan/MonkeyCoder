using System;
using System.Collections.Generic;

namespace MonkeyCoder.Core
{
    internal interface ISingleFunctionInvoker : IEnumerable<Func<object>>
    {
        Delegate Function { get; }
        IReadOnlyCollection<object> PossibleArguments { get; }
    }
}
