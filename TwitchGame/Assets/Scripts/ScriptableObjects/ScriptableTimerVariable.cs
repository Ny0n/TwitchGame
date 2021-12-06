using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using UnityEngine;
using Debug = UnityEngine.Debug;

[CreateAssetMenu(menuName = "Special/Timer")]
public class ScriptableTimerVariable : ScriptableObject
{
    [SerializeField] [Tooltip("Duration in seconds")]
    private float _duration;
    
    public float Value { get; private set; } = 0f;
    public event System.Action TimerEnd;

    // private static int _delay = 10; // in milliseconds
    // private static bool _isTaskRunning;
    // private static CancellationTokenSource _tokenSource;
    // private static List<ScriptableTimerVariable> _timers;

    private void OnEnable()
    {
        Value = 0f;
        
        // if (!_isTaskRunning)
        // {
        //     _timers = new List<ScriptableTimerVariable>();
        //     _tokenSource = new CancellationTokenSource();
        //     Task.Run(TimerTask);
        //     _isTaskRunning = true;
        // }
    }
    
    private void OnDisable()
    {
        // if (!_tokenSource.IsCancellationRequested)
        // {
        //     _tokenSource.Cancel();
        //     _isTaskRunning = false;
        //     Debug.Log("CANCEL");
        // }
    
        Value = 0f;
    }

    public bool IsTimerDone() => Value <= 0;

    public void StartTimer()
    {
        // Value = _duration;
        // _timers.Remove(this);
        // _timers.Add(this);
        // Timer timer = new Timer();
    }
    
    // private static async void TimerTask()
    // {
    //     Debug.Log("TimerTask start (X+)");
    //     while (!_tokenSource.IsCancellationRequested)
    //     {
    //         Debug.Log("yup");
    //         await Task.Delay(_delay);
    //         foreach (var timer in _timers.ToList())
    //         {
    //             timer.Value -= _delay/1000f;
    //             if (timer.Value <= 0)
    //             {
    //                 timer.TimerEnd?.Invoke();
    //                 _timers.Remove(timer);
    //             }
    //         }
    //
    //     }
    //     Debug.Log("TimerTask end (X-)");
    // }
}
