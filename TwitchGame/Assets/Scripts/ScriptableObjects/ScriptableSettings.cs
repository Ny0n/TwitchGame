using UnityEngine;

[CreateAssetMenu(menuName = "Special/Settings")]
public class ScriptableSettings : ScriptableObject
{
    [field: Header("Default values")]
    [field: SerializeField] public int MaxNumberOfPlayers { get; set; } = 30;
    [field: SerializeField] public float RoundDuration { get; set; } = 10f;
}
