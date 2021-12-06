using System.Collections.Generic;
using UnityEngine;

public class PlatformContact : MonoBehaviour
{
    [SerializeField] private List<Material> _materials = new List<Material>();
    private Renderer _renderer;
    private int _currentState = -1;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        GoToNextState();
    }

    public void GoToNextState()
    {
        _currentState++;
        if (_currentState >= _materials.Count)
        {
            Destroy(gameObject);
            return;
        }
        
        Material mat = _materials[_currentState];
        _renderer.material = mat;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            GoToNextState();
    }
}
