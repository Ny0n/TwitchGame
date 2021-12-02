using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Slider timerSlider;
    public TMP_Text currentStateText;
    public TMP_Text playersListText;

    public ScriptableFloatVariable timerVariable;
    public ScriptablePlayersList playersList;

    private void OnEnable() => playersList.Players.ValueChanged += OnPlayersUpdated;
    private void OnDisable() => playersList.Players.ValueChanged -= OnPlayersUpdated;

    void OnPlayersUpdated()
    {
        playersListText.text = "";
        foreach (Player player in playersList.GetPlayersList())
        {
            playersListText.text += player.Number + ": " + player.Name + " (" + (player.IsAlive ? "alive" : "dead") + ")" + "\n";
        }
    }

    // Update is called once per frame
    void Update()
    {
        timerSlider.value = timerVariable.Value / 20f;
        currentStateText.text = GameManager.Instance.CurrentState.ToString();
    }
}
