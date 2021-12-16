using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(SaveSystem))]
public class SaveLoader : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private ScriptableGameStateVariable _gameState;
    [SerializeField] private ScriptableSkinDatabase _skinDatabase;
    [SerializeField] private ScriptablePlayersList _playersList;
    [SerializeField] private ScriptablePlatformsList _platformsList;

    [Header("Events")]
    [SerializeField] private ScriptableGameEvent _loadSaveStartEvent;
    [SerializeField] private ScriptableGameEvent _loadSaveStartPrepEvent;
    [SerializeField] private ScriptableGameEvent _loadSaveEndSuccessEvent;
    [SerializeField] private ScriptableGameEvent _loadSaveEndFailureEvent;
    
    [Header("Other")]
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _platformPrefab;
    [SerializeField] private float _delay = 0.04f;
    
    private SaveSystem _saveSystem;

    private int _startedCoroutines, _finishedCoroutines;

    private void Start() => _saveSystem = GetComponent<SaveSystem>();

    public async void Load()
    {
        // start anim
        _loadSaveStartEvent.Raise();
        
        SaveData? data = await _saveSystem.LoadData(); // already protected
        
        if (data != null)
        {
            _loadSaveStartPrepEvent.Raise(); // for scripts that need to do things before loading a new save

            try
            {
                await ApplyData((SaveData) data);
            }
            catch
            {
                Debug.Log("Loader: Error while trying to load data (we advise restarting a new game)");
                _loadSaveEndFailureEvent.Raise();
                return;
            }
            
            // show success
            _loadSaveEndSuccessEvent.Raise();
        }
        else
        {
            // show failure
            _loadSaveEndFailureEvent.Raise();
        }
    }

    private async Task ApplyData(SaveData data)
    {
        _gameState.SwitchToState(Enums.GameState.Paused); // disable commands
        
        #region Coroutines

        _startedCoroutines = 0;
        _finishedCoroutines = 0;
        
        StartCo(LoadPlatforms(data));
        StartCo(LoadPlayers(data));

        await Task.Run(WaitForCoroutinesThread);

        foreach (var player in _playersList.Players)
        {
            player.Value.PlayerObject.GetComponent<Rigidbody>().useGravity = true; // we add the gravity back
        }

        #endregion
        
        _gameState.SwitchToState(Enums.GameState.Playing);
    }

    private void StartCo(IEnumerator coroutine)
    {
        _startedCoroutines++;
        StartCoroutine(coroutine);
    }
    
    private void EndCo()
    {
        _finishedCoroutines++;
    }

    private void WaitForCoroutinesThread()
    {
        while (_startedCoroutines != _finishedCoroutines)
        {
            Thread.Sleep(100);
        }
    }
    
    private IEnumerator LoadPlatforms(SaveData data)
    {
        PlatformGenerator platformGenerator = FindObjectOfType<PlatformGenerator>();
        
        foreach (PlatformSaveData platformSaveData in data.platforms)
        {
            Vector2 pos = platformSaveData.position;
            GameObject platform = Instantiate(_platformPrefab,
                platformGenerator.originPoint.position + (platformGenerator.dist * pos.x * Vector3.right) + (platformGenerator.dist * pos.y * Vector3.forward),
                Quaternion.identity, transform);
            
            yield return null; // we let the gameobject spawn
            
            _platformsList.Platforms[pos] = platform.GetComponent<PlatformData>();
            _platformsList.Platforms[pos].Init(pos);
            _platformsList.Platforms[pos].SetState(platformSaveData.state);
            
            yield return new WaitForSeconds(_delay);
        }

        EndCo();
    }
    
    private IEnumerator LoadPlayers(SaveData data)
    {
        // spawn saved players
        foreach (PlayerSaveData playerSaveData in data.players)
        {
            Player player = new Player(playerSaveData.name, playerSaveData.number);
            player.SetSkin(_skinDatabase.GetSkinAt(playerSaveData.skinIndex));
            player.Instantiate(_playerPrefab, playerSaveData.position, Quaternion.identity);
            
            _playersList.Players.Add(playerSaveData.name, player);
            
            yield return null; // we let the gameobject spawn
            
            player.PlayerObject.GetComponent<Rigidbody>().useGravity = false;
            
            yield return new WaitForSeconds(_delay);
        }

        EndCo();
    }
}
