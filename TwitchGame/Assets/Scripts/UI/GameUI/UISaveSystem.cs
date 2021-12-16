using UnityEngine;
using DG.Tweening;

public class UISaveSystem : MonoBehaviour
{
    [SerializeField] private CanvasGroup _buttonsGroup;
    
    [SerializeField] private GameObject _loadingIcon;
    [SerializeField] private CanvasGroup _successIcon;
    [SerializeField] private CanvasGroup _failureIcon;
    
    [SerializeField] private float _delay = 0.5f;
    [SerializeField] private float _fadeOutDelay = 1f;

    public void StartProcess()
    {
        DOTween.KillAll();
        
        _buttonsGroup.interactable = false;
        _buttonsGroup.alpha = 0.5f;

        _loadingIcon.SetActive(true);
    }
    
    public void EndProcess()
    {
        _buttonsGroup.interactable = true;
        _buttonsGroup.alpha = 1;
        
        _loadingIcon.SetActive(false);
    }

    private void ShowEnd(CanvasGroup cg)
    {
        cg.alpha = 1;
        
        Sequence sequence = DOTween.Sequence();

        sequence.AppendInterval(_delay);
        sequence.Append(cg.DOFade(0, _fadeOutDelay));
        sequence.AppendInterval(_fadeOutDelay);

        sequence.OnComplete(() =>
        {
            cg.alpha = 0;
        });

        sequence.OnKill(() =>
        {
            cg.alpha = 0;
        });
    }

    public void ShowSuccess()
    {
        ShowEnd(_successIcon);
    }

    public void ShowFailure()
    {
        ShowEnd(_failureIcon);
    }
}
