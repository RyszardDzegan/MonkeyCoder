using System;

namespace TestHelpers
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class ExpectedOutputAttribute : Attribute
    {
        public string FileName { get; }

        public ExpectedOutputAttribute()
        { }

        public ExpectedOutputAttribute(string fileName)
        {
            FileName = fileName;
        }
    }
}
