using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TwitchInterpreter : MonoBehaviour
{
    [SerializeField] private string _commandPrefix;
    [SerializeField] private List<TwitchCommand> _twitchCommands;
    
    public void Interpret(string playerName, string message)
    {
        // player name
        if (string.IsNullOrEmpty(playerName)) return;
        
        // message prefix
        if (!message.StartsWith(_commandPrefix)) return; // here for optimization
        message = message.Remove(0, _commandPrefix.Length);
        
        // command
        var twitchCommand = _twitchCommands.FirstOrDefault(twitchCommand => twitchCommand.Find(message));
        if (twitchCommand != null)
            CommandManager.Instance.AddCommand(new GameCommandObject(playerName, twitchCommand.GetGameCommand()));
    }
}
