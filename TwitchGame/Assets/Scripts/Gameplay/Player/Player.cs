using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Object
{
    public GameObject PlayerObject { get; private set; }
    public string Name { get; private set; } = "NONAME";
    public int Number { get; private set; } = -1;
    public bool IsAlive { get; private set; } = true;

    private GameObject skin;

    public Player(string name, int number)
    {
        Name = name;
        Number = number;
    }

    public void Instantiate(GameObject prefab, Vector3 position)
    {
        // creation of player scene character
        PlayerObject = Instantiate(prefab, position, Quaternion.identity);
        PlayerObject.GetComponent<PlayerData>().Player = this;
        PlayerObject.name = Name;
        UpdateSkin();
    }

    public void SetSkin(GameObject skin)
    {
        this.skin = skin;
        UpdateSkin();
    }

    public void Remove()
    {
        if (PlayerObject != null) Destroy(PlayerObject);
    }

    private void UpdateSkin()
    {
        if (skin != null && PlayerObject != null)
            PlayerObject.GetComponent<PlayerData>().SetSkin(skin);
    }

    public void Kill()
    {
        IsAlive = false;
        Remove();
    }
}
