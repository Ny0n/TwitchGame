using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MySingleton<GameManager>
{
    public TimerManager timerManager;
    public ScriptableGameEvent startRoundEvent;
    public ScriptableGameEvent gameEndEvent;

    public Enums.GameState CurrentState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        CurrentState = Enums.GameState.PAUSED;
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(2);
    }

    public void OnMapLoaded() // event
    {
        StartGame();
    }

    private void Update()
    {
        if (CurrentState == Enums.GameState.PLAYING)
        {
            if (PlayersManager.Instance.Players.Count <= 1)
                gameEndEvent.Raise();
        }
    }

    public void StartGame()
    {
        print("StartGame");
        CurrentState = Enums.GameState.WAITINGFORPLAYERS; // use event
        timerManager.StartTimer(5);
    }

    public void OnTimerEnd() // event
    {
        switch (CurrentState)
        {
            case Enums.GameState.WAITINGFORPLAYERS:
                timerManager.StartTimer(10);
                CurrentState = Enums.GameState.PLAYING;
                startRoundEvent.Raise();
                break;
            case Enums.GameState.PLAYING:
                StartCoroutine(WaitForActionEnd());
                break;
        }
    }

    IEnumerator WaitForActionEnd()
    {
        yield return new WaitForSeconds(2);
        timerManager.StartTimer(10);
        startRoundEvent.Raise();
    }

    public void OnGameEnd() // event
    {
        StartGame();
    }
}
