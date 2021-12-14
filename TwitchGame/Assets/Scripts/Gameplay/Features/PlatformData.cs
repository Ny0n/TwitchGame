using System.Collections.Generic;
using UnityEngine;

public class PlatformData : MonoBehaviour
{
    [SerializeField] private List<Material> _materials = new List<Material>();
    private Renderer _renderer;
    
    public int CurrentState { get; private set; }
    public Vector2 CurrentPosition { get; set; }

    public void Init(Vector2 position)
    {
        CurrentState = -1;
        CurrentPosition = position;
    }

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        GoToNextState();
    }

    public void SetState(int state)
    {
        CurrentState = state;
        Material mat = _materials[state];
        _renderer.material = mat;
    }

    public void GoToNextState()
    {
        CurrentState++;
        if (CurrentState >= _materials.Count)
        {
            Destroy(gameObject);
            return;
        }

        SetState(CurrentState);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            GoToNextState();
    }
}
