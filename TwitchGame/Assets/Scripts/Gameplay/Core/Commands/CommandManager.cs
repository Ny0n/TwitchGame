using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MySingleton<CommandManager>
{
    public ScriptablePlayersList playersList;

    public Dictionary<string, CommandObject> StoredCommands { get; private set; }

    private List<CommandObject> _commandsToProcess;

    protected override void Awake()
    {
        base.Awake();
        StoredCommands = new Dictionary<string, CommandObject>();
        _commandsToProcess = new List<CommandObject>();
    }

    // *********************** PROCESS *********************** //

    private void Update()
    {
        if (_commandsToProcess.Count > 0)
        {
            foreach (CommandObject commandObject in _commandsToProcess.ToList())
            {
                ProcessCommand(commandObject);
                _commandsToProcess.Remove(commandObject);
            }
        }
    }

    public void AddCommand(CommandObject commandObject)
    {
        _commandsToProcess.Add(commandObject);
    }

    private void ProcessCommand(CommandObject commandObject)
    {
        switch (GameManager.Instance.CurrentState)
        {
            case Enums.GameState.WAITINGFORPLAYERS:
                ExecuteCommand(commandObject);
                break;

            default:
                StoreCommand(commandObject);
                break;
        }
    }

    // *********************** EXECUTION *********************** //

    private void ExecuteCommand(CommandObject commandObject)
    {
        commandObject.Execute(playersList);
    }

    private void StoreCommand(CommandObject commandObject)
    {
        StoredCommands[commandObject.PlayerName] = commandObject;
    }

    public void ExecuteAllCommands()
    {
        foreach (var entry in StoredCommands.ToList())
            ExecuteCommand(entry.Value);
    }
    
    public void ResetAllCommands()
    {
        foreach (var entry in StoredCommands.ToList())
            StoreCommand(new CommandObject(entry.Key, new EmptyCommand()));
    }

    // *********************** EVENTS *********************** //

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
