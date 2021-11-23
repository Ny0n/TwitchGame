using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ScriptableObject_GameEvent))]
public class GameEventEditor : Editor
{
    private void OnEnable()
    {
        _event = target as ScriptableObject_GameEvent;
    }

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Raise"))
        {
            _event.Raise();
        }
    }

    private ScriptableObject_GameEvent _event;
}
