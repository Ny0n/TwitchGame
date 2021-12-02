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
            if (myMonoBehaviour == null) continue;
            if (!myMonoBehaviour.gameObject.activeInHierarchy) continue;
            myMonoBehaviour.DoUpdate();
        }
    }
}
