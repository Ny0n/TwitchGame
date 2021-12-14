using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public ScriptablePlayersList playersList;
    
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private PlatformGenerator _platformGenerator;

    public void OnReceivePlayerEvent(GenericEvent evt) // event
    {
        ScriptablePlayerEvent playerEvent = (ScriptablePlayerEvent)evt;
        if (playerEvent.Action == Enums.PlayerEventAction.Spawn)
        {
            Player player = playersList.TryGetPlayer(playerEvent.PlayerName);
            player.Instantiate(_playerPrefab, _platformGenerator.GetPlayerSpawn(), Quaternion.identity);
        }
    }
}
