using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwitchInterpreter
{
    public void Interpret(string playerName, string command)
    {
        if (playerName == null || playerName == "") return;

        foreach (var entry in Enums.Commands)
        {
            if (command == entry.Value)
            {
                CommandManager.Instance.ProcessCommand(new Command(playerName, entry.Key));
                return;
            }
        }
    }
}
