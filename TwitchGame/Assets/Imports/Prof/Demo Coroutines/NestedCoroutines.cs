using System.Collections;

using UnityEngine;

public class NestedCoroutines : MonoBehaviour
{
    private IEnumerator Start()
    {
        _myCoroutine = StartCoroutine(MyCoroutine());
        yield return _myCoroutine;

        Debug.Log("Start end");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            StopCoroutine(_myCoroutine);
            StopAllCoroutines();
        }
    }

    private IEnumerator MyCoroutine()
    {
        StartCoroutine(MyCoroutine2());

        while (true)
        {
            Debug.Log($"[{Time.frameCount}] MyCoroutine");
            yield return null;
        }

        // Debug.Log($"[{Time.frameCount}] MyCoroutine end");
    }

    private IEnumerator MyCoroutine2()
    {
        while (true)
        {
            Debug.Log($"<color=cyan>[{Time.frameCount}] MyCoroutine 2</color>");
            yield return null;
        }

        // Debug.Log($"<color=cyan>[{Time.frameCount}] MyCoroutine 2 end</color>");
    }

    // private float duration = 2;
    private Coroutine _myCoroutine;
    // private bool _isRunning;
}
