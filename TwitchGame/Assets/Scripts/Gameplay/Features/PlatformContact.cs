using UnityEngine;

public class PlatformContact : MonoBehaviour
{
    public float destructionTimer = 2f;

    private bool _destructionStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        StartDestruction();
    }

    void StartDestruction()
    {
        if (_destructionStarted) return;
        Destroy(gameObject, destructionTimer);
        _destructionStarted = true;
    }
}
