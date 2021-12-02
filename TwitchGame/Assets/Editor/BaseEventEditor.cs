using UnityEditor;
using UnityEngine;

public abstract class BaseEventEditor : Editor
{
    private GenericEvent _event;

    private void OnEnable()
    {
        _event = target as GenericEvent;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Raise"))
        {
            _event.Raise();
        }
    }
}
