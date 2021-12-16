using UnityEngine;

public class DestroyOnLoad : MonoBehaviour
{
    [SerializeField] private bool DoDestroyOnLoad = true;
    
    private void Awake()
    {
        if (DoDestroyOnLoad)
            Destroy(gameObject);
    }
}
