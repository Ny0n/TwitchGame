using UnityEngine;

public class RotateCube : MonoBehaviour
{
    public float speed = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, speed);
        transform.Rotate(Vector3.right, speed);
        transform.Rotate(Vector3.forward, speed);
    }
}
