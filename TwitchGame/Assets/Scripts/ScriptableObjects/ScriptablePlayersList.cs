using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Variable/List/Players")]
public class ScriptablePlayersList : ScriptableObject
{
    public MyDictionary<string, Player> Players { get; private set; }

    public List<string> GetNamesList() => Players.Keys.ToList();
    public List<Player> GetPlayersList() => Players.Values.ToList();
    public bool IsPlayerRegistered(string playerName) => Players.ContainsKey(playerName);

    private void OnEnable()
    {
        Players = new MyDictionary<string, Player>();
    }
}
