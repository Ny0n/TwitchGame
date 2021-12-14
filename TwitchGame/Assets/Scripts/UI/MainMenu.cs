using UnityEngine;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private AudioMixer _audioMixer;

    public void Start()
    {
        SetAllVolumes();
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Application quit");
    }

    public void PlayGame()
    {
        //start game scenes
    }

    private void SetAllVolumes()
    {
        if (PlayerPrefs.HasKey("mainVolume"))
        {
            _audioMixer.SetFloat("mainVolume", PlayerPrefs.GetFloat("mainVolume"));
        }
        if (PlayerPrefs.HasKey("sfxVolume"))
        {
            _audioMixer.SetFloat("sfxVolume", PlayerPrefs.GetFloat("sfxVolume"));
        }
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            _audioMixer.SetFloat("musicVolume", PlayerPrefs.GetFloat("musicVolume"));
        }

    }
}
