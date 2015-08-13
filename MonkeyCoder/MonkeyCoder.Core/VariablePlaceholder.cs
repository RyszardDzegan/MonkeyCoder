using System;

namespace MonkeyCoder.Core
{
    public class VariablePlaceholder
    {
        public static implicit operator VariablePlaceholder(string name) => new VariablePlaceholder(name);

        public string Name { get; }
        public Func<dynamic, ValuePlaceholder, bool> Predicate { get; }

        public VariablePlaceholder(string name)
        {
            Name = name;
            Predicate = (vb, x) => true;
        }

        public VariablePlaceholder(string name, Func<dynamic, bool> predicate)
        {
            Name = name;
            Predicate = new Func<dynamic, ValuePlaceholder, bool>((vb, v) => predicate(v.Value));
        }

        public VariablePlaceholder(string name, Func<dynamic, dynamic, bool> predicate)
        {
            Name = name;
            Predicate = new Func<dynamic, ValuePlaceholder, bool>((vb, v) => predicate(vb, v.Value));
        }
    }
}
