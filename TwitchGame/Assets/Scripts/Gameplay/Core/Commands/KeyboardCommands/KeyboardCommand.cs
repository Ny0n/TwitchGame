using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Command/Keyboard/New", fileName = "Keyboard _Command_")]
public class KeyboardCommand : ScriptableObject
{
    [SerializeField] private GameCommand _gameCommand;

    public GameCommand GetGameCommand()
    {
        if (_gameCommand == null) throw new Exception("[Game Command] not set in a [Keyboard Command] Scriptable Object");
        return _gameCommand;
    }
}
