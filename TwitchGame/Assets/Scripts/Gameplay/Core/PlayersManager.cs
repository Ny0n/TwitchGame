using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SkinDatabase))]
public class PlayersManager : MySingleton<PlayersManager>
{
    public Dictionary<string, Player> Players { get; private set; }

    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private PlatformGenerator platformGenerator;

    private int currentPlayerNumber = 1;

    private SkinDatabase skinDatabase;

    protected override void Awake()
    {
        base.Awake();
        Players = new Dictionary<string, Player>();
        skinDatabase = GetComponent<SkinDatabase>();
    }

    public List<string> GetNamesList()
    {
        return Players.Keys.ToList();
    }

    public bool IsPlayerRegistered(string name)
    {
        return Players.ContainsKey(name);
    }

    public void OnRecievePlayerEvent(ScriptablePlayerEvent playerEvent) // event
    {
        switch (playerEvent.Action)
        {
            case Enums.PlayerEventAction.DEAD:
                Players[playerEvent.PlayerName].Kill();
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

        // creation of new player object
        Player player = new Player(playerName, currentPlayerNumber);
        player.SetSkin(skinDatabase.GetRandomSkin());
        player.Instantiate(playerPrefab, platformGenerator.GetPlayerSpawn()); // TODO here for now

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
