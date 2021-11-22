using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MyMonoBehaviour
{
    public float MovePower = 2f;
    public float JumpPower = 3f;

    private Animator _anim;
    private Rigidbody _rb;
    private bool _rotating = false;

    void Start()
    {
        GameManager.Instance?.Hey();
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }

    public override void DoUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Z)) GoUp();
        if (Input.GetKeyDown(KeyCode.S)) GoDown();
        if (Input.GetKeyDown(KeyCode.Q)) GoLeft();
        if (Input.GetKeyDown(KeyCode.D)) GoRight();
        _anim.SetBool("Rotating", _rotating);
        print(_rotating);
    }

    private void GoDir(Vector3 targetRotation)
    {
        Quaternion rotation = Quaternion.Euler(targetRotation);
        StartCoroutine(rotateAndMove(gameObject, rotation, 1f));
    }

    public void GoUp()
    {
        GoDir(new Vector3(0f, 0f, 0f));
    }
    
    public void GoDown()
    {
        GoDir(new Vector3(0f, 180f, 0f));
    }
    
    public void GoLeft()
    {
        GoDir(new Vector3(0f, -90f, 0f));
    }
    
    public void GoRight()
    {
        GoDir(new Vector3(0f, 90f, 0f));
    }

    bool coroutineRunning = false;
    IEnumerator rotateAndMove(GameObject go, Quaternion newRot, float duration)
    {
        if (coroutineRunning)
        {
            yield break;
        }
        coroutineRunning = true;

        Quaternion currentRot = go.transform.rotation;
        if (currentRot == newRot)
        {
            // Wait 1 sec
            yield return new WaitForSeconds(1);
        }
        else
        {
            // Rotate over 1 sec
            _rotating = true;
            float counter = 0;
            while (counter < duration)
            {
                counter += Time.deltaTime;
                go.transform.rotation = Quaternion.Lerp(currentRot, newRot, counter / duration);
                yield return null;
            }
            _rotating = false;
        }
        
        // Jump
        _rb.velocity = transform.forward * MovePower + Vector3.up * JumpPower;
        _anim.SetTrigger("Jump");

        coroutineRunning = false;
    }


    private void OnCollisionEnter(Collision collision)
    {
        _rb.velocity = Vector3.zero;
        _anim.SetTrigger("JumpEnd");
    }
}
