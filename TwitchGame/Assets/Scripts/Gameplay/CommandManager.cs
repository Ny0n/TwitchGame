using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MySingleton<CommandManager>
{
    public override bool DoDestroyOnLoad => false;

    public ScriptablePlayerEvent playerEvent;

    public Dictionary<string, Enums.Command> Commands { get; private set; }

    private void Awake()
    {
        Commands = new Dictionary<string, Enums.Command>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ProcessCommand(string playerName, Enums.Command command)
    {
        switch (command)
        {
            case Enums.Command.REGISTER:
                playerEvent.name = playerName;
                playerEvent.action = ScriptablePlayerEvent.Action.ADD;
                playerEvent.Raise();
                break;
            
            case Enums.Command.UNREGISTER:
                playerEvent.name = playerName;
                playerEvent.action = ScriptablePlayerEvent.Action.REMOVE;
                playerEvent.Raise();
                break;

            default:
                if (PlayersManager.Instance.IsPlayerRegistered(playerName))
                {
                    Commands[playerName] = command;
                }
                break;
        }
    }

    public void OnRoundStart() // event
    {
        foreach (string playerName in PlayersManager.Instance.Players.Keys)
        {
            Commands[playerName] = Enums.Command.STAY;
        }
    }

    public void OnTimerEnd() // event
    {
        foreach (string playerName in PlayersManager.Instance.Players.Keys)
        {
            PlayersManager.Instance.Players[playerName].PlayerObject.GetComponent<PlayerMove>().RunCommand(Commands[playerName]);
        }
    }
}
