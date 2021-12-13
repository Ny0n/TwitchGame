using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Variable/List/Platforms")]
public class ScriptablePlatformsList : ScriptableObject
{
    [field: SerializeField] private List<Vector2> _positions; // to show the platforms in the inspector
    
    public MyDictionary<Vector2, PlatformData> Platforms { get; private set; }

    public List<Vector2> GetKeysList() => Platforms.Keys.ToList();
    public List<PlatformData> GetValuesList() => Platforms.Values.ToList();

    private void OnEnable()
    {
        Platforms = new MyDictionary<Vector2, PlatformData>();
        Platforms.ValueChanged += UpdateForInspector;
        UpdateForInspector();
    }

    private void UpdateForInspector()
    {
        _positions = GetKeysList();
    }
}
