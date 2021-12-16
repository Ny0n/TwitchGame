using System;
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
    [SerializeField] private ScriptableGameEvent _saveGameStartEvent;
    [SerializeField] private ScriptableGameEvent _saveGameEndSuccessEvent;
    [SerializeField] private ScriptableGameEvent _saveGameEndFailureEvent;
    
    private SaveSystem _saveSystem;
    
    private void Start() => _saveSystem = GetComponent<SaveSystem>();

    public async void Save()
    {
        // start anim
        _saveGameStartEvent.Raise();
        
        if (!_gameState.CompareState(Enums.GameState.Playing))
        {
            Debug.Log("Can only save in playing mode!");
            _saveGameEndFailureEvent.Raise();
            return;
        }

        SaveData data;

        try
        {
            data = await Task.Run(GetDataThread);
        }
        catch (Exception e)
        {
            Debug.Log("Saver: Error while trying to get data");
            _saveGameEndFailureEvent.Raise();
            return;
        }
        
        bool success = await _saveSystem.SaveData(data); // already protected
        
        if (success)
        {
            // show success
            _saveGameEndSuccessEvent.Raise();
        }
        else
        {
            // show failure
            _saveGameEndFailureEvent.Raise();
        }
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
