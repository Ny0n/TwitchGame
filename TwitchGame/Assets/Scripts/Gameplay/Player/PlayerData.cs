using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public Player Player { get; set; }

    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _skinParent;
    [SerializeField] private GameObject _hudParent;

    private int _rebindAnimator;
    private readonly int Win1 = Animator.StringToHash("Win");

    private void Update()
    {
        Player.Position = transform.position;
    }

    private void LateUpdate()
    {
        if (_rebindAnimator > 0)
        {
            _animator.Rebind();
            _rebindAnimator--;
        }
    }

    public void SetSkin(GameObject skin)
    {
        Destroy(_skinParent.transform.GetChild(0).gameObject);
        Instantiate(skin, _skinParent.transform);
        _rebindAnimator = 10;
    }

    public void Win()
    {
        _animator.SetTrigger(Win1);
        Destroy(_hudParent);
    }
}
