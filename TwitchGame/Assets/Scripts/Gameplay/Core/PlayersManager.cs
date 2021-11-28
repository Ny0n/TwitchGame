using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PlayersManager : MySingleton<PlayersManager>
{
    public Dictionary<string, Player> Players { get; private set; }

    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private PlatformGenerator platformGenerator;

    private int currentPlayerNumber = 1;

    protected override void Awake()
    {
        base.Awake();
        Players = new Dictionary<string, Player>();
    }

    public List<string> GetNamesList()
    {
        return Players.Keys.ToList();
    }

    public bool IsPlayerRegistered(string name)
    {
        return Players.ContainsKey(name);
    }

    private GameObject CreateNewPlayerGameObject(string name)
    {
        // creation of player scene character
        GameObject player = Instantiate(playerPrefab, platformGenerator.GetPlayerSpawn(), Quaternion.identity);
        player.name = name;
        // add random skin, link Player object...

        return player;
    }

    public void OnRecievePlayerEvent(ScriptablePlayerEvent playerEvent) // event
    {
        switch (playerEvent.Action)
        {
            case Enums.PlayerEventAction.DEAD:
                UnregisterPlayer(playerEvent.PlayerName); // TODO change later
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

        Player player = new Player(CreateNewPlayerGameObject(playerName), playerName, currentPlayerNumber);
        player.PlayerObject.GetComponent<PlayerData>().Player = player; // TODO pas ouf
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
        foreach (string playerName in GetNamesList())
        {
            UnregisterPlayer(playerName);
        }
    }

    public void OnGameEnd() // event
    {
        UnregisterAllPlayers();
    }
}