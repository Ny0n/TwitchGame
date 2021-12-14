using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField]
    private AudioMixer _audioMixer;

    [SerializeField]
    private Slider _sliderMainVolume;

    [SerializeField]
    private Slider _sliderSFXVolume;

    [SerializeField]
    private Slider _sliderMusicVolume;

    [SerializeField]
    private Toggle _toggleFullscreen;

    [SerializeField]
    private TMPro.TMP_Dropdown _resDropdown;

    public void OnEnable()
    {

        SetAllSlidersToPos();

        SetResAndFullscreen();
    }

    private void SetResAndFullscreen()
    {
        if (PlayerPrefs.HasKey("fullscreen"))
        {
            bool fsState = intToBool(PlayerPrefs.GetInt("fullscreen"));

            _toggleFullscreen.isOn = fsState;

        }

        if (PlayerPrefs.HasKey("resolution"))
        {
            Debug.Log("resol : " + PlayerPrefs.GetInt("resolution"));
            _resDropdown.value = PlayerPrefs.GetInt("resolution");
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
        if (PlayerPrefs.HasKey("mainVolume"))
        {
            _sliderMainVolume.SetValueWithoutNotify(PlayerPrefs.GetFloat("mainVolume"));
        }
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            _sliderMusicVolume.SetValueWithoutNotify(PlayerPrefs.GetFloat("musicVolume"));
        }
        if (PlayerPrefs.HasKey("sfxVolume"))
        {
            _sliderSFXVolume.SetValueWithoutNotify(PlayerPrefs.GetFloat("sfxVolume"));
        }
    }

    //names are magic. Store them?
    public void MainVolumeChanged(float value)
    {
        _audioMixer.SetFloat("mainVolume", value);

        PlayerPrefs.SetFloat("mainVolume", value);

    }

    public void SFXVolumeChanged(float value)
    {
        _audioMixer.SetFloat("sfxVolume", value);

        PlayerPrefs.SetFloat("sfxVolume", value);
    }

    public void MusicVolumeChanged(float value)
    {
        _audioMixer.SetFloat("musicVolume", value);

        PlayerPrefs.SetFloat("musicVolume", value);
    }

    public void ResolutionChanged()
    {
        PlayerPrefs.SetInt("resolution", _resDropdown.value);

        Debug.Log("value : " + _resDropdown.value);

        //PROBLEM IN RESOLUTION DROPDOWN ==> WHEN OPTIONS ARE CREATED THEY SET PLAYERPREF WITH CALLBACK OF VALUECHANGED
    }

    public void FullscreenChanged()
    {
        int fsState = boolToInt(_toggleFullscreen.isOn);

        PlayerPrefs.SetInt("fullscreen", fsState);


    }

    /// <summary>
    /// toggles fullscreen on & off
    /// </summary>
    /// <param name="isFullScreen">bool : is checkbox checked</param>
    public void ToggleFullscreen(bool isFullScreen)
    {
        if (isFullScreen)
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

