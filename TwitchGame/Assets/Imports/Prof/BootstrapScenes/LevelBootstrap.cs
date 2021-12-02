using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelBootstrap : Bootstrap
{
    [SerializeField]
    private string _thisLevel;

    [SerializeField]
    private string _nextLevelName;

    [SerializeField]
    private ScriptableGameEvent _levelStartEvent;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(4);
        _levelStartEvent.Raise();
    }

    [ContextMenu("Next Level")]
    public void NextLevel()
    {
        SceneManager.UnloadSceneAsync(_thisLevel);

        foreach (var scene in _scenesToLoad)
        {
            SceneManager.UnloadSceneAsync(scene);
        }

        LoadNextLevel();
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(_nextLevelName, LoadSceneMode.Additive);
    }
}
