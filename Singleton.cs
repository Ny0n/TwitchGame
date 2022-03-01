using JetBrains.Annotations;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    
    // [CanBeNull]
    public static T Instance { get; private set; }
    
    protected virtual bool DestroyOnLoad => true;

    protected virtual void SingletonAwake() { }

    protected void Awake()
    {
        if (Instance == null)
        {
            Instance = (T)this;
            if (!DestroyOnLoad)
                DontDestroyOnLoad(this);
            SingletonAwake();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
}
