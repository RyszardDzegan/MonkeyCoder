namespace MonkeyCoder.Core
{
    public class ValuePlaceholder
    {
        public dynamic Value { get; set; }

        public ValuePlaceholder() {}

        public ValuePlaceholder(dynamic v)
        {
            Value = v;
        }
    }
}
