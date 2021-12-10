using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class GenericEventListener
{
    #region SINGLE EVENT FOR ONE UNITY EVENT

    // [SerializeField]
    // private GenericEvent _event;
    //
    // [SerializeField]
    // private UnityEvent<GenericEvent> _onEventRaised;
    //
    // public void OnEventRaised(GenericEvent evt)
    // {
    //     _onEventRaised.Invoke(evt);
    // }
    //
    // public void Enable()
    // {
    //     _event.RegisterListener(this);
    // }
    //
    // public void Disable()
    // {
    //     _event.UnregisterListener(this);
    // }
    
    #endregion

    #region MULTIPLE EVENTS FOR ONE UNITY EVENT

    [SerializeField]
    private List<GenericEvent> _events;
    
    [SerializeField]
    private UnityEvent<GenericEvent> _onEventRaised;
    
    public void OnEventRaised(GenericEvent evt)
    {
        _onEventRaised.Invoke(evt);
    }
    
    public void Enable()
    {
        foreach (var evt in _events)
        {
            evt.RegisterListener(this);
        }
    }
    
    public void Disable()
    {
        foreach (var evt in _events)
        {
            evt.UnregisterListener(this);
        }
    }
    
    #endregion
}
