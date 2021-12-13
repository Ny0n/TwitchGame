using UnityEngine;

public class ObservableVariable<T> : ScriptableObject
{
    [SerializeField] private T _value;
    
    public T Value
    {
        get => _value;
        set => SetValue(value);
    }

    public System.Action ValueChanged;

    public void SetValue(T value)
    {
        T oldValue = _value;
        _value = value;
        
        if (!Equals(oldValue, _value))
            ValueChanged.Invoke();
    }
}