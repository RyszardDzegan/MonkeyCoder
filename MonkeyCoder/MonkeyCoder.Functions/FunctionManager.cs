using MonkeyCoder.Functions.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MonkeyCoder.Functions
{
    internal class FunctionManager : IEnumerable<Func<object>>
    {
        private Lazy<FunctionsEnumerable> Invocations { get; }

        public FunctionManager(IReadOnlyCollection<object> arguments, int stackSize = 0)
        {
            if (arguments == null)
                throw new ArgumentNullException(nameof(arguments), "Arguments cannot be null.");

            var functions =
                from argument in arguments
                let function = argument as Delegate
                let results =
                    function != null ?
                    new FunctionsEnumerable(new Expanding(function, arguments, stackSize)) :
                    new FunctionsEnumerable(argument)
                from result in results
                select result;

            Invocations = FunctionsEnumerable.Lazy(functions);
        }

        public IEnumerator<Func<object>> GetEnumerator() => Invocations.Value.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
