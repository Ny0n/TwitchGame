using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class GameEventListener : MonoBehaviour
{
    [SerializeField]
    private UnityEvent _unityEvent;

    [SerializeField]
    private GameEvent _gameEvent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("listener : " + _gameEvent._listenerList[0]);
        }
    }

    private void OnEnable()
    {
        _gameEvent.RegisterListener(GetComponent<GameEventListener>());
    }

    private void OnDisable()
    {
        _gameEvent.UnregisterListener(GetComponent<GameEventListener>());
    }

    public void OnEventRaised()
    {
        _unityEvent.Invoke();
    }
}
