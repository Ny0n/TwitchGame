using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Object
{
    public string Name { get; private set; } = "NONAME";
    public int Number { get; private set; } = -1;
    public GameObject PlayerObject { get; private set; }
    public bool IsAlive { get; private set; } = true;

    private GameObject skin;

    public Player(string name, int number)
    {
        Name = name;
        Number = number;
    }

    private void NotifyChange() => PlayersManager.Instance?.NotifyPlayerUpdated(this);

    public void Instantiate(GameObject prefab, Vector3 position)
    {
        // creation of player scene character
        PlayerObject = Instantiate(prefab, position, Quaternion.identity);
        PlayerObject.GetComponent<PlayerData>().Player = this;
        PlayerObject.name = Name;
        UpdateSkin();

        NotifyChange();
    }

    public void SetSkin(GameObject skin)
    {
        this.skin = skin;
        UpdateSkin();

        NotifyChange();
    }

    public void Remove()
    {
        if (PlayerObject != null)
        {
            Destroy(PlayerObject);
            NotifyChange();
        }
    }

    private void UpdateSkin()
    {
        if (skin != null && PlayerObject != null)
            PlayerObject.GetComponent<PlayerData>().SetSkin(skin);
    }

    public void Kill()
    {
        IsAlive = false;
        NotifyChange();
        Remove();
    }

    public override string ToString() => $"{{ Name = \"{Name}\", Number = \"{Number}\" }}";
}
