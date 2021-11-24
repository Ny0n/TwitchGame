using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums : MonoBehaviour
{
    public enum Command
    {
        REGISTER,
        UNREGISTER,
        STAY,
        UP,
        DOWN,
        LEFT,
        RIGHT,
    }

    public enum GameState
    {
        PAUSED,
        WAITINGFORPLAYERS,
        STARTINGGAME,
        PLAYING,
        ENDINGGAME,
    }

    public enum PlayerEventAction
    {
        ADD,
        REMOVE,
        DEAD,
    }
}
