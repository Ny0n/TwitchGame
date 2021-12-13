using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Coroutines/Counter")]
public class CounterCoroutine : GameCoroutine
{
    [SerializeField] private TextVariable _counterText;
    [SerializeField] private float _counterDelay;

    public override IEnumerator ExecuteCoroutine()
    {
        Debug.Log("CounterCoroutine Start");

        float counter = _counterDelay;
        _counterText.Value.gameObject.SetActive(true);

        while (counter > 0)
        {
            counter -= Time.deltaTime;
            _counterText.Value.text = counter.ToString("N0");
            yield return null;
        }

        _counterText.Value.gameObject.SetActive(false);

        Debug.Log("CounterCoroutine End");
    }
}
