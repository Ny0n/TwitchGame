using UnityEngine;

[CreateAssetMenu(menuName = "Command/MoveDown", fileName = "MoveDown")]
public class MoveDownCommand : BaseMoveCommand
{
    public override void Execute(string playerName, ScriptablePlayersList playersList)
    {
        base.Execute(playerName, playersList);
        if (!IsValid) return;
        
        _playerMove.GoDown();
    }
}