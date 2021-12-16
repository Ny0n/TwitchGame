using UnityEngine;
using TMPro;

public class GameSettingsMenu : MonoBehaviour
{
    [SerializeField] private ScriptableSettings _settings;
    
    [Header("Max players")]
    [SerializeField] private TMP_Text _maxPlayersText;
    [SerializeField] private int _maxPlayersMin = 2;
    [SerializeField] private int _maxPlayersMax = 50;
    
    [Header("Round duration")]
    [SerializeField] private TMP_Text _roundDurationText;
    [SerializeField] private float _roundDurationMin = 5;
    [SerializeField] private float _roundDurationMax = 60;

    private void Start()
    {
        RefreshUI();
    }

    private void RefreshUI()
    {
        OnMaxPlayersChanged(_settings.MaxNumberOfPlayers.ToString());
        OnRoundDurationChanged(_settings.RoundDuration.ToString());
    }

    public void OnMaxPlayersChanged(string value)
    {
        if (int.TryParse(value, out int r)) // if we typed a correct int
        {
            if (r >= _maxPlayersMin && r <= _maxPlayersMax)
            {
                _settings.MaxNumberOfPlayers = r;
                _maxPlayersText.text = r.ToString();
            }
        }
    }
    
    public void OnRoundDurationChanged(string value)
    {
        if (float.TryParse(value, out float r)) // if we typed a correct float
        {
            if (r >= _roundDurationMin && r <= _roundDurationMax)
            {
                _settings.RoundDuration = r;
                _roundDurationText.text = r.ToString();
            }
        }
    }

    public void LoadFromGoogle()
    {
        int maxPlayers = 20;
        float roundDuration = 15f;
        
        _settings.MaxNumberOfPlayers = maxPlayers;
        _settings.RoundDuration = roundDuration;
        
        RefreshUI();
    }
}
