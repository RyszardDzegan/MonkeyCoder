using System;
using System.Collections.Generic;

namespace MonkeyCoder.Functions.Helpers
{
    internal class TypeFullNameEqualityComparer : IEqualityComparer<Type>
    {
        private static Lazy<TypeFullNameEqualityComparer> _instance = new Lazy<TypeFullNameEqualityComparer>(() => new TypeFullNameEqualityComparer());
        public static TypeFullNameEqualityComparer Instance => _instance.Value;
        public bool Equals(Type x, Type y) => x.FullName == y.FullName;
        public int GetHashCode(Type obj) => obj.FullName.GetHashCode();
    }
}
