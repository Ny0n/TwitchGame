using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinDatabase : MonoBehaviour
{
    [SerializeField]
    private GameObject defaultSkin;

    [SerializeField]
    private List<GameObject> skins = new List<GameObject>();

    public GameObject GetRandomSkin()
    {
        if (skins.Count == 0) return defaultSkin;
        return skins[Random.Range(0, skins.Count-1)];
    }
}
