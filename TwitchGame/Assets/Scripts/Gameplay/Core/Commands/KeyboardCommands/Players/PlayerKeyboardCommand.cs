using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Command/Keyboard/Player", fileName = "_P_ Keyboard _Command_")]
public class PlayerKeyboardCommand : ScriptableObject
{
    [SerializeField] private List<KeyCode> _keyCodes;
    
    [SerializeField] private KeyboardCommand _keyboardCommand;

    public bool Find()
    {
        return _keyCodes.Any(Input.GetKeyDown);
    }

    public GameCommand GetGameCommand()
    {
        if (_keyboardCommand == null) throw new Exception("[Keyboard Command] not set in a [Player Keyboard Command] Scriptable Object");
        return _keyboardCommand.GetGameCommand();
    }
}
