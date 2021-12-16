using UnityEngine;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    public PlayerData playerData;
    public TextMeshProUGUI numberText;
    public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        numberText.text = playerData.Player.Number.ToString();
    }

    private void Update()
    {
        canvas.transform.rotation = Quaternion.Euler(90, 0, 0); // TODO maybe opti (rotation != ...)
    }
}
