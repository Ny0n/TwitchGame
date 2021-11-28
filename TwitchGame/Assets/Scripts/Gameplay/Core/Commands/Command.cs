using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command
{
    public string playerName;
    public Enums.CommandType type;

    public Command(string playerName, Enums.CommandType type)
    {
        this.playerName = playerName;
        this.type = type;
    }

    public override string ToString()
    {
        return $"{{ playerName = \"{playerName}\", type = \"{type}\" }}";
    }
}
