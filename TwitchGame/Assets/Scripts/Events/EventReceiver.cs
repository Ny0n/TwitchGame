using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventReceiver : MonoBehaviour
{
    [SerializeField] private List<GenericEventListener> _eventListeners;

    private void OnEnable()
    {
        foreach (GenericEventListener _eventListener in _eventListeners)
        {
            _eventListener.OnEnable();
        }
    }

    private void OnDisable()
    {
        foreach (GenericEventListener _eventListener in _eventListeners)
        {
            _eventListener.OnDisable();
        }
    }
}
