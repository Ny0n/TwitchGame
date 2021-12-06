using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Timers;
using UnityEngine;
using Debug = UnityEngine.Debug;

[CreateAssetMenu(menuName = "Special/Timer")]
public class ScriptableTimerVariable : ScriptableObject
{
    [SerializeField] [Tooltip("Duration in seconds")]
    private float _duration;
    
    public float Value { get; set; }
    public event System.Action TimerEnd;

    private void OnEnable() => Value = 0f;

    public bool IsTimerDone() => Value <= 0;

    public void StartTimer()
    {
        var timerManager = TimerManager.Instance;
        if (timerManager != null)
        {
            Value = _duration;
            timerManager.AddTimer(this);
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
