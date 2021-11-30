using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MySingleton<T> : MonoBehaviour where T : MySingleton<T>
{
    public static T Instance { get; private set; }

    protected virtual bool DoDestroyOnLoad { get; } = false;

    protected virtual void Awake()
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
