using MonkeyCoder.Core.Math;
using System.Collections.Generic;

namespace MonkeyCoder.Core
{
    internal class VariableVariations : Variations<ValuePlaceholder>
    {
        public VariableVariations(IEnumerable<IEnumerable<ValuePlaceholder>> items)
            : base(items)
        { }
    }
}
