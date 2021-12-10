using System.Collections;

using UnityEngine;

public abstract class GameCoroutine : ScriptableObject
{
    public abstract IEnumerator ExecuteCoroutine();
}
