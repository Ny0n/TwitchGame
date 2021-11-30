using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Command/Register", fileName = "Register")]
public class RegisterCommand : Command
{
    public ScriptablePlayerEvent playerEvent;

    public override void Execute(string playerName, ScriptablePlayersList playersList)
    {
        playerEvent.SetAndRaise(playerName, Enums.PlayerEventAction.ADD);
    }
}
