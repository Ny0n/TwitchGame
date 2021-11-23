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
        WAITINGFORPLAYERS,
        STARTINGGAME,
        PLAYING,
        ENDINGGAME,
    }
}
