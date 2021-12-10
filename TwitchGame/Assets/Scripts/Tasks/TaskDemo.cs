using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class TaskDemo : MonoBehaviour
{
    /*
     *  Script inputs usage:
     *      E => Job
     *      A => AsyncJob
     *      Z => AsyncJob2
     *      B => AsyncBigJob
     *      B+LeftShift => AsyncBigJobCustom
     *      (B)+LeftAlt => () + Auto Cancel after 1 sec
     *      N => Cancel B
     *      T => ThreadJob
     */
    
    [SerializeField] private int _counter = 10;
    [SerializeField] private int _delay = 3000;
    [SerializeField] private int _wait = 200;
    [SerializeField] private int _step = 10;
    
    private CancellationTokenSource _tokenSource;

    private static void Print(string state)
    {
        var st = new StackTrace();
        var sf = st.GetFrame(1);
        Debug.Log($"[func: \"{sf.GetMethod().Name}\"] [thread: \"{Thread.CurrentThread.Name}\"] {state}");
    }

    // Update is called once per frame
    void Update()
    {
        // blocking
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartJob();
        }
        
        // async
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartAsyncJob();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            StartAsyncJob2();
        }
        
        // cancellable
        if (Input.GetKeyDown(KeyCode.B))
        {
            _tokenSource = new CancellationTokenSource();
            CancellationToken token = _tokenSource.Token;
            
            if (Input.GetKey(KeyCode.LeftAlt))
                _tokenSource.CancelAfter(1000);
            
            if (Input.GetKey(KeyCode.LeftShift))
                StartAsyncBigJobCustom(token);
            else
                StartAsyncBigJob(token);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            _tokenSource.Cancel();
        }
        
        // thread
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartThreadJob();
        }
    }

    #region Job

    void StartJob()
    {
        Job();
    }

    void Job()
    {
        Print("start");
        
        int counter = _counter;
        while (counter > 0)
        {
            Thread.Sleep(_wait);
            counter--;
            Print("++");
        }
        
        Print("done");
    }

    #endregion

    #region AsyncJob

    void StartAsyncJob()
    {
        JobAsync();
    }
    
    async void JobAsync()
    {
        Print("start");
        
        int counter = _counter;
        while (counter > 0)
        {
            await Task.Delay(_wait);
            counter--;
            Print("++");
        }
        
        Print("done");
    }

    #endregion
    
    #region AsyncJob2 (with return)

    async void StartAsyncJob2()
    {
        Print("start");
        
        string r = await Job2Async();
        
        Print($"end with result: {r}");
    }
    
    async Task<string> Job2Async()
    {
        Print("start");
        
        await Task.Delay(_delay);
        
        Print("done");

        return "<hey je suis le return>";
    }

    #endregion

    #region AsyncBigJob (Cancellable)

    void StartAsyncBigJob(CancellationToken token)
    {
        BigJobAsync(token);
    }

    async void BigJobAsync(CancellationToken token)
    {
        Print("start");

        try
        {
            await Task.Delay(_delay, token); // with token
        }
        catch (TaskCanceledException e)
        {
            Print($"cancelled: {e}");
        }
        
        Print("done");
    }

    #endregion

    #region AsyncBigJobCustom (Cancellable)

    async void StartAsyncBigJobCustom(CancellationToken token)
    {
        try
        {
            await BigJobCustomAsync(token);
        }
        catch (TaskCanceledException e)
        {
            Print($"cancelled: {e}");
        }
    }
    
    async Task BigJobCustomAsync(CancellationToken token)
    {
        Print("start");

        int counter = _counter;
        int delay = _delay;
        int step = _step;

        try
        {
            while (counter < delay)
            {
                if (token.IsCancellationRequested)
                {
                    throw new TaskCanceledException();
                }
                await Task.Delay(step); // no token
                counter += step;
                Print("++");
            }
        }
        catch (TaskCanceledException e)
        {
            throw e;
        }
        finally
        {
            Print("finally");
        }
    }

    #endregion

    #region ThreadJob (SlowFunc)

    async void StartThreadJob()
    {
        Print("start");
        
        int r = await Task.Run(SlowFunc);
        
        Print($"end with result: {r}");
    }

    int SlowFunc()
    {
        Print("start");
        
        int counter = _counter;
        while (counter > 0)
        {
            Thread.Sleep(_wait);
            counter--;
            Print("++");
        }
        
        Print("end");

        return 50;
    }

    #endregion
}
