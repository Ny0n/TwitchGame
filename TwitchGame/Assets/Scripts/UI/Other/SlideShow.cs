using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SlideShow : MonoBehaviour
{
    [SerializeField] private ScriptableGameEvent _nextLevelEvent;
    
    [SerializeField] private List<CanvasGroup> _canvasGroups;
    
    [SerializeField] private float _pauseDelay = 1f;
    [SerializeField] private float _fadeInDelay = 1f;
    [SerializeField] private float _fadeOutDelay = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (var slide in _canvasGroups)
        {
            slide.alpha = 0;
            slide.gameObject.SetActive(true);
        }

        Sequence sequence = DOTween.Sequence();
        
        foreach (var slide in _canvasGroups)
        {
            sequence.Append(slide.DOFade(1, _fadeInDelay));
            sequence.AppendInterval(_fadeInDelay);
            sequence.AppendInterval(_pauseDelay);
            sequence.Append(slide.DOFade(0, _fadeOutDelay));
            sequence.AppendInterval(_fadeOutDelay);
        }

        sequence.OnComplete(() =>
        {
            _nextLevelEvent.Raise();
        });
    }
}
