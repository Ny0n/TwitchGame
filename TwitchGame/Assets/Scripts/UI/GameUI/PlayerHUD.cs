using UnityEngine;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private TextMeshProUGUI _numberText;
    [SerializeField] private GameObject _canvas;

    private readonly Quaternion _rotation = Quaternion.Euler(90, 0, 0);

    void Start()
    {
        _numberText.text = _playerData.Player.Number.ToString();
    }

    private void Update()
    {
        if (_canvas.transform.rotation != _rotation)
            _canvas.transform.rotation = _rotation;
    }
}
