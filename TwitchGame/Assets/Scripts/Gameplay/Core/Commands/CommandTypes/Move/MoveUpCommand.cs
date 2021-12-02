using UnityEngine;

[CreateAssetMenu(menuName = "Command/MoveUp", fileName = "MoveUp")]
public class MoveUpCommand : BaseMoveCommand
{
    public override void Execute(string playerName, ScriptablePlayersList playersList)
    {
        base.Execute(playerName, playersList);
        if (!IsValid) return;

        _playerMove.GoUp();
    }
}
