using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class GameStartup : MonoBehaviour
{
    [SerializeField] private Image _blackImage;
    [SerializeField] private TMP_Text _counterText;

    [SerializeField] private float _fadeSpeed;
    [SerializeField] private float _waitDelay = 2;
    [SerializeField] private float _counterDelay = 5;

    [SerializeField] private List<GameCoroutine> _coroutines;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(GameStartCoroutine());
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            StartCoroutine(GameStart2Coroutine());
        }
    }

    private IEnumerator GameStartCoroutine()
    {
        yield return StartCoroutine(FadeOutCoroutine());
        yield return StartCoroutine(WaitCoroutine(_waitDelay));
        yield return StartCoroutine(FadeInCoroutine());
        yield return StartCoroutine(CounterCoroutine());
    }

    private IEnumerator GameStart2Coroutine()
    {
        foreach (var coroutine in _coroutines)
        {
            yield return StartCoroutine(coroutine.ExecuteCoroutine());
        }
    }

    private IEnumerator FadeOutCoroutine()
    {
        Debug.Log("FadeOutCoroutine Start");

        while (_blackImage.color.a < 1)
        {
            _blackImage.color = _blackImage.color + new Color(0, 0, 0, _fadeSpeed * Time.deltaTime);
            yield return null;
        }

        Debug.Log("FadeOutCoroutine End");
    }

    private IEnumerator FadeInCoroutine()
    {
        Debug.Log("FadeInCoroutine Start");

        while (_blackImage.color.a > 0)
        {
            _blackImage.color = _blackImage.color - new Color(0, 0, 0, _fadeSpeed * Time.deltaTime);
            yield return null;
        }

        Debug.Log("FadeInCoroutine End");
    }

    private IEnumerator WaitCoroutine(float delay)
    {
        Debug.Log("WaitCoroutine Start");

        yield return new WaitForSeconds(delay);

        Debug.Log("WaitCoroutine End");
    }

    private IEnumerator CounterCoroutine()
    {
        Debug.Log("CounterCoroutine Start");

        float counter = _counterDelay;
        _counterText.gameObject.SetActive(true);

        while (counter > 0)
        {
            counter -= Time.deltaTime;
            _counterText.text = counter.ToString("N0");
            yield return null;
        }

        _counterText.gameObject.SetActive(false);

        Debug.Log("CounterCoroutine End");
    }
}
