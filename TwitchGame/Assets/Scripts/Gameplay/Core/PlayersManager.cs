using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SkinDatabase))]
public class PlayersManager : MySingleton<PlayersManager>
{
    public ScriptablePlayersList PlayersList;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private PlatformGenerator platformGenerator;

    private int _currentPlayerNumber = 1;
    private SkinDatabase _skinDatabase;
    private MyDictionary<string, Player> _players;

    protected override void Awake()
    {
        base.Awake();
        _players = PlayersList.Dico;
        _skinDatabase = GetComponent<SkinDatabase>();
    }

    public void NotifyPlayerUpdated(Player player)
    {
        if (_players.ContainsKey(player.Name))
            _players.NotifyChange();
    }

    public void OnRecievePlayerEvent(ScriptablePlayerEvent playerEvent) // event
    {
        switch (playerEvent.Action)
        {
            case Enums.PlayerEventAction.DEAD:
                _players[playerEvent.PlayerName].Kill();
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
        if (PlayersList.IsPlayerRegistered(playerName))
            return;

        // creation of new player object
        Player player = new Player(playerName, _currentPlayerNumber);
        player.SetSkin(_skinDatabase.GetRandomSkin());
        player.Instantiate(playerPrefab, platformGenerator.GetPlayerSpawn()); // TODO here for now

        _players.Add(playerName, player);
        _currentPlayerNumber++;
    }
    
    private void UnregisterPlayer(string playerName)
    {
        if (!PlayersList.IsPlayerRegistered(playerName))
            return;

        _players[playerName].Remove();
        _players.Remove(playerName);
    }

    private void UnregisterAllPlayers()
    {
        foreach (string playerName in PlayersList.GetNamesList())
        {
            UnregisterPlayer(playerName);
        }
    }

    public void OnGameEnd() // event
    {
        UnregisterAllPlayers();
    }
}
