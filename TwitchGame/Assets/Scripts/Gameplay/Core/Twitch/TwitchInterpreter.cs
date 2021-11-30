using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwitchInterpreter : MonoBehaviour
{
    public List<Command> commands;

    public void Interpret(string playerName, string message)
    {
        if (playerName == null || playerName == "") return;

        foreach (Command command in commands)
        {
            if (message == command.Text)
            {
                CommandManager.Instance.AddCommand(new CommandObject(playerName, command));
                return;
            }
        }
    }
}
