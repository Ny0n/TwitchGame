using UnityEngine;

[CreateAssetMenu(menuName = "Special/Cooldown")]
public class ScriptableCooldown : ScriptableObject
{
    [SerializeField] [Tooltip("Length in ms")]
    private float _cooldownDuration;

    public void StartCooldown()
    {
        _nextCooldownDate = Time.time + _cooldownDuration;
    }

    public bool IsCooldownDone()
    {
        return Time.time > _nextCooldownDate;
    }
    
    private float _nextCooldownDate;
}
