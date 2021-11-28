using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    public Slider TimerSlider;
    public TMP_Text CurrentStateText;
    public TMP_Text PlayersList;

    public ScriptableFloatVariable TimerVariable;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TimerSlider.value = TimerVariable.Value / 20f;
        CurrentStateText.text = GameManager.Instance.CurrentState.ToString();

        PlayersList.text = "";
        foreach (var entry in PlayersManager.Instance.Players)
        {
            Player player = entry.Value;
            PlayersList.text += player.Number + ": " + player.Name + " (" + (player.IsAlive ? "alive" : "dead") + ")" + "\n"; // TODO temporaire
        }
    }
}
