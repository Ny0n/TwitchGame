using UnityEngine;

public abstract class Command : ScriptableObject
{
    [field: SerializeField] public string Text { get; private set; } = ""; // change in Unity inspector

    public abstract void Execute(string playerName, ScriptablePlayersList playersList);

    public override string ToString() => $"{{ command: <text = \"{Text}\">, <type = \"{this.GetType()}\"> }}";
}
