using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelBootstrap : Bootstrap
{
    [SerializeField]
    private ScriptableStringVariable _thisLevel;

    [SerializeField]
    private ScriptableStringVariable _nextLevelName;

    [SerializeField]
    private ScriptableGameEvent _levelStartEvent;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        _levelStartEvent?.Raise();
    }
    
    [ContextMenu("Next Level")]
    public void NextLevel()
    {
        SceneManager.LoadScene(_nextLevelName.Value);
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
