using UnityEngine;

[CreateAssetMenu(menuName = "Command/Empty", fileName = "Empty")]
public class EmptyCommand : Command
{
    public override void Execute(string playerName, ScriptablePlayersList playersList)
    {
        // Do nothing
    }
}
