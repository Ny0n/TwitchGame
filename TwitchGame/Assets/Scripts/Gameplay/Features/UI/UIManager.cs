using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    public Slider TimerSlider;
    public TMP_Text CurrentStateText;

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
    }
}
