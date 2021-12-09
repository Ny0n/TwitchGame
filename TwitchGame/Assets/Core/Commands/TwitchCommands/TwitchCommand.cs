using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Command/Twitch", fileName = "Twitch _Command_")]
public class TwitchCommand : ScriptableObject
{
    [SerializeField] [Tooltip("Without prefix")]
    private List<string> _stringCommands;
    
    [SerializeField] private GameCommand _gameCommand;

    public bool Find(string s)
    {
        return _stringCommands.Exists(strCmd => s == strCmd);
    }

    public GameCommand GetGameCommand()
    {
        if (_gameCommand == null) throw new Exception("[Game Command] not set in a [Twitch Command] Scriptable Object");
        return _gameCommand;
    }
}
