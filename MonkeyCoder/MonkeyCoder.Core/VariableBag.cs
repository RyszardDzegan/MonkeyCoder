using System.Collections.Generic;
using System.Dynamic;

namespace MonkeyCoder.Core
{
    /// <summary>
    /// Name taken from <see href="https://msdn.microsoft.com/en-us/library/system.web.mvc.controllerbase.viewbag(v=vs.118).aspx">ControlerBase.ViewBag</see> of ASP.NET MVC.
    /// Allows to access variables via dot notation as if they were properties of dynamic type.
    /// </summary>
    /// <remarks>
    /// Parameters and variables called <c>vb</c> are of type <see cref="VariableBag"/>.
    /// Using dictionaries wouldn't give us more type safety.
    /// </remarks>
    public class VariableBag : DynamicObject
    {
        /// <summary>
        /// Pairs of variable name and associated value.
        /// </summary>
        public IDictionary<string, ValuePlaceholder> VariableValuePairs { get; }

        /// <summary>
        /// Constructor that takes pairs of variable name and associated value.
        /// </summary>
        /// <param name="variableValuePairs">Pairs of variable name and associated value.</param>
        public VariableBag(IDictionary<string, ValuePlaceholder> variableValuePairs)
        {
            VariableValuePairs = variableValuePairs;
        }
        
        /// <summary>Tries to return value for given variable name.</summary>
        /// <param name="binder">Contains variable name that is being used as property of given dynamic type.</param>
        /// <param name="result">Value associated with the variable.</param>
        /// <returns>True if variable with provided name exists. False otherwise.</returns>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            ValuePlaceholder valuePlaceholder;
            var ret = VariableValuePairs.TryGetValue(binder.Name, out valuePlaceholder);
            result = valuePlaceholder.Value;
            return ret;
        }
    }
}
