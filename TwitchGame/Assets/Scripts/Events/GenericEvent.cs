using System.Collections.Generic;
using UnityEngine;

public abstract class GenericEvent : ScriptableObject
{
    [SerializeField]
    private List<GenericEventListener> _listeners;

    public void Raise()
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
        {
            _listeners[i]?.OnEventRaised();
        }
    }

    public void RegisterListener(GenericEventListener listener)
    {
        _listeners.Add(listener);
    }

    public void UnregisterListener(GenericEventListener listener)
    {
        _listeners.Remove(listener);
    }

}
