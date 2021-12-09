using UnityEngine;

public abstract class GameCommand : ScriptableObject
{
    public abstract void Execute(string playerName, ScriptablePlayersList playersList);

    public override string ToString() => $"{{ command: <type = \"{this.GetType()}\"> }}";
}
