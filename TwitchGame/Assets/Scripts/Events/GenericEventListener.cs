using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class GenericEventListener
{
    //////////////////////////////////////
    // SINGLE EVENT FOR ONE UNITY EVENT //
    //////////////////////////////////////

    [SerializeField]
    private GenericEvent _event;

    [SerializeField]
    private UnityEvent _onEventRaised;

    public void OnEventRaised()
    {
        _onEventRaised.Invoke();
    }

    public void OnEnable()
    {
        _event.RegisterListener(this);
    }

    public void OnDisable()
    {
        _event.UnregisterListener(this);
    }

    /////////////////////////////////////////
    // MULTIPLE EVENTS FOR ONE UNITY EVENT //
    /////////////////////////////////////////

    //[SerializeField]
    //private List<ScriptableEvent> _events;

    //[SerializeField]
    //private UnityEvent _onEventRaised;

    //public void OnEventRaised()
    //{
    //    _onEventRaised.Invoke();
    //}

    //public void OnEnable()
    //{
    //    foreach (ScriptableEvent _event in _events)
    //    {
    //        _event.RegisterListener(this);
    //    }
    //}

    //public void OnDisable()
    //{
    //    foreach (ScriptableEvent _event in _events)
    //    {
    //        _event.UnregisterListener(this);
    //    }
    //}
}
