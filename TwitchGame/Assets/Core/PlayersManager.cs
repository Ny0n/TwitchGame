using UnityEngine;

[RequireComponent(typeof(SkinDatabase))]
public class PlayersManager : MySingleton<PlayersManager>
{
    public ScriptableGameStateVariable gameState;
    public ScriptablePlayersList playersList;
    public ScriptablePlayerEvent playerEvent;

    private int _currentPlayerNumber = 1;
    private SkinDatabase _skinDatabase;
    private MyDictionary<string, Player> _players;

    protected override void Awake()
    {
        base.Awake();
        _players = playersList.Players;
        _skinDatabase = GetComponent<SkinDatabase>();
    }

    public void NotifyPlayerUpdated(Player player)
    {
        if (_players.ContainsKey(player.Name))
            _players.NotifyChange();
    }

    public void OnReceivePlayerEvent(GenericEvent evt) // event
    {
        ScriptablePlayerEvent playerEvent = (ScriptablePlayerEvent)evt;
        switch (playerEvent.Action)
        {
            case Enums.PlayerEventAction.Dead:
                _players[playerEvent.PlayerName].Kill();
                break;

            default:
                if (!gameState.CompareState(Enums.GameState.WaitingForPlayers))
                    return;

                switch (playerEvent.Action)
                {
                    case Enums.PlayerEventAction.Add: // create new player
                        RegisterNewPlayer(playerEvent.PlayerName);
                        break;

                    case Enums.PlayerEventAction.Remove: // remove existing player
                        UnregisterPlayer(playerEvent.PlayerName);
                        break;
                }
                break;
        }
    }

    private void RegisterNewPlayer(string playerName)
    {
        if (playersList.IsPlayerRegistered(playerName))
            return;

        // creation of new player object
        Player player = new Player(playerName, _currentPlayerNumber);
        player.SetSkin(_skinDatabase.GetRandomSkin());

        _players.Add(playerName, player);
        _currentPlayerNumber++;
        
        playerEvent.SetAndRaise(playerName, Enums.PlayerEventAction.Spawn);
    }
    
    private void UnregisterPlayer(string playerName)
    {
        if (!playersList.IsPlayerRegistered(playerName))
            return;

        _players[playerName].Remove();
        _players.Remove(playerName);
    }

    private void UnregisterAllPlayers()
    {
        foreach (string playerName in playersList.GetNamesList())
        {
            UnregisterPlayer(playerName);
        }
    }

    public void OnGameEnd(GenericEvent evt) // event
    {
        foreach (var player in playersList.GetPlayersList())
        {
            player.Remove();
        }
        evt.Answer();
    }
}
