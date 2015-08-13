namespace MonkeyCoder.Core
{
    internal class EvaluatedVariable
    {
        public VariablePlaceholder VariablePlaceholder { get; }
        public ValuePlaceholder ValuePlaceholder { get; }

        public EvaluatedVariable(VariablePlaceholder variablePlaceholder, ValuePlaceholder valuePlaceholder)
        {
            VariablePlaceholder = variablePlaceholder;
            ValuePlaceholder = valuePlaceholder;
        }
    }
}
