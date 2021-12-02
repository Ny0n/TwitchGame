using UnityEngine;

[CreateAssetMenu(menuName = "Command/MoveLeft", fileName = "MoveLeft")]
public class MoveLeftCommand : BaseMoveCommand
{
    public override void Execute(string playerName, ScriptablePlayersList playersList)
    {
        base.Execute(playerName, playersList);
        if (!IsValid) return;

        _playerMove.GoLeft();
    }
}
