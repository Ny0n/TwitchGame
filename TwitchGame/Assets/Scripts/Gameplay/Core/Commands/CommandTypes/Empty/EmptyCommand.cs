using UnityEngine;

[CreateAssetMenu(menuName = "Command/Empty", fileName = "Empty Command")]
public class EmptyCommand : Command
{
    public override void Execute(string playerName, ScriptablePlayersList playersList)
    {
        // Do nothing
    }
}
