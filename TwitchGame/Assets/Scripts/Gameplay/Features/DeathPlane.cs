using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    public ScriptablePlayerEvent playerEvent;

    private void OnCollisionEnter(Collision collision)
    {
        playerEvent.SetAndRaise(collision.gameObject.name, Enums.PlayerEventAction.Dead);
    }
}
