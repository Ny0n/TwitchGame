using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Coroutines/Wait")]
public class WaitCoroutine : GameCoroutine
{
    [SerializeField] private float _delay;


    public override IEnumerator ExecuteCoroutine()
    {
        Debug.Log("WaitCoroutine Start");

        yield return new WaitForSeconds(_delay);

        Debug.Log("WaitCoroutine End");
    }
}
