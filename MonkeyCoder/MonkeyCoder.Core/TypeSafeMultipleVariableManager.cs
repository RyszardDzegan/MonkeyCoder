using MonkeyCoder.Core.Math;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MonkeyCoder.Core
{
    internal class TypeSafeMultipleVariableManager<TVariables>
    {
        private class VariableValuesZip
        {
            public PropertyInfo Variable { get; set; }
            public IEnumerable PossibleValues { get; set; }
        }

        private IList<VariableValuesZip> VariablesValues { get; }

        public TypeSafeMultipleVariableManager(object possibleValues)
        {
            if (possibleValues == null)
                throw new ArgumentNullException(nameof(possibleValues), "Possible values cannot be null.");
            
            var variablesType = typeof(TVariables);
            var variableProperties = variablesType.GetProperties().OrderBy(x => x.Name).ToList();

            if (variableProperties.Count() == 0)
                throw new Exception($"Expected at least one property in {variablesType.Name}.");

            var variablePropertiesWithoutSetter =
                from x in variableProperties
                where x.SetMethod == null
                select x.Name;

            if (variablePropertiesWithoutSetter.Any())
                throw new Exception($"Variable properties must have setters. The following properties don't have setters: {string.Join(", ", variablePropertiesWithoutSetter)}.");

            var possibleValuesProperties = possibleValues
                .GetType()
                .GetProperties()
                .Where(x => variableProperties.Any(y => y.Name == x.Name))
                .OrderBy(x => x.Name)
                .ToList();

            if (variableProperties.Count() != possibleValuesProperties.Count())
            {
                var missingPossibleValues =
                    from x in variableProperties
                    where !possibleValuesProperties.Any(y => y.Name == x.Name)
                    select x;

                throw new Exception($"All variable properties must have same named possible values properties. The following variable properties don't have possible values: {string.Join(", ", missingPossibleValues)}.");
            }

            var variableValues =
                from x in variableProperties.Zip(possibleValuesProperties, (x, y) => new { Variable = x, Values = y })
                let variableType = x.Variable.PropertyType
                let valuesType = x.Values.PropertyType
                let variableEnumerableType = typeof(IEnumerable<>).MakeGenericType(variableType)
                select new
                {
                    Variable = x.Variable,
                    PossibleValues = x.Values.GetValue(possibleValues),
                    IsSingleValue = variableType.IsAssignableFrom(valuesType),
                    IsCollectionOfValues = variableEnumerableType.IsAssignableFrom(valuesType)
                };

            var invalidVariableValues =
                from x in variableValues
                where !(x.IsSingleValue || x.IsCollectionOfValues)
                select x.Variable.Name;

            if (invalidVariableValues.Any())
                throw new Exception($"Variable must be assignable from a single value or collection of values. The following possible values don't have valid typ: {string.Join(", ", invalidVariableValues)}.");

            VariablesValues = variableValues.Select(x => new VariableValuesZip
            {
                Variable = x.Variable,
                PossibleValues = (IEnumerable)(x.IsCollectionOfValues ? x.PossibleValues : new object[] {x.PossibleValues})
            }).ToList();
        }

        public IEnumerator<TVariables> GetEnumerator()
        {
            if (!VariablesValues.Any())
            {
                var variableBox = Activator.CreateInstance<TVariables>();
                yield return variableBox;
                yield break;
            }

            var variationsInput = VariablesValues.Select(x => x.PossibleValues.Cast<object>().ToList());
            var variations = new Variations<object>(variationsInput);
            var results =
                from variation in variations
                let variableBox = Activator.CreateInstance<TVariables>()
                select new
                {
                    VariableBox = variableBox,
                    Properties = from variable in VariablesValues.Select((x, i) => new { Variable = x.Variable, Index = i })
                                 select new { Variable = variable.Variable, Value = variation[variable.Index] }
                };

            foreach (var result in results)
            {
                foreach (var property in result.Properties)
                {
                    property.Variable.SetValue(result.VariableBox, property.Value);
                }

                yield return result.VariableBox;
            }
        }
    }
}
