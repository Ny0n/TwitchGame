using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MySingleton<CommandManager>
{
    public ScriptableGameStateVariable gameState;
    public ScriptablePlayersList playersList;
    public GameCommand defaultGameCommand;

    private List<GameCommandObject> _commandsToProcess;
    private Dictionary<string, GameCommandObject> _storedCommands;

    protected override void Awake()
    {
        base.Awake();
        _storedCommands = new Dictionary<string, GameCommandObject>();
        _commandsToProcess = new List<GameCommandObject>();
    }

    // *********************** PROCESS *********************** //

    private void Update()
    {
        if (_commandsToProcess.Count <= 0) return;
        foreach (GameCommandObject gameCommandObject in _commandsToProcess.ToList())
        {
            _commandsToProcess.Remove(gameCommandObject);
            ProcessCommand(gameCommandObject);
        }
    }

    public void AddCommand(GameCommandObject gameCommandObject)
    {
        _commandsToProcess.Add(gameCommandObject);
    }

    private void ProcessCommand(GameCommandObject gameCommandObject)
    {
        switch (gameState.Value)
        {
            case Enums.GameState.WaitingForPlayers:
                ExecuteCommand(gameCommandObject);
                break;

            default:
                StoreCommand(gameCommandObject);
                break;
        }
    }

    // *********************** EXECUTION *********************** //

    private void ExecuteCommand(GameCommandObject gameCommandObject)
    {
        gameCommandObject.Execute(playersList);
    }

    private void StoreCommand(GameCommandObject gameCommandObject)
    {
        _storedCommands[gameCommandObject.PlayerName] = gameCommandObject;
    }

    private void ExecuteAllCommands()
    {
        foreach (var entry in _storedCommands.ToList())
        {
            ExecuteCommand(entry.Value);
        }
    }

    private void ResetAllCommands()
    {
        foreach (var entry in _storedCommands.ToList())
            StoreCommand(new GameCommandObject(entry.Key, defaultGameCommand));
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

    public void OnActionStart(GenericEvent evt) // SO event
    {
        StartCoroutine(ActionStart(evt));
    }

    public void OnGameEnd(GenericEvent evt) // SO event
    {
        _storedCommands.Clear();
        evt.Answer();
    }
}
