using System.Collections.Generic;

namespace MonkeyCoder.Core
{
    public interface IVariableManager<out T>
    {
        IEnumerator<T> GetEnumerator();
    }
}
