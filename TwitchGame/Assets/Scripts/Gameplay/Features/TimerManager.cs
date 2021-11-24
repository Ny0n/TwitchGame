using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public ScriptableFloatVariable Timer;

    public ScriptableGameEvent TimerEnd;

    bool timerActive = false;

    // Start is called before the first frame update
    void Start()
    {
        Timer.Value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerActive)
        {
            Timer.Value -= Time.deltaTime;
            if (Timer.Value <= 0)
            {
                timerActive = false;
                TimerEnd.Raise();
            }
        }
    }

    public void StartTimer(float time)
    {
        Timer.Value = time;
        timerActive = true;
    }
}
