using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums
{
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
