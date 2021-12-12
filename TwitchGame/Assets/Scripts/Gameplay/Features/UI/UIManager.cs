using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Slider timerSlider;
    public TMP_Text currentStateText;
    public TMP_Text playersListText;
    public TMP_Text twitchText;
    private TwitchClient _twitchClient;

    public ScriptableTimerVariable roundTimer;
    public ScriptablePlayersList playersList;

    private void Start()
    {
        _twitchClient = FindObjectOfType<TwitchClient>();
    }

    private void OnEnable() => playersList.Players.ValueChanged += OnPlayersUpdated;
    private void OnDisable() => playersList.Players.ValueChanged -= OnPlayersUpdated;

    void OnPlayersUpdated()
    {
        playersListText.text = "";
        foreach (Player player in playersList.GetPlayersList())
        {
            playersListText.text += GetLineFormat(player);
        }
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
        timerSlider.value = roundTimer.Value / 20f;
        currentStateText.text = GameManager.Instance.CurrentState.ToString();
        
        const string connected = "<color=green>connected</color>";
        const string disconnected = "<color=red>disconnected</color>";

        twitchText.text = _twitchClient.Connected ? connected : disconnected;
    }
}
