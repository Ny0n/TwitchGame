using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelBootstrap : Bootstrap
{
    [SerializeField] private ScriptableGameEvent _loadLevelAsyncEvent;
    
    [Header("Level details")]
    [SerializeField] private ScriptableStringVariable _thisLevel;
    [SerializeField] private ScriptableStringVariable _nextLevelName;
    
    [ContextMenu("Reload current level async")]
    public void ReloadLevelAsync()
    {
        _loadLevelAsyncEvent.Raise();
        SceneManager.LoadSceneAsync(_thisLevel.Value);
    }
    
    [ContextMenu("Next Level")]
    public void NextLevel()
    {
        SceneManager.LoadScene(_nextLevelName.Value);
    }
    
    [ContextMenu("Next Level Async")]
    public void NextLevelAsync()
    {
        _loadLevelAsyncEvent.Raise();
        SceneManager.LoadSceneAsync(_nextLevelName.Value);
    }

    [ContextMenu("Next Level additive")]
    public void NextLevelAdditive()
    {
        SceneManager.UnloadSceneAsync(_thisLevel.Value);

        foreach (var scene in _scenesToLoad)
        {
            SceneManager.UnloadSceneAsync(scene.Value);
        }

        SceneManager.LoadScene(_nextLevelName.Value, LoadSceneMode.Additive);
    }
}
