using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class  GenericEvent : ScriptableObject
{
    [SerializeField]
    private List<GenericEventListener> _listeners;

    private int _answered;
    public void Answer() => _answered++;
    public bool HasEveryoneAnswered => _answered >= _listeners.Count;

    public void Raise()
    {
        _answered = 0;
        for (int i = _listeners.Count - 1; i >= 0; i--)
        {
            _listeners[i]?.OnEventRaised(this);
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
    
    public IEnumerator RaiseAndWait()
    {
        this.Raise();
        while (!this.HasEveryoneAnswered)
            yield return null;
    }
    
    public IEnumerator WaitForSelfAnswers()
    {
        while (!this.HasEveryoneAnswered)
            yield return null;
    }

    public IEnumerator WaitForEventAnswers(GenericEvent waitingEvent, GenericEvent concernedEvent)
    {
        while (!concernedEvent.HasEveryoneAnswered)
            yield return null;
        waitingEvent.Answer();
    }
    
    public IEnumerator WaitForEventsAnswers(GenericEvent waitingEvent, List<GenericEvent> concernedEvents)
    {
        while (concernedEvents.Any(evt => !evt.HasEveryoneAnswered))
            yield return null;
        waitingEvent.Answer();
    }
}
