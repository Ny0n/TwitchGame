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

    public ScriptableGameStateVariable gameState;
    public ScriptableTimerVariable roundTimer;
    public ScriptableBoolVariable twitchStatus;
    public ScriptablePlayersList playersList;

    private void Start()
    {
        OnPlayersUpdated();
        OnTwitchUpdated();
    }

    private void OnEnable()
    {
        playersList.Players.ValueChanged += OnPlayersUpdated;
        twitchStatus.ValueChanged += OnTwitchUpdated;
    }

    private void OnDisable()
    {
        playersList.Players.ValueChanged -= OnPlayersUpdated;
        twitchStatus.ValueChanged -= OnTwitchUpdated;
    }

    void OnPlayersUpdated()
    {
        playersListText.text = "";
        foreach (Player player in playersList.GetPlayersList())
        {
            playersListText.text += GetLineFormat(player);
        }
    }
    
    void OnTwitchUpdated()
    {
        const string connected = "<color=green>connected</color>";
        const string disconnected = "<color=red>disconnected</color>";

        twitchText.text = twitchStatus.Value ? connected : disconnected;
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
        currentStateText.text = gameState.Value.ToString();
    }
}
