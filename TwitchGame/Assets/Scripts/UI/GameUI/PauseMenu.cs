using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private ScriptableBoolVariable _gamePaused;
    [SerializeField] private ScriptableStringVariable _mainMenuScene;
    [SerializeField] private ScriptableGameEvent _reloadLevel;
    
    [SerializeField] private GameObject _panel;
    [SerializeField] private GameObject _defaultSelectedItem;

    private EventSystem _myEventSystem;

    private void Start()
    {
        _myEventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>(); // to select menu items

        // we set the game to unpaused by default (if it was for some reason not already unpaused)
        if (_gamePaused.Value)
            Resume();
    }

    private void OnDestroy()
    {
        if (_gamePaused.Value) // we resume the game if the pause menu is destroyed in any way (loading a new scene for example...)
            Resume();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // press escape to pause/unpause
            TogglePause();
    }

    private void TogglePause()
    {
        // if (_gamePaused.Value)
        //     Resume();
        // else
        //     Pause();
        
        if (!_gamePaused.Value)
            Pause();
    }
    
    private void Pause()
    {
        _panel.SetActive(true);
        _myEventSystem.SetSelectedGameObject(_defaultSelectedItem);
        // Time.timeScale = 0f;
        _gamePaused.Value = true;
        // pause sounds?
    }

    public void Resume() // exact opposite of Pause()
    {
        _panel.SetActive(false);
        _myEventSystem.SetSelectedGameObject(null); // to deselect every menu item
        // Time.timeScale = 1f;
        _gamePaused.Value = false;
    }
    
    public void Restart()
    {
        _reloadLevel.Raise();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(_mainMenuScene.Value);
    }
}
