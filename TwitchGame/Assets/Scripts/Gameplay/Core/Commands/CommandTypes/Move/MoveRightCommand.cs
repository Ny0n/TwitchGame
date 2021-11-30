using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Command/MoveRight", fileName = "MoveRight")]
public class MoveRightCommand : BaseMoveCommand
{
    public override void Execute(string playerName, ScriptablePlayersList playersList)
    {
        base.Execute(playerName, playersList);
        if (!IsValid) return;

        playerMove.GoRight();
    }
}
