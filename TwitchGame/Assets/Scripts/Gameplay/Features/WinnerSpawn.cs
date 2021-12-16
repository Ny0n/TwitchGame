using System.Collections;
using System.Linq;
using UnityEngine;

public class WinnerSpawn : MonoBehaviour
{
    [SerializeField] private ScriptablePlayersList _playersList;
    [SerializeField] private ScriptableGameEvent _nextLevelEvent;
    
    [SerializeField] private GameObject _playerPrefab;
    
    [SerializeField] private bool _loadNextLevel = true;

    private IEnumerator Start()
    {
        Player winner = (from player in _playersList.Players where player.Value.IsAlive select player.Value).FirstOrDefault();

        if (winner is Player)
        {
            var transform1 = transform;
            winner.Instantiate(_playerPrefab, transform1.position, transform1.rotation);

            for (int i = 0; i < 10; i++) // we wait for the player's animator to be good
                yield return null;
            winner.Win();
        }

        StartCoroutine(ReturnCoroutine());
    }

    private IEnumerator ReturnCoroutine()
    {
        yield return new WaitForSeconds(5);
        _playersList.Players.Clear();
        
        if (_loadNextLevel)
            _nextLevelEvent.Raise();
    }
}
