using UnityEngine;
using UnityEngine.SceneManagement;

public class BootstrapLoader : MonoBehaviour
{
    [SerializeField]
    private ScriptableStringVariable _bootstrapSceneName;

    private void Awake()
    {
        bool isLoaded = false;

        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).name == _bootstrapSceneName.Value)
            {
                isLoaded = true;
            }
        } 

        if (!isLoaded)
        {
            SceneManager.LoadScene(_bootstrapSceneName.Value);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
