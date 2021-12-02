using UnityEngine;

[CreateAssetMenu(menuName = "Variable/Float")]
public class ScriptableFloatVariable : ScriptableObject
{
    private float _value;

    public float Value {
        get
        {
            return _value;
        }
        
        set
        {
            _value = Mathf.Max(value, 0);
        }
    }
}
