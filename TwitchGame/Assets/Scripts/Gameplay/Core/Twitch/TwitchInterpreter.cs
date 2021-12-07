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

        var command = commands.FirstOrDefault(command => command.Text == message);
        if (command != null)
            CommandManager.Instance.AddCommand(new CommandObject(playerName, command));
    }
}
