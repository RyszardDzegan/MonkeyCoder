using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MonkeyCoder.Core
{
    internal class TypeSafeSingleVariableManager<T>
    {
        public PropertyInfo PropertyInfo { get; }
        public dynamic[] PossibleValues { get; }

        public TypeSafeSingleVariableManager(params dynamic[] possibleValues)
        {
            var type = typeof(T);
            var properties = type.GetProperties();

            if (properties.Count() != 1)
                throw new Exception($"Expected one property in {type.Name} but found {properties.Count()}.");

            PropertyInfo = properties.Single();

            if (PropertyInfo.SetMethod == null)
                throw new Exception($"Property {PropertyInfo.Name} must have setter.");

            if (possibleValues == null)
                throw new ArgumentNullException(nameof(possibleValues), "Possible values cannot be null.");

            var invalidValues = from x in possibleValues
                                where !PropertyInfo.PropertyType.IsAssignableFrom(x.GetType())
                                select x;

            if (invalidValues.Any())
                throw new ArgumentException($"Cannot assign the following values to {PropertyInfo.Name} due to type mismatch: {string.Join(", ", invalidValues)}.", nameof(possibleValues));

            PossibleValues = possibleValues;
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (!PossibleValues.Any())
                yield break;
            
            foreach (var value in PossibleValues)
            {
                var variableBox = Activator.CreateInstance<T>();
                PropertyInfo.SetValue(variableBox, value);
                yield return variableBox;
            }
        }
    }
}
