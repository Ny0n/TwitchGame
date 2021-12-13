using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour
{
    [SerializeField]
    protected List<ScriptableStringVariable> _scenesToLoad;

    private void Awake()
    {
        foreach (var scene in _scenesToLoad)
        {
            SceneManager.LoadScene(scene.Value, LoadSceneMode.Additive);
        }
    }
}