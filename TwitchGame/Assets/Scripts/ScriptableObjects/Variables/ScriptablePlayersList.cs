using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Variable/List/Players")]
public class ScriptablePlayersList : ScriptableObject
{
    [field: SerializeField] private List<string> _names; // to show the players in the inspector
    
    public MyDictionary<string, Player> Players { get; private set; } // THE players

    public List<string> GetNamesList() => Players.Keys.ToList();
    public List<Player> GetPlayersList() => Players.Values.ToList();
    public bool IsPlayerRegistered(string playerName) => Players.ContainsKey(playerName);
    public Player TryGetPlayer(string playerName)
    {
        return IsPlayerRegistered(playerName) ? Players[playerName] : null;
    }

    private void OnEnable()
    {
        Players = new MyDictionary<string, Player>();
        Players.ValueChanged += UpdateForInspector;
        UpdateForInspector();
    }

    private void UpdateForInspector()
    {
        _names = GetNamesList();
    }
}
