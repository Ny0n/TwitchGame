using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMoveCommand : Command
{
    protected Player player;
    protected PlayerMove playerMove;

    protected bool IsValid { get; private set; } = false; // can sub-classes execute?

    public override void Execute(string playerName, ScriptablePlayersList playersList)
    {
        if (!GameManager.Instance.CompareState(Enums.GameState.PLAYING)) return;
        if (!playersList.IsPlayerRegistered(playerName)) return;

        player = playersList.Dico[playerName];
        if (!player.IsAlive) return;

        playerMove = player.PlayerObject.GetComponent<PlayerMove>();
        IsValid = true;
    }
}
