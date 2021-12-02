using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public GameObject Platform { get; private set; }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            Platform = collision.gameObject;
            Platform.GetComponent<PlatformPlayerInfo>().Player = GetComponent<PlayerData>().Player;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            Platform.GetComponent<PlatformPlayerInfo>().Player = null;
            Platform = null;
        }
    }

    private void OnDestroy()
    {
        if (Platform != null)
            Platform.GetComponent<PlatformPlayerInfo>().Player = null;
    }
}
