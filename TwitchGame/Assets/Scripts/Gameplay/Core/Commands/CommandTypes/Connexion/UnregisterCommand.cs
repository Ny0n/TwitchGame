using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Command/Unregister", fileName = "Unregister")]
public class UnregisterCommand : Command
{
    public ScriptablePlayerEvent playerEvent;

    public override void Execute(string playerName, ScriptablePlayersList playersList)
    {
        playerEvent.SetAndRaise(playerName, Enums.PlayerEventAction.REMOVE);
    }
}
