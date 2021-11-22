using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyLocalManager : MySingleton<MyLocalManager>
{
    public override bool DoDestroyOnLoad => true;
}
