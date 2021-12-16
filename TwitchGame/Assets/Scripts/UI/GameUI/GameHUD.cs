using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameHUD : MonoBehaviour
{
    [SerializeField] private Slider _timerSlider;
    [SerializeField] private TMP_Text _currentStateText;
    [SerializeField] private TMP_Text _playersListText;
    [SerializeField] private TMP_Text _twitchText;

    [SerializeField] private ScriptableSettings _settings;
    [SerializeField] private ScriptableGameStateVariable _gameState;
    [SerializeField] private ScriptableTimerVariable _roundTimer;
    [SerializeField] private ScriptableBoolVariable _twitchStatus;
    [SerializeField] private ScriptablePlayersList _playersList;

    private void Start()
    {
        _timerSlider.maxValue = _settings.RoundDuration;
        _timerSlider.value = _timerSlider.maxValue;
        OnPlayersUpdated();
        OnTwitchUpdated();
    }

    private void OnEnable()
    {
        _playersList.Players.ValueChanged += OnPlayersUpdated;
        _twitchStatus.ValueChanged += OnTwitchUpdated;
    }

    private void OnDisable()
    {
        _playersList.Players.ValueChanged -= OnPlayersUpdated;
        _twitchStatus.ValueChanged -= OnTwitchUpdated;
    }

    void OnPlayersUpdated()
    {
        _playersListText.text = "";
        foreach (Player player in _playersList.GetPlayersList())
        {
            _playersListText.text += GetLineFormat(player);
        }
    }
    
    void OnTwitchUpdated()
    {
        const string connected = "<color=green>connected</color>";
        const string disconnected = "<color=red>disconnected</color>";

        _twitchText.text = _twitchStatus.Value ? connected : disconnected;
    }

    private string GetLineFormat(Player player)
    {
        const string alive = "<color=green>alive</color>";
        const string dead = "<color=red>dead</color>";
        
        string format = player.Number + ": " + player.Name;
        format += " (" + (player.IsAlive ? alive : dead) + ")" + "\n";

        return format;
    }

    // Update is called once per frame
    void Update()
    {
        _timerSlider.value = _roundTimer.Value; // not really necessary to subscribe to an event for this
        _currentStateText.text = _gameState.Value.ToString();
    }
}
