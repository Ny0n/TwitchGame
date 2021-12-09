using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MySingleton<TimerManager>
{
    private List<ScriptableTimerVariable> _timers;
    
    private void Start()
    {
        _timers = new List<ScriptableTimerVariable>();
    }

    private void Update()
    {
        foreach (var timer in _timers.ToList())
        {
            timer.Value -= Time.deltaTime;
            if (timer.IsTimerDone())
            {
                timer.OnTimerEnd();
                _timers.Remove(timer);
            }
        }
    }

    public void AddTimer(ScriptableTimerVariable timer)
    {
        if (_timers.Contains(timer)) return;
        _timers.Add(timer);
    }
    
    public void RemoveTimer(ScriptableTimerVariable timer)
    {
        if (_timers.Contains(timer))
            _timers.Remove(timer);
    }
}
