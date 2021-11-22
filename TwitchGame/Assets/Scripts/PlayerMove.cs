using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MyMonoBehaviour
{

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);
        GameManager.Instance?.Hey();
    }

    public override void DoUpdate()
    {

    }
}
