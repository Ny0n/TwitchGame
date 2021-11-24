using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MySingleton<CommandManager>
{
    public ScriptablePlayerEvent playerEvent;

    public Dictionary<string, Enums.Command> Commands { get; private set; }

    protected override void Awake()
    {
        base.Awake();
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
        print("ProcessCommand " + playerName + " " + command.ToString());
        switch (command)
        {
            case Enums.Command.REGISTER:
                playerEvent.SetAndRaise(playerName, Enums.PlayerEventAction.ADD);
                break;
            
            case Enums.Command.UNREGISTER:
                playerEvent.SetAndRaise(playerName, Enums.PlayerEventAction.REMOVE);
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
            print(playerName);
            print(Commands[playerName].ToString());
        }
    }

    public void OnGameEnd() // event
    {
        Commands.Clear();
    }
}
