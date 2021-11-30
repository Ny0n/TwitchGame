using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command : ScriptableObject
{
    [SerializeField] private string _text; // change in Unity inspector
    //[SerializeField] protected bool _isActive = true; // TODO

    public string Text => _text;

    public abstract void Execute(string playerName, ScriptablePlayersList playersList);

    public override string ToString() => $"{{ text = \"{Text}\" }}";
}
