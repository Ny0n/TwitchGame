using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SaveData
{
    public List<PlayerSaveData> players;
    public List<PlatformSaveData> platforms;
}

[System.Serializable]
public struct PlayerSaveData
{
    public string name;
    public int number;
    public bool isAlive;
    public int skinIndex;
    public Vector3 position;
}

[System.Serializable]
public struct PlatformSaveData
{
    public Vector2 position;
    public int state;
}
