using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MySingleton<T> : MonoBehaviour where T : MySingleton<T>
{
    public static T Instance { get; private set; }

    public abstract bool DoDestroyOnLoad { get; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = (T)this;
            if (!DoDestroyOnLoad)
                DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
