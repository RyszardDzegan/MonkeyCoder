using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MonkeyCoder.Variables
{
    public class VariableManagerFactory
    {
        public static IEnumerable<T> Create<T>()
        {
            return Enumerable.Empty<T>();
        }

        public static IEnumerable<T> Create<T>(params object[] possibleValues)
        {
            return new SingleVariableManager<T>(possibleValues);
        }

        public static IEnumerable<T> Create<T>(IEnumerable<object> possibleValues)
        {
            return new SingleVariableManager<T>(possibleValues.ToArray());
        }

        public static IEnumerable<T> Create<T>(object possibleValues)
        {
            if (typeof(T).GetProperties().Count() != 1)
                return new MultipleVariableManager<T>(possibleValues);

            if (typeof(T).GetProperties().First().PropertyType.IsAssignableFrom(possibleValues.GetType()))
                return new SingleVariableManager<T>(possibleValues);

            if (typeof(IEnumerable).IsAssignableFrom(possibleValues.GetType()))
                return new SingleVariableManager<T>(((IEnumerable)possibleValues).Cast<object>().ToArray());
            
            return new MultipleVariableManager<T>(possibleValues);
        }
    }
}
