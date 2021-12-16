using System.Linq;
using UnityEngine;
using TMPro;

public class WinnerHUD : MonoBehaviour
{
    [SerializeField] private ScriptablePlayersList _playersList;
    
    [Header("Winner")]
    [SerializeField] private GameObject _winnerFrame;
    [SerializeField] private TMP_Text _winnerText;
    
    [Header("Draw")]
    [SerializeField] private GameObject _drawFrame;

    void Start()
    {
        Player winner = (from player in _playersList.Players where player.Value.IsAlive select player.Value).FirstOrDefault();

        if (winner is Player)
        {
            _winnerText.text = winner.Name;
            _winnerFrame.SetActive(true);
            _drawFrame.SetActive(false);
        }
        else
        {
            _winnerFrame.SetActive(false);
            _drawFrame.SetActive(true);
        }
    }
}
