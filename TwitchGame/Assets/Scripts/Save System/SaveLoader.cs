using System;
using UnityEngine;

[RequireComponent(typeof(SaveSystem))]
public class SaveLoader : MonoBehaviour
{
    private SaveSystem _saveSystem;

    private void Start() => _saveSystem = GetComponent<SaveSystem>();

    public async void Load()
    {
        // start anim
        Debug.Log("Load start");
        
        SaveData? data = await _saveSystem.LoadData();
        
        // stop anim
        Debug.Log("Load end");
        
        if (data != null)
        {
            ApplyData(data);
            
            // show success
            Debug.Log("Loaded save file successfully!");
        }
        else
        {
            // show failure
            Debug.Log("Failed to load save file...");
        }
    }

    private void ApplyData(SaveData? data)
    {
        Debug.Log("ApplyData");
        // load game
    }
}
