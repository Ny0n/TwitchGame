using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Variable/Game State")]
public class ScriptableGameStateVariable : ScriptableObject
{
    [SerializeField] private Enums.GameState _inspectorValue = Enums.GameState.Null;
    
    private Enums.GameState _value = Enums.GameState.Null;
    
    [Header("State change events")]
    [SerializeField] private ScriptableGameEvent _gamePausedEvent;
    [SerializeField] private ScriptableGameEvent _gameWaitingEvent;
    [SerializeField] private ScriptableGameEvent _gameStartingEvent;
    [SerializeField] private ScriptableGameEvent _gamePlayingEvent;
    [SerializeField] private ScriptableGameEvent _gameEndingEvent;
    
    public Enums.GameState Value
    {
        get => _value;
        set => SwitchToState(value);
    }
    
    public ScriptableGameEvent CurrentStateEvent { get; private set; }

    public bool CompareState(Enums.GameState state) => Value == state;

    public void SwitchToState(Enums.GameState state)
    {
        _value = state;
        
        switch (Value)
        {
            case Enums.GameState.Paused:
                CurrentStateEvent = _gamePausedEvent;
                break;
            case Enums.GameState.WaitingForPlayers:
                CurrentStateEvent = _gameWaitingEvent;
                break;
            case Enums.GameState.Starting:
                CurrentStateEvent = _gameStartingEvent;
                break;
            case Enums.GameState.Playing:
                CurrentStateEvent = _gamePlayingEvent;
                break;
            case Enums.GameState.Ending:
                CurrentStateEvent = _gameEndingEvent;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    
        if (CurrentStateEvent is ScriptableGameEvent)
            CurrentStateEvent.Raise();
    }

    
    [ContextMenu("Switch to Selected")]
    private void ManualSwitch()
    {
        SwitchToState(_inspectorValue);
    }
}
