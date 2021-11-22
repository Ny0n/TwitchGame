using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MySingleton<GameManager>
{
    public override bool DoDestroyOnLoad => false;

    public void Hey()
    {
        print("Hey");
    }
}
