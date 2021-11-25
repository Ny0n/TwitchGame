using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPlayerInfo : MonoBehaviour
{
    private GameObject _player;
    public bool HasPlayer { get; set; } = false; // TODO REDO
    
    //public bool HasPlayer
    //{
    //    get
    //    {
    //        return _player != null;
    //    }
    //}

    public Player Player
    {
        get
        {
            if (HasPlayer)
            {
                return _player.GetComponent<PlayerData>().Player;
            }
            return null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        _player = collision.gameObject;
        HasPlayer = true;
    }

    // TODO CHECK IN UPDATE ??

    private void OnCollisionExit(Collision collision)
    {
        print("EXIT");
        _player = null;
        HasPlayer = false;
    }
}
