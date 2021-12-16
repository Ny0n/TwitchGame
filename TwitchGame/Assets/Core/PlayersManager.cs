using UnityEngine;

public class PlayersManager : MySingleton<PlayersManager>
{
    [SerializeField] private ScriptableSettings _settings;
    [SerializeField] private ScriptableGameStateVariable _gameState;
    [SerializeField] private ScriptableSkinDatabase _skinDatabase;
    [SerializeField] private ScriptablePlayersList _playersList;
    [SerializeField] private ScriptablePlayerEvent _playerSpawnEvent;

    private int _currentPlayerNumber = 1;
    private MyDictionary<string, Player> _players;

    protected override void Awake()
    {
        base.Awake();
        _players = _playersList.Players;
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
                if (!_gameState.CompareState(Enums.GameState.WaitingForPlayers))
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
        if (_playersList.Players.Count >= _settings.MaxNumberOfPlayers) // settings
        {
            Debug.Log("Maximum number of players reached!");
            return;
        }
        
        if (_playersList.IsPlayerRegistered(playerName))
            return;

        // creation of new player object
        Player player = new Player(playerName, _currentPlayerNumber);
        player.SetSkin(_skinDatabase.GetRandomSkin());

        _players.Add(playerName, player);
        _currentPlayerNumber++;
        
        _playerSpawnEvent.SetAndRaise(playerName, Enums.PlayerEventAction.Spawn);
    }
    
    private void UnregisterPlayer(string playerName)
    {
        if (!_playersList.IsPlayerRegistered(playerName))
            return;

        _players[playerName].Remove();
        _players.Remove(playerName);
    }

    public void UnregisterAllPlayers()
    {
        foreach (string playerName in _playersList.GetNamesList())
        {
            UnregisterPlayer(playerName);
        }
    }

    // *********************** EVENTS *********************** //
    
    public void OnGameEnd(GenericEvent evt) // event
    {
        foreach (var player in _playersList.GetPlayersList())
        {
            player.Remove();
        }
        evt.Answer();
    }
    
    public void OnGameWaiting(GenericEvent evt) // SO event
    {
        UnregisterAllPlayers();
        evt.Answer();
    }
    
    public void OnLoadSave() // SO event
    {
        UnregisterAllPlayers();
    }
}
