using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "Database/Skin")]
public class ScriptableSkinDatabase : ScriptableObject
{
    [SerializeField]
    private GameObject _defaultSkin;

    [SerializeField]
    private List<GameObject> _skins = new List<GameObject>();

    public (int index, GameObject skin) GetRandomSkin()
    {
        int index;
        GameObject skin;
        
        if (_skins.Count == 0)
        {
            index = -1;
            skin = _defaultSkin;
        }
        else
        {
            index = Random.Range(0, _skins.Count);
            skin = _skins[index];
        }

        return (index, skin);
    }
    
    public (int index, GameObject skin) GetSkinAt(int index)
    {
        GameObject skin = index < 0 ? _defaultSkin : _skins[index];
        return (index, skin);
    }
}
