using UnityEngine;

[CreateAssetMenu(menuName = "Event/Player")]
public class ScriptablePlayerEvent : GenericEvent
{
    [field: SerializeField] public string PlayerName { get; private set; }
    [field: SerializeField] public Enums.PlayerEventAction Action { get; private set; }

    public void SetAndRaise(string playerName, Enums.PlayerEventAction action)
    {
        PlayerName = playerName;
        Action = action;
        Raise();
    }
}
