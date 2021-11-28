using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public GameObject PlayerObject { get; set; }
    public string Name { get; set; }
    public int Number { get; set; }
    public bool IsAlive { get; set; } = true;

    public Player(GameObject gameObject, string name, int number)
    {
        PlayerObject = gameObject;
        Name = name;
        Number = number;
    }

    public void Remove()
    {
        if (PlayerObject != null) Object.Destroy(PlayerObject);
    }
}
