using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _health;

    [SerializeField]
    private FloatVariable _playerHealth;

    // Update is called once per frame
    void Update()
    {
        _health.text = _playerHealth.Value.ToString();
    }
}
