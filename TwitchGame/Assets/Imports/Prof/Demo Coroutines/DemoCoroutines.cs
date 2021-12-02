using System;
using System.Collections;

using UnityEngine;

public class DemoCoroutines : MonoBehaviour
{
    [SerializeField] private bool _myBool;

    private void OnValidate()
    {
        //Debug.Log($"[frame {Time.frameCount}] MyBool = {_myBool}");

    }

    private void Awake()
    {
        Application.targetFrameRate = 10;
    }

    private void Update()
    {
        Debug.Log($"[frame {Time.frameCount}] Hello from Update");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log($"<color=green>[frame {Time.frameCount}] Before coroutine call</color>");
            _myOtherCoroutine = StartCoroutine(MyOtherCoroutine());
            _myCoroutine = StartCoroutine(MyCoroutine());
            Debug.Log($"<color=green>[[frame {Time.frameCount}] After coroutine call</color>");
            Debug.Break();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StopAllCoroutines();
        }


        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            if (_myOtherCoroutine != null)
            {
                Debug.Log($"<color=green>[frame {Time.frameCount}] Stopping MyOtherCoroutine</color>");
                StopCoroutine(_myOtherCoroutine);
                _myOtherCoroutine = null;
                Debug.Break();
            }
            else
            {
                Debug.Log($"<color=green>[frame {Time.frameCount}] No coroutine to stop</color>");
                Debug.Break();
            }
        }
    }

    private void LateUpdate()
    {
        Debug.Log($"<color=yellow>[frame {Time.frameCount}] Hello from LateUpdate</color>");
    }

    private void FixedUpdate()
    {
        Debug.Log($"<color=red>[frame {Time.frameCount}] Hello from FixedUpdate</color>");
    }


    private IEnumerator MyCoroutine()
    {
        Debug.Log($"<color=pink>[frame {Time.frameCount}] MyCoroutine start</color>");
        //yield return null;
        //Time.timeScale = 5;
        //yield return new WaitForSeconds(5f);
        //yield return new WaitForSecondsRealtime(5f);
        //Time.timeScale = 1;
        //yield return new WaitForEndOfFrame();
        //yield return new WaitUntil(IsTrue);
        //yield return new WaitWhile(IsFalse);
        //yield return new WaitForFixedUpdate();
        //yield return new MyWaitWhile(IsFalse);
        //yield return new MyWaitWhile2(IsFalse);

        yield return _myOtherCoroutine;

        Debug.Log($"<color={(Time.inFixedTimeStep ? "red" : "pink")}>[frame {Time.frameCount}] MyCoroutine end</color>");
        Debug.Break();
        _myCoroutine = null;
    }

    private IEnumerator MyOtherCoroutine()
    {
        Debug.Log($"<color=pink>[frame {Time.frameCount}] MyOtherCoroutine start</color>");
        yield return new WaitForSeconds(5f);
        Debug.Log($"<color=pink>[frame {Time.frameCount}] MyOtherCoroutine end</color>");
        Debug.Break();
        _myOtherCoroutine = null;
    }



    private IEnumerator MyUpdateCoroutine()
    {
        while (true)
        {
            Debug.Log("Du code");
            yield return _waitFor100ms;
        }
    }

    private WaitForSeconds _waitFor100ms = new WaitForSeconds(.1f);

    private IEnumerator My2dCoroutine()
    {
        Debug.Log("My2eCoroutine start");
        yield return new WaitForSeconds(10);
        Debug.Log("My2eCoroutine end");
    }

    private bool IsTrue()
    {
        return _myBool;
    }

    private bool IsFalse()
    {
        return !_myBool;
    }

    private Coroutine _myCoroutine;
    private Coroutine _myOtherCoroutine;
}

public class MyWaitWhile : CustomYieldInstruction
{
    public override bool keepWaiting => _predicate.Invoke();

    public MyWaitWhile(Func<bool> predicate)
    {
        _predicate = predicate;
    }

    private Func<bool> _predicate;
}

public class MyWaitWhile2 : IEnumerator
{
    public object Current => null;

    public bool MoveNext() => _predicate.Invoke();

    public void Reset() { }

    public MyWaitWhile2(Func<bool> predicate)
    {
        _predicate = predicate;
    }

    private Func<bool> _predicate;
}