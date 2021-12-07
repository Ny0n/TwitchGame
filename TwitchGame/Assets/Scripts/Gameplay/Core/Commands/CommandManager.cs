using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MySingleton<CommandManager>
{
    public ScriptablePlayersList playersList;
    public Command defaultCommand;

    private List<CommandObject> _commandsToProcess;
    private Dictionary<string, CommandObject> _storedCommands;

    protected override void Awake()
    {
        base.Awake();
        _storedCommands = new Dictionary<string, CommandObject>();
        _commandsToProcess = new List<CommandObject>();
    }

    // *********************** PROCESS *********************** //

    private void Update()
    {
        if (_commandsToProcess.Count <= 0) return;
        foreach (CommandObject commandObject in _commandsToProcess.ToList())
        {
            _commandsToProcess.Remove(commandObject);
            ProcessCommand(commandObject);
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
            case Enums.GameState.WaitingForPlayers:
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
        _storedCommands[commandObject.PlayerName] = commandObject;
    }

    private void ExecuteAllCommands()
    {
        foreach (var entry in _storedCommands.ToList())
            ExecuteCommand(entry.Value);
    }

    private void ResetAllCommands()
    {
        foreach (var entry in _storedCommands.ToList())
            StoreCommand(new CommandObject(entry.Key, defaultCommand));
    }

    // *********************** EVENTS *********************** //

    public void OnRoundStart() // SO event
    {
        ResetAllCommands();
    }

    private IEnumerator ActionStart(GenericEvent evt) 
    {
        ExecuteAllCommands();
        yield return new WaitForSeconds(4);
        evt.Answer();
    }

    public void OnActionStart(GenericEvent evt)
    {
        StartCoroutine(ActionStart(evt));
    }

    public void OnGameEnd(GenericEvent evt) // SO event
    {
        _storedCommands.Clear();
        evt.Answer();
    }
}
