using System.Collections;
using System.Collections.Generic;
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

    public void OnEnable()
    {

        SetAllSlidersToPos();

        //SetAllVolumes();
        //it works but it has to happen on main menu not options menu


    }

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

    /// <summary>
    /// set mixer volumes to match player pref volumes
    /// </summary>
    //private void SetAllVolumes()
    //{
    //    if (PlayerPrefs.HasKey("mainVolume"))
    //    {
    //        _audioMixer.SetFloat("mainVolume", PlayerPrefs.GetFloat("mainVolume"));
    //    }
    //    if (PlayerPrefs.HasKey("sfxVolume"))
    //    {
    //        _audioMixer.SetFloat("sfxVolume", PlayerPrefs.GetFloat("sfxVolume"));
    //    }
    //    if (PlayerPrefs.HasKey("musicVolume"))
    //    {
    //        _audioMixer.SetFloat("musicVolume", PlayerPrefs.GetFloat("musicVolume"));
    //    }

    //}

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
    
    /// <summary>
    /// toggles fullscreen on & off
    /// </summary>
    /// <param name="isFullScreen">bool : is checkbox checked</param>
    public void ToggleFullscreen(bool isFullScreen)
    {
        if (isFullScreen)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        } else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }
}
