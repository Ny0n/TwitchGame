using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] protected List<ScriptableStringVariable> _scenesToLoad;
    
    [Header("Load events")]
    [SerializeField] private ScriptableGameEvent _finishedLoadingLevelEvent;
    [SerializeField] private ScriptableGameEvent _levelStartEvent;
    
    [SerializeField] private ScriptableFloatVariable _loadedScenes;

    private void Awake()
    {
        if (_scenesToLoad.Count <= 0)
        {
            Finished();
            return;
        }

        // normal mode
        // StartCoroutine(LoadScenesCoroutine());
        
        // async mode (loading icon doesn't freeze) (but maybe dangerous?)
        _loadedScenes.Value = 0;
        foreach (var scene in _scenesToLoad)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(scene.Value, LoadSceneMode.Additive);
            operation.completed += LoadedScene;
        }
    }

    private IEnumerator LoadScenesCoroutine()
    {
        _loadedScenes.Value = 0;
        foreach (var scene in _scenesToLoad)
        {
            SceneManager.LoadScene(scene.Value, LoadSceneMode.Additive);
            _loadedScenes.Value++;
        }
        yield return null;
        Finished();
    }

    private void LoadedScene(AsyncOperation asyncOperation)
    {
        _loadedScenes.Value++;
        if (_loadedScenes.Value >= _scenesToLoad.Count)
            Finished();
    }

    private void Finished()
    {
        _finishedLoadingLevelEvent.Raise();
        if (_levelStartEvent != null)
            _levelStartEvent.Raise();
    }
}
