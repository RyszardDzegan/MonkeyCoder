using System;
using System.Collections.Generic;
using System.Linq;

namespace MonkeyCoder.Core
{
    internal class SingleVariableManager
    {
        public string VariableName { get; }
        public dynamic[] PossibleValues { get; }

        public SingleVariableManager(string variableName, params dynamic[] possibleValues)
        {
            if (string.IsNullOrWhiteSpace(variableName))
                throw new ArgumentException($"Invalid variable name: \"{variableName}\".", nameof(variableName));

            if (possibleValues == null)
                throw new ArgumentException($"Invalid variable name: \"{variableName}\".", nameof(variableName));

            VariableName = variableName;
            PossibleValues = possibleValues;
        }

        public IEnumerator<dynamic> GetEnumerator()
        {
            if (!PossibleValues.Any())
            {
                var valuePlaceholder = new ValuePlaceholder(null);
                var dictionary = new Dictionary<string, ValuePlaceholder> {[VariableName] = valuePlaceholder };
                var variableBag = new VariableBag(dictionary);
                yield return variableBag;
                yield break;
            }

            var valuePlaceholders = PossibleValues.Select(x => new ValuePlaceholder(x));

            var query = from x in valuePlaceholders
                        let dictionary = new Dictionary<string, ValuePlaceholder> {[VariableName] = x }
                        select new VariableBag(dictionary);

            foreach (var item in query)
                yield return item;
        }
    }
}
