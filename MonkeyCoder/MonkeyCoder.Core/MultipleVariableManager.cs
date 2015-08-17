using MonkeyCoder.Core.Math;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MonkeyCoder.Core
{
    internal class MultipleVariableManager<T> : IVariableManager<T>
    {
        private class VariableValuesPair
        {
            public Action<T, object> VariableSetter { get; set; }
            public IEnumerable PossibleValues { get; set; }
        }

        private IList<VariableValuesPair> VariableValuesPairs { get; }

        public MultipleVariableManager(object possibleValuesPlaceholder)
        {
            var variablesPlaceholderType = typeof(T);

            var variablesProperties = variablesPlaceholderType
                .GetProperties()
                .OrderBy(x => x.Name)
                .ToList();

            if (!variablesProperties.Any())
            {
                VariableValuesPairs = new VariableValuesPair[0];
                return;
            }

            var variablesPropertiesWithoutSetter =
                from x in variablesProperties
                where x.SetMethod == null
                select x.Name;

            if (variablesPropertiesWithoutSetter.Any())
                throw new Exception($"Variable properties must have setters. The following properties don't have setters: {string.Join(", ", variablesPropertiesWithoutSetter)}.");

            if (possibleValuesPlaceholder == null)
                throw new ArgumentNullException(nameof(possibleValuesPlaceholder), "Possible values cannot be null.");

            var possibleValuesProperties = possibleValuesPlaceholder
                .GetType()
                .GetProperties()
                .Where(x => variablesProperties.Any(y => y.Name == x.Name))
                .OrderBy(x => x.Name)
                .ToList();

            if (variablesProperties.Count() != possibleValuesProperties.Count())
            {
                var missingPossibleValues =
                    from x in variablesProperties
                    where !possibleValuesProperties.Any(y => y.Name == x.Name)
                    select x.Name;

                throw new Exception($"All variable properties must have same named possible values placeholder properties. The following possible values placeholders are missing: {string.Join(", ", missingPossibleValues)}.");
            }

            var variableValues =
                from x in variablesProperties.Zip(possibleValuesProperties, (x, y) => new { Variable = x, Values = y })
                let variableType = x.Variable.PropertyType
                let valuesType = x.Values.PropertyType
                let variableEnumerableType = typeof(IEnumerable<>).MakeGenericType(variableType)
                select new
                {
                    Variable = x.Variable,
                    PossibleValues = x.Values.GetValue(possibleValuesPlaceholder),
                    IsSingleValue = variableType.IsAssignableFrom(valuesType),
                    IsCollectionOfValues = variableEnumerableType.IsAssignableFrom(valuesType)
                };

            var invalidVariableValues =
                from x in variableValues
                where !(x.IsSingleValue || x.IsCollectionOfValues)
                select x.Variable.Name;

            if (invalidVariableValues.Any())
                throw new Exception($"Variable must be assignable from a single value or collection of values. The following possible values don't have valid typ: {string.Join(", ", invalidVariableValues)}.");

            VariableValuesPairs = variableValues.Select(x => new VariableValuesPair
            {
                VariableSetter = (variableBox, value) => x.Variable.SetValue(variableBox, value),
                PossibleValues = (IEnumerable)(x.IsCollectionOfValues ? x.PossibleValues : new object[] {x.PossibleValues})
            }).ToList();
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (!VariableValuesPairs.Any())
                yield break;

            var variationsInput = VariableValuesPairs.Select(x => x.PossibleValues.Cast<object>().ToList());
            var variations = new Variations<object>(variationsInput);

            var results =
                from variation in variations
                let variableBox = Activator.CreateInstance<T>()
                let variableSetters = from variable in VariableValuesPairs.Select((x, i) => new { VariableSetter = x.VariableSetter, Index = i })
                                      select new Action(() => variable.VariableSetter(variableBox, variation[variable.Index]))
                select new { VariableBox = variableBox, VariableSetters = variableSetters };

            foreach (var result in results)
            {
                foreach (var variableSetter in result.VariableSetters)
                {
                    variableSetter();
                }

                yield return result.VariableBox;
            }
        }
    }
}
