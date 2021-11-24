using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    public ScriptablePlayerEvent playerEvent;

    private void OnCollisionEnter(Collision collision)
    {
        playerEvent.SetAndRaise(collision.gameObject.name, Enums.PlayerEventAction.DEAD);
    }
}
