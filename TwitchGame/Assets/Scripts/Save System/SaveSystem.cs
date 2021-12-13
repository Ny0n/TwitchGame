using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class SaveSystem : MonoBehaviour
{
    [SerializeField] private string _saveFolderName;
    [SerializeField] private string _saveFileName;
    [SerializeField] private string _saveFileExtension;
    
    [SerializeField] private ScriptablePlayersList _playersList;
    [SerializeField] private ScriptablePlatformsList _platformsList;
    
    private readonly Encoding _encoding = Encoding.UTF8;

    private string _directoryPath;
    private string _filePath;

    private void Start()
    {
        _directoryPath = Path.Combine(Application.persistentDataPath, _saveFolderName);
        _filePath = Path.Combine(_directoryPath, _saveFileName + "." + _saveFileExtension);
    }

    private void TestForDir()
    {
        if (!Directory.Exists(_directoryPath))
            Directory.CreateDirectory(_directoryPath);
    }

    #region Saving

    public void SaveData()
    {
        StartCoroutine(GetDataCoroutine(async data =>
        {
            try
            {
                await WriteDataAsync(data);
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Error while trying to save: {e}");
            }
            
            Debug.Log("Saved!");
        }));
    }

    private IEnumerator GetDataCoroutine(System.Action<SaveData> callback)
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
            });
            yield return null;
        }
        
        foreach (var platform in _platformsList.GetValuesList())
        {
            platformSaveDatas.Add(new PlatformSaveData()
            {
                state = platform.CurrentState,
                position = platform.CurrentPosition,
            });
            yield return null;
        }
        
        SaveData data = new SaveData()
        {
            players = playerSaveDatas,
            platforms = platformSaveDatas,
        };
        
        callback.Invoke(data);
    }

    private async Task WriteDataAsync(SaveData data)
    {
        TestForDir();
        
        #region Write (simple)

        // string jsonData = JsonUtility.ToJson(data);
        // File.WriteAllText(_filePath, jsonData);

        #endregion
        
        #region Write (Async)
        
        // get bytes to write
        byte[] bytes = await Task.Run(() =>
        {
            string jsonData = JsonUtility.ToJson(data);
            return _encoding.GetBytes(jsonData);
        });

        // write in file
        using FileStream fileStream = new FileStream(_filePath, FileMode.Create, FileAccess.Write, FileShare.Write);
        await fileStream.WriteAsync(bytes, 0, bytes.Length);

        #endregion
    }

    #endregion

    #region Loading

    public async void LoadData()
    {
        try
        {
            SaveData data = await ReadDataAsync();
            // load game with save data
            
            Debug.Log("Save file loaded!");
        }
        catch (Exception e)
        {
            Debug.LogWarning("No save file to load");
        }
    }
    
    private async Task<SaveData> ReadDataAsync()
    {
        TestForDir();
        
        #region Read (Simple)
        
        // string text = File.ReadAllText(_filePath);
        // string jsonData = JsonUtility.ToJson(text);
        // return JsonUtility.FromJson<SaveData>(jsonData);
        
        #endregion
        
        #region Read (Async)
        
        var sb = new StringBuilder();
        byte[] buffer = new byte[0x1000];
        
        using FileStream fileStream = new FileStream(_filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        int numRead;
        while ((numRead = await fileStream.ReadAsync(buffer, 0, buffer.Length)) != 0)
        {
            string text = _encoding.GetString(buffer, 0, numRead);
            sb.Append(text);
        }
        
        return await Task.Run(() =>
        {
            string jsonData = JsonUtility.ToJson(sb.ToString());
            return JsonUtility.FromJson<SaveData>(jsonData);
        });

        #endregion
    }

    #endregion
}
