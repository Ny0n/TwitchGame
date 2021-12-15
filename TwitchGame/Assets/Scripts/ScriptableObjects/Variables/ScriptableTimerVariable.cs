using UnityEngine;
using Debug = UnityEngine.Debug;

[CreateAssetMenu(menuName = "Special/Timer")]
public class ScriptableTimerVariable : ScriptableObject
{
    [SerializeField] private ScriptableSettings _settings;
    
    public float Value { get; set; }
    public event System.Action TimerEnd;

    private void OnEnable() => Value = 0f;

    public bool IsTimerDone() => Value <= 0;

    public void StartTimer()
    {
        var timerManager = TimerManager.Instance;
        if (timerManager != null)
        {
            Value = _settings.RoundDuration;
            timerManager.AddTimer(this);
        }
        else
        {
            Debug.LogError("To use timers, please add an instance of TimerManager to the scene (temp)");
        }
    }
    
    public void StopTimer()
    {
        var timerManager = TimerManager.Instance;
        if (timerManager != null)
        {
            timerManager.RemoveTimer(this);
            Value = 0;
        }
        else
        {
            Debug.LogError("To use timers, please add an instance of TimerManager to the scene (temp)");
        }
    }

    public void OnTimerEnd()
    {
        TimerEnd?.Invoke();
    }
}
