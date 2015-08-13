using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MonkeyCoder.Core
{
    public class VariableManager : IEnumerator<IDictionary<string, ValuePlaceholder>>
    {
        public ValuePlaceholder[] ValuePlaceholders { get; }
        public VariablePlaceholder[] VariablePlaceholders { get; }
        public dynamic VariableBag { get; private set; }
        public bool IsValid => EvaluatedVariables.All(x => x.VariablePlaceholder.Predicate(VariableBag, x.ValuePlaceholder));

        private IEnumerator<IList<ValuePlaceholder>> VariableVariations { get; set; }
        private IEnumerable<EvaluatedVariable> EvaluatedVariables { get; set; }
        private IDictionary<string, ValuePlaceholder> Current { get; set; }
        IDictionary<string, ValuePlaceholder> IEnumerator<IDictionary<string, ValuePlaceholder>>.Current => Current;
        object IEnumerator.Current => Current;

        public VariableManager(ValuePlaceholder[] valuePlaceholders, params VariablePlaceholder[] variablePlaceholders)
        {
            ValuePlaceholders = valuePlaceholders;
            VariablePlaceholders = variablePlaceholders;

            Reset();
        }

        void IDisposable.Dispose()
        {
            throw new InvalidOperationException();
        }

        public bool MoveNext()
        {
            if (!VariableVariations.MoveNext())
            {
                VariableVariations = null;
                EvaluatedVariables = null;
                Current = null;
                VariableBag = null;

                return false;
            }

            var currentValues = VariableVariations.Current;
            EvaluatedVariables = VariablePlaceholders.Zip(currentValues, (x, y) => new EvaluatedVariable(x, y));

            // Predicates are running first time at this line.
            Current = EvaluatedVariables.ToDictionary(x => x.VariablePlaceholder.Name, x => x.ValuePlaceholder);
            VariableBag = new VariableBag(Current);

            return true;
        }

        public void Reset()
        {
            var data = from x in VariablePlaceholders
                       select ValuePlaceholders;

            var variations = new VariableVariations(data);
            VariableVariations = variations.GetEnumerator();
        }
    }
}