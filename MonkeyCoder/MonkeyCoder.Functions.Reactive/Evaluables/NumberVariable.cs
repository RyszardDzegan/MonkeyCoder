namespace MonkeyCoder.Functions.Reactive
{
    internal class NumberVariable : Variable, INumberVariable
    {
        public NumberVariable(string name, object value)
            : base(name, value)
        { }
    }
}
