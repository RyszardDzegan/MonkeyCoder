using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MonkeyCoder.Variables
{
    public class VariableManagerFactory
    {
        /// <summary>
        /// Creates a new, empty variable manager of type <typeparamref name="T"/>.
        /// It means that an empty enumerable of <typeparamref name="T"/> will be produced and returned.
        /// <see cref="https://github.com/RyszardDzegan/MonkeyCoder/wiki"/> for further details.
        /// </summary>
        /// <typeparam name="T">A type of variable.</typeparam>
        /// <returns>An empty enumerable of variables.</returns>
        public static IEnumerable<T> Create<T>()
        {
            return Enumerable.Empty<T>();
        }

        /// <summary>
        /// Creates a variable manager that hosts one variable.
        /// <see cref="https://github.com/RyszardDzegan/MonkeyCoder/wiki"/> for further details.
        /// </summary>
        /// <typeparam name="T">A type containing one property that can be set.
        /// This property represents the variable that will be assigned
        /// with different values comming from <paramref name="possibleValues"/>
        /// during variable manager's iterations.</typeparam>
        /// <param name="possibleValues">Values that will be assigned to single property
        /// of type <typeparamref name="T"/> during variable manager's iterations.
        /// Each value must have a compatible type to property to which it will be assigned.</param>
        /// <returns>An enumerable of type <typeparamref name="T"/>.
        /// Each enumeration uses one of a value from <paramref name="possibleValues"/>
        /// to fill in the sole property of type <typeparamref name="T"/>.
        /// You can fetch that value within an iteration block from the property of <typeparamref name="T"/>.</returns>
        public static IEnumerable<T> Create<T>(params object[] possibleValues)
        {
            return new SingleVariableManager<T>(possibleValues);
        }

        /// <summary>
        /// Creates a variable manager that hosts one variable.
        /// <see cref="https://github.com/RyszardDzegan/MonkeyCoder/wiki"/> for further details.
        /// </summary>
        /// <typeparam name="T">A type containing one property that can be set.
        /// This property represents the variable that will be assigned
        /// with different values comming from <paramref name="possibleValues"/>
        /// during variable manager's iterations.</typeparam>
        /// <param name="possibleValues">Values that will be assigned to single property
        /// of type <typeparamref name="T"/> during variable manager's iterations.
        /// Each value must have a compatible type to property to which it will be assigned.</param>
        /// <returns>An enumerable of type <typeparamref name="T"/>.
        /// Each enumeration uses one of a value from <paramref name="possibleValues"/>
        /// to fill in the sole property of type <typeparamref name="T"/>.
        /// You can fetch that value within an iteration block from the property of <typeparamref name="T"/>.</returns>
        public static IEnumerable<T> Create<T>(IEnumerable<object> possibleValues)
        {
            return new SingleVariableManager<T>(possibleValues.ToArray());
        }

        /// <summary>
        /// Creates a variable manager that hosts one or more variables.
        /// <see cref="https://github.com/RyszardDzegan/MonkeyCoder/wiki"/> for further details.
        /// </summary>
        /// <typeparam name="T">A type containing one or more properties that can be set.
        /// These properties represent variables that will be assigned
        /// with different values comming from <paramref name="possibleValues"/>
        /// during variable manager's iterations.</typeparam>
        /// <param name="possibleValues">Direct values or a container containing variables-values pairs as properties
        /// that will be assigned to respecting properties of type <typeparamref name="T"/> during variable manager's iterations.
        /// Each value must have a compatible type to property to which it will be assigned.</param>
        /// <returns>An enumerable of type <typeparamref name="T"/>.
        /// Each enumeration uses one of a value or one set of values from <paramref name="possibleValues"/>
        /// to fill in respective properties of type <typeparamref name="T"/>.
        /// You can fetch that values within an iteration block from properties of <typeparamref name="T"/>.</returns>
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
