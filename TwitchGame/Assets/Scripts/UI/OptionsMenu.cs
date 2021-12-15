using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;

    [Header("UI Elements")]
    [SerializeField] private Slider _sliderMainVolume;
    [SerializeField] private Slider _sliderSFXVolume;
    [SerializeField] private Slider _sliderMusicVolume;
    [SerializeField] private Toggle _toggleFullscreen;
    [SerializeField] private TMPro.TMP_Dropdown _resDropdown;
    
    [Header("PlayerPrefs keys")]
    [SerializeField] private ScriptableStringVariable _mainVolumeKey;
    [SerializeField] private ScriptableStringVariable _sfxVolumeKey;
    [SerializeField] private ScriptableStringVariable _musicVolumeKey;
    [SerializeField] private ScriptableStringVariable _fullscreenKey;
    [SerializeField] private ScriptableStringVariable _resolutionKey;

    public void OnEnable()
    {
        SetAllSlidersToPos();
        SetResAndFullscreen();
    }

    private void SetResAndFullscreen()
    {
        if (PlayerPrefs.HasKey(_fullscreenKey.Value))
        {
            bool fsState = intToBool(PlayerPrefs.GetInt(_fullscreenKey.Value));
            _toggleFullscreen.isOn = fsState;
        }

        if (PlayerPrefs.HasKey(_resolutionKey.Value))
        {
            _resDropdown.value = PlayerPrefs.GetInt(_resolutionKey.Value);
        }
    }

    #region bool & int functions
    
    //used to save bools in playerprefs (only used in this script)
    int boolToInt(bool val)
    {
        if (val)
            return 1;
        else
            return 0;
    }

    bool intToBool(int val)
    {
        if (val != 0)
            return true;
        else
            return false;
    }
    
    #endregion

    /// <summary>
    /// sets the position of sliders to match player pref volumes
    /// </summary>
    private void SetAllSlidersToPos()
    {
        if (PlayerPrefs.HasKey(_mainVolumeKey.Value))
        {
            _sliderMainVolume.SetValueWithoutNotify(PlayerPrefs.GetFloat(_mainVolumeKey.Value));
        }
        if (PlayerPrefs.HasKey(_musicVolumeKey.Value))
        {
            _sliderMusicVolume.SetValueWithoutNotify(PlayerPrefs.GetFloat(_musicVolumeKey.Value));
        }
        if (PlayerPrefs.HasKey(_sfxVolumeKey.Value))
        {
            _sliderSFXVolume.SetValueWithoutNotify(PlayerPrefs.GetFloat(_sfxVolumeKey.Value));
        }
    }

    public void MainVolumeChanged(float value)
    {
        _audioMixer.SetFloat(_mainVolumeKey.Value, value);
        PlayerPrefs.SetFloat(_mainVolumeKey.Value, value);

    }

    public void SFXVolumeChanged(float value)
    {
        _audioMixer.SetFloat(_sfxVolumeKey.Value, value);
        PlayerPrefs.SetFloat(_sfxVolumeKey.Value, value);
    }

    public void MusicVolumeChanged(float value)
    {
        _audioMixer.SetFloat(_musicVolumeKey.Value, value);
        PlayerPrefs.SetFloat(_musicVolumeKey.Value, value);
    }

    public void ResolutionChanged()
    {
        PlayerPrefs.SetInt(_resolutionKey.Value, _resDropdown.value);
        // the actual resolution change takes place in the ResolutionDropdown script
    }

    public void FullscreenChanged()
    {
        int fsState = boolToInt(_toggleFullscreen.isOn);
        PlayerPrefs.SetInt(_fullscreenKey.Value, fsState);
        
        if (_toggleFullscreen.isOn)
        {
#if !UNITY_EDITOR
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
#endif
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }
}
