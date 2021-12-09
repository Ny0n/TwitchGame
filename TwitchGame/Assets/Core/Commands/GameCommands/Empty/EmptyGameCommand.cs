using UnityEngine;

[CreateAssetMenu(menuName = "Command/Game/Empty", fileName = "Game _EmptyCommand_")]
public class EmptyGameCommand : GameCommand
{
    public override void Execute(string playerName, ScriptablePlayersList playersList)
    {
        // Do nothing
    }
}
