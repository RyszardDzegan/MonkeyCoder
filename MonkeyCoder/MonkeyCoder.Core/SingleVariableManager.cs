using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MonkeyCoder.Core
{
    internal class SingleVariableManager<T> : IEnumerable<T>
    {
        public Action<T, object> VariableSetter { get; }
        public object[] PossibleValues { get; }

        public SingleVariableManager(params object[] possibleValues)
        {
            var variablesPlaceholderType = typeof(T);
            var variablesProperties = variablesPlaceholderType.GetProperties();

            if (variablesProperties.Count() != 1)
                throw new Exception($"Expected one property in {variablesPlaceholderType.Name} but found {variablesProperties.Count()}.");

            var variable = variablesProperties.Single();

            if (variable.SetMethod == null)
                throw new Exception($"Property {variable.Name} must have setter.");

            if (possibleValues == null)
                throw new ArgumentNullException(nameof(possibleValues), "Possible values cannot be null.");

            var invalidValues =
                from x in possibleValues
                where !variable.PropertyType.IsAssignableFrom(x.GetType())
                select x;

            if (invalidValues.Any())
                throw new ArgumentException($"Cannot assign the following values to {variable.Name} due to type mismatch: {string.Join(", ", invalidValues)}.", nameof(possibleValues));

            VariableSetter = (variableBox, value) => variable.SetValue(variableBox, value);
            PossibleValues = possibleValues;
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (!PossibleValues.Any())
                yield break;
            
            foreach (var value in PossibleValues)
            {
                var variableBox = Activator.CreateInstance<T>();
                VariableSetter(variableBox, value);
                yield return variableBox;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
