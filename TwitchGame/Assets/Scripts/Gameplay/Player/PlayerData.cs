using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public Player Player { get; set; }

    private GameObject _platform;

    [SerializeField]
    private GameObject skinParent;

    public Animator _animator;

    private bool _rebindAnimator = false;

    private void Start()
    {
        //_animator = GetComponent<Animator>();
    }

    public void SetSkin(GameObject skin)
    {
        Destroy(skinParent.transform.GetChild(0).gameObject);
        Instantiate(skin, skinParent.transform);
        _rebindAnimator = true;
    }
    private void LateUpdate()
    {
        if (_rebindAnimator)
        {
            _animator.Rebind();
            _rebindAnimator = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            _platform = collision.gameObject;
            _platform.GetComponent<PlatformPlayerInfo>().Player = Player;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            _platform.GetComponent<PlatformPlayerInfo>().Player = null;
            _platform = null;
        }
    }

    private void OnDestroy()
    {
        if (_platform != null)
            _platform.GetComponent<PlatformPlayerInfo>().Player = null;
    }
}
