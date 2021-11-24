using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersManager : MySingleton<PlayersManager>
{
    public Dictionary<string, Player> Players { get; private set; }

    [SerializeField]
    private GameObject playerPrefab;

    private int currentPlayerNumber = 1;

    protected override void Awake()
    {
        base.Awake();
        Players = new Dictionary<string, Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsPlayerRegistered(string name)
    {
        return Players.ContainsKey(name);
    }

    private GameObject CreateNewPlayerObject(string name)
    {
        // creation of player scene character
        GameObject player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        player.name = name;
        // add random skin, link Player object...

        return player;
    }

    public void OnRecievePlayerEvent(ScriptablePlayerEvent playerEvent) // event
    {
        switch (playerEvent.Action)
        {
            case Enums.PlayerEventAction.DEAD:
                UnregisterPlayer(playerEvent.PlayerName); // change later
                break;

            default:
                if (GameManager.Instance.CurrentState != Enums.GameState.WAITINGFORPLAYERS)
                    return;

                switch (playerEvent.Action)
                {
                    case Enums.PlayerEventAction.ADD: // create new player
                        RegisterNewPlayer(playerEvent.PlayerName);
                        break;

                    case Enums.PlayerEventAction.REMOVE: // remove existing player
                        UnregisterPlayer(playerEvent.PlayerName);
                        break;
                }
                break;
            
        }
    }

    private void RegisterNewPlayer(string playerName)
    {
        if (IsPlayerRegistered(playerName))
            return;

        Player player = new Player(CreateNewPlayerObject(playerName), playerName, currentPlayerNumber);
        Players.Add(playerName, player);
        currentPlayerNumber++;
    }
    
    private void UnregisterPlayer(string playerName)
    {
        if (!IsPlayerRegistered(playerName))
            return;

        Players[playerName].Remove();
        Players.Remove(playerName);
    }

    private void UnregisterAllPlayers()
    {
        foreach (string playerName in Players.Keys)
        {
            UnregisterPlayer(playerName);
        }
    }

    public void OnGameEnd() // event
    {
        UnregisterAllPlayers();
    }
}
