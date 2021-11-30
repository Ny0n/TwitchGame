using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CommandObject
{
    public string PlayerName { get; }
    public Command Command { get; }

    public CommandObject(string playerName, Command command)
    {
        PlayerName = playerName;
        Command = command;
    }

    public void Execute(ScriptablePlayersList playersList)
    {
        if (Command is Command)
            Command.Execute(PlayerName, playersList);
    }

    public override string ToString() => $"{{ PlayerName = \"{PlayerName}\", Command = \"{Command}\" }}";
}
