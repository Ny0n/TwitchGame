using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Event", menuName = "Game Event")]
public class GameEvent : ScriptableObject
{
    public List<GameEventListener> _listenerList = new List<GameEventListener>();

    public void RegisterListener(GameEventListener _eventListener)
    {
        _listenerList.Add(_eventListener);
    }

    public void UnregisterListener(GameEventListener _eventListener)
    {
        _listenerList.Remove(_eventListener);
    }

    public void Raise()
    {
        foreach (GameEventListener listener in _listenerList)
        {
            listener.OnEventRaised();
        }
    }
}
