using UnityEngine;

[CreateAssetMenu(menuName = "Event/GameState")]
public class ScriptableGameStateEvent : GenericEvent
{
    [field: SerializeField] public Enums.GameState State { get; private set; }

    public void SetAndRaise(Enums.GameState state)
    {
        State = state;
        Raise();
    }
}
