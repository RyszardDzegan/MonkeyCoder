using System.Collections.Generic;
using System.Dynamic;

namespace MonkeyCoder.Core
{
    public class VariableBag : DynamicObject
    {
        public IDictionary<string, ValuePlaceholder> DecomposedVariables { get; }

        public VariableBag(IDictionary<string, ValuePlaceholder> decomposedVariables)
        {
            DecomposedVariables = decomposedVariables;
        }
        
        /// <summary>Tries to return value for given variable name.</summary>
        /// <param name="binder">Contains name that is provided to dynamic type.</param>
        /// <param name="result">Value of the variable.</param>
        /// <returns>True if variable with provided name exists. False otherwise.</returns>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            ValuePlaceholder valuePlaceholder;
            var ret = DecomposedVariables.TryGetValue(binder.Name, out valuePlaceholder);
            result = valuePlaceholder.Value;
            return ret;
        }
    }
}
