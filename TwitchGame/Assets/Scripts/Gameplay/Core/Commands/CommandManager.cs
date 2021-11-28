using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CommandExecutor))]
public class CommandManager : MySingleton<CommandManager>
{
    public Dictionary<string, Command> StoredCommands { get; private set; }

    private CommandExecutor commandExecutor;

    protected override void Awake()
    {
        base.Awake();
        StoredCommands = new Dictionary<string, Command>();
        commandExecutor = GetComponent<CommandExecutor>();
    }

    public void ProcessCommand(Command command)
    {
        //print("ProcessCommand " + command.ToString());
        switch (command.type)
        {
            case Enums.CommandType.REGISTER:
            case Enums.CommandType.UNREGISTER:
                commandExecutor.Execute(command);
                break;

            default:
                if (PlayersManager.Instance.IsPlayerRegistered(command.playerName))
                    StoreCommand(command);
                break;
        }
    }

    private void StoreCommand(Command command)
    {
        StoredCommands[command.playerName] = command;
    }

    public void ExecuteAllCommands()
    {
        foreach (string playerName in PlayersManager.Instance.GetNamesList())
        {
            commandExecutor.Execute(StoredCommands[playerName]);
        }
    }
    
    public void ResetAllCommands()
    {
        foreach (string playerName in PlayersManager.Instance.GetNamesList())
        {
            StoredCommands[playerName] = new Command(playerName, Enums.CommandType.STAY); // MOUAIS pas fan des new
        }
    }

    public void OnRoundStart() // event
    {
        ResetAllCommands();
    }

    public void OnTimerEnd() // event
    {
        ExecuteAllCommands();
    }

    public void OnGameEnd() // event
    {
        StoredCommands.Clear();
    }
}
