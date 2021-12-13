using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Variable/Game State")]
public class ScriptableGameStateVariable : ScriptableObject
{
    [SerializeField] private Enums.GameState _value = Enums.GameState.Null;
    
    [Header("State change events")]
    public ScriptableGameEvent gameStartEvent;
    public ScriptableGameEvent gameEndEvent;
    
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
                CurrentStateEvent = null;
                break;
            case Enums.GameState.WaitingForPlayers:
                CurrentStateEvent = null;
                break;
            case Enums.GameState.Starting:
                CurrentStateEvent = gameStartEvent;
                break;
            case Enums.GameState.Playing:
                CurrentStateEvent = null;
                break;
            case Enums.GameState.Ending:
                CurrentStateEvent = gameEndEvent;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    
        if (CurrentStateEvent is ScriptableGameEvent)
            CurrentStateEvent.Raise();
    }
}
