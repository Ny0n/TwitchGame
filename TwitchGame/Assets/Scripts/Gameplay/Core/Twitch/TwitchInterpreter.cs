using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TwitchInterpreter : MonoBehaviour
{
    public List<Command> commands;

    public void Interpret(string playerName, string message)
    {
        if (!message.StartsWith("!")) return;
        if (string.IsNullOrEmpty(playerName)) return;

        foreach (var command in commands.Where(command => message == command.Text))
        {
            CommandManager.Instance.AddCommand(new CommandObject(playerName, command));
            return;
        }
    }
}
