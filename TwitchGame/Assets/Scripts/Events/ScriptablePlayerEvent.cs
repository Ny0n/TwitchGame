using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Event/Player")]
public class ScriptablePlayerEvent : GenericEvent
{
    public enum Action
    {
        ADD,
        REMOVE,
    }

    public Action action;
    public new string name;
}
