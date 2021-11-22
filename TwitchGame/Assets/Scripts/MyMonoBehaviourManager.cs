using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMonoBehaviourManager : MonoBehaviour
{
    private MyMonoBehaviour[] _myMonoBehaviours;

    // Start is called before the first frame update
    void Start()
    {
        _myMonoBehaviours = FindObjectsOfType<MyMonoBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (MyMonoBehaviour myMonoBehaviour in _myMonoBehaviours)
        {
            myMonoBehaviour.DoUpdate();
        }
    }
}
