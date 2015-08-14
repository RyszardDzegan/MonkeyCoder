using System.Collections.Generic;

namespace MonkeyCoder.Core
{
    internal class EmptyVariableManager
    {
        public IEnumerator<dynamic> GetEnumerator()
        {
            yield return new VariableBag(new Dictionary<string, ValuePlaceholder>());
        }
    }
}
