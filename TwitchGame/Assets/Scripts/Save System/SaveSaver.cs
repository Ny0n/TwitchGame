using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(SaveSystem))]
public class SaveSaver : MonoBehaviour
{
    [SerializeField] private ScriptableGameStateVariable _gameState;
    [SerializeField] private ScriptablePlayersList _playersList;
    [SerializeField] private ScriptablePlatformsList _platformsList;
    
    [Header("Events")]
    [SerializeField] private ScriptableGameEvent _saveGameEvent;
    [SerializeField] private ScriptableGameEvent _saveGameEndEvent;
    
    private SaveSystem _saveSystem;
    
    private void Start() => _saveSystem = GetComponent<SaveSystem>();

    public async void Save()
    {
        // start anim
        Debug.Log("Save start");

        if (!_gameState.CompareState(Enums.GameState.Playing))
        {
            Debug.Log("Can only save in playing mode!");
            return;
        }
        
        _saveGameEvent.Raise();
        
        SaveData data = await Task.Run(GetDataThread);
        bool success = await _saveSystem.SaveData(data);
        
        // stop anim
        Debug.Log("Save end");
        
        if (success)
        {
            // show success
            Debug.Log("Saved successfully!");
        }
        else
        {
            // show failure
            Debug.Log("Failed to save...");
        }
        
        _saveGameEndEvent.Raise();
    }

    private SaveData GetDataThread()
    {
        // We save the players and the platforms
        
        List<PlayerSaveData> playerSaveDatas = new List<PlayerSaveData>();
        List<PlatformSaveData> platformSaveDatas = new List<PlatformSaveData>();

        foreach (var player in _playersList.GetPlayersList())
        {
            playerSaveDatas.Add(new PlayerSaveData()
            {
                name = player.Name,
                number = player.Number,
                isAlive = player.IsAlive,
                skinIndex = player.skinData.index,
                position = player.Position,
            });
        }
        
        foreach (var platform in _platformsList.GetValuesList())
        {
            platformSaveDatas.Add(new PlatformSaveData()
            {
                state = platform.CurrentState,
                position = platform.CurrentPosition,
            });
        }
        
        SaveData data = new SaveData()
        {
            players = playerSaveDatas,
            platforms = platformSaveDatas,
        };

        return data;
    }
}
