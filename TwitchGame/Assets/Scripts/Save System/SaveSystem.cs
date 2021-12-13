using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(SaveSaver), typeof(SaveLoader))]
public class SaveSystem : MonoBehaviour
{
    [Header("Save config")]
    [SerializeField] private string _saveFolderName;
    [SerializeField] private string _saveFileName;
    [SerializeField] private string _saveFileExtension;
    
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

    public async Task<bool> SaveData(SaveData data)
    {
        try
        {
            await WriteDataAsync(data);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Error while trying to save: {e}");
            return false;
        }
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

    public async Task<SaveData?> LoadData()
    {
        try
        {
            return await ReadDataAsync();
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Error while trying to load save file: {e}");
            return null;
        }
    }
    
    private async Task<SaveData> ReadDataAsync()
    {
        TestForDir();
        
        #region Read (Simple)
        
        // string text = File.ReadAllText(_filePath);
        // return JsonUtility.FromJson<SaveData>(text);
        
        #endregion
        
        #region Read (Async)
        
        // read bytes from file
        using FileStream fileStream = new FileStream(_filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        byte[] bytes = new byte[fileStream.Length];
        await fileStream.ReadAsync(bytes, 0, (int)fileStream.Length);
        
        // get data from bytes
        return await Task.Run(() =>
        {
            string text = _encoding.GetString(bytes);
            return JsonUtility.FromJson<SaveData>(text);
        });

        #endregion
    }

    #endregion
}
