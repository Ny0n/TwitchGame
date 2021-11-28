using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    public PlayerData playerData;
    public TextMeshProUGUI numberText;

    // Start is called before the first frame update
    void Start()
    {
        numberText.text = playerData.Player.Number.ToString();
    }
}
