using System.Collections;

using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Coroutines/Fade")]
public class FadeCoroutine : GameCoroutine
{
    [SerializeField] private bool _isFadeIn;
    [SerializeField] private float _fadeSpeed;
    [SerializeField] private ImageVariable _fadeImage;

    public override IEnumerator ExecuteCoroutine()
    {
        Debug.Log("FadeCoroutine Start");

        if (_isFadeIn)
        {
            while (_fadeImage.Value.color.a > 0)
            {
                // 0            => 0
                // fadeSpeed    => 1

                _fadeImage.Value.color -= new Color(0, 0, 0, _fadeSpeed * Time.deltaTime);
                yield return null;
            }
        }
        else
        {
            while (_fadeImage.Value.color.a < 1)
            {
                _fadeImage.Value.color += new Color(0, 0, 0, _fadeSpeed * Time.deltaTime);
                yield return null;
            }
        }

        Debug.Log("FadeCoroutine End");
    }
}
