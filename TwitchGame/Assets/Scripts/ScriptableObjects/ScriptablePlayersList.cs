using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Variable/List/Players")]
public class ScriptablePlayersList : ScriptableObject
{
    public MyDictionary<string, Player> Dico { get; private set; }

    public List<string> GetNamesList() => Dico.Keys.ToList();
    public List<Player> GetPlayersList() => Dico.Values.ToList();
    public bool IsPlayerRegistered(string name) => Dico.ContainsKey(name);

    private void OnEnable()
    {
        Dico = new MyDictionary<string, Player>();
    }
}
