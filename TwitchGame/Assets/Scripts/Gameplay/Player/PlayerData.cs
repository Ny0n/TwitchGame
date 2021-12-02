using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public Player Player { get; set; }

    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _skinParent;

    private bool _rebindAnimator = false;

    private void LateUpdate()
    {
        if (_rebindAnimator)
        {
            _animator.Rebind();
            _rebindAnimator = false;
        }
    }

    public void SetSkin(GameObject skin)
    {
        Destroy(_skinParent.transform.GetChild(0).gameObject);
        Instantiate(skin, _skinParent.transform);
        _rebindAnimator = true;
    }
}
