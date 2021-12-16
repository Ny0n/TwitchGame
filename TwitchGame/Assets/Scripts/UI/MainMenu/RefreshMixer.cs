using UnityEngine;
using UnityEngine.Audio;

public class RefreshMixer : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    
    [Header("PlayerPrefs keys")]
    [SerializeField] private ScriptableStringVariable _mainVolumeKey;
    [SerializeField] private ScriptableStringVariable _sfxVolumeKey;
    [SerializeField] private ScriptableStringVariable _musicVolumeKey;
    
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey(_mainVolumeKey.Value))
        {
            _audioMixer.SetFloat(_mainVolumeKey.Value, PlayerPrefs.GetFloat(_mainVolumeKey.Value));
        }
        if (PlayerPrefs.HasKey(_musicVolumeKey.Value))
        {
            _audioMixer.SetFloat(_musicVolumeKey.Value, PlayerPrefs.GetFloat(_musicVolumeKey.Value));
        }
        if (PlayerPrefs.HasKey(_sfxVolumeKey.Value))
        {
            _audioMixer.SetFloat(_sfxVolumeKey.Value, PlayerPrefs.GetFloat(_sfxVolumeKey.Value));
        }
    }
}
