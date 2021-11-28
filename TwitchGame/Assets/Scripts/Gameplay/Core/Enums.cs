using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums
{
    public static readonly Dictionary<CommandType, string> Commands = new Dictionary<Enums.CommandType, string>
    {
        [CommandType.REGISTER] = "!join",
        [CommandType.UNREGISTER] = "!leave",
        [CommandType.UP] = "!up",
        [CommandType.DOWN] = "!down",
        [CommandType.LEFT] = "!left",
        [CommandType.RIGHT] = "!right",
    };
    
    public enum CommandType
    {
        // public (in the Commands Dictionnary)
        REGISTER,
        UNREGISTER,
        UP,
        DOWN,
        LEFT,
        RIGHT,

        // private
        STAY,
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
