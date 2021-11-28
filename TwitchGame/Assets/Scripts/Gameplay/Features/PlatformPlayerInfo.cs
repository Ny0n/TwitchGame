using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPlayerInfo : MonoBehaviour
{
    private Player _player;

    public bool HasPlayer
    {
        get
        {
            return _player is Player;
        }
    }

    public Player Player
    {
        get
        {
            return _player;
        }
        set
        {
            _player = value;
        }
    }
}
