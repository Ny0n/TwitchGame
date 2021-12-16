using UnityEngine;

[CreateAssetMenu(menuName = "Special/Cooldown")]
public class ScriptableCooldown : ScriptableObject
{
    [SerializeField] [Tooltip("Length in s")]
    private float _cooldownDuration;
    
    private float _nextCooldownDate;

    private void OnEnable()
    {
        _nextCooldownDate = Time.time;
    }

    public void StartCooldown()
    {
        _nextCooldownDate = Time.time + _cooldownDuration;
    }

    public bool IsCooldownDone()
    {
        return Time.time > _nextCooldownDate;
    }
}
