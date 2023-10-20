using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NurseCalling
{
    public class Wrapped<T>  {
    private T _value;

    public Action WillChange;
    public Action DidChange;

    public T Value
    {
        get => _value;

        set
        {
            if (!_value.Equals(value))
           {
                OnWillChange();
               _value = value;
                OnDidChange();
            }
        }
    }

    protected virtual void OnWillChange() => WillChange?.Invoke();
    protected virtual void OnDidChange() => DidChange?.Invoke();

        public static implicit operator Wrapped<T>(ushort v)
        {
            throw new NotImplementedException();
        }
    }
}
