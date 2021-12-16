using UnityEngine;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private AudioMixer _audioMixer;
    
    [SerializeField] private OptionsMenu _optionsMenu;
    [SerializeField] private ResolutionDropdown _resDropdown;
    
    [Header("PlayerPrefs keys")]
    [SerializeField] private ScriptableStringVariable _mainVolumeKey;
    [SerializeField] private ScriptableStringVariable _sfxVolumeKey;
    [SerializeField] private ScriptableStringVariable _musicVolumeKey;

    public void Start()
    {
        SetAllVolumes();
        _resDropdown.InitOptions();
        _optionsMenu.InitOptions();
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void SetAllVolumes()
    {
        if (PlayerPrefs.HasKey(_mainVolumeKey.Value))
        {
            _audioMixer.SetFloat(_mainVolumeKey.Value, PlayerPrefs.GetFloat(_mainVolumeKey.Value));
        }
        if (PlayerPrefs.HasKey(_sfxVolumeKey.Value))
        {
            _audioMixer.SetFloat(_sfxVolumeKey.Value, PlayerPrefs.GetFloat(_sfxVolumeKey.Value));
        }
        if (PlayerPrefs.HasKey(_musicVolumeKey.Value))
        {
            _audioMixer.SetFloat(_musicVolumeKey.Value, PlayerPrefs.GetFloat(_musicVolumeKey.Value));
        }
    }
}
