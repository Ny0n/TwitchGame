using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MySingleton<GameManager>
{
    public ScriptablePlayersList PlayersList;

    public TimerManager timerManager;
    public ScriptableGameEvent startRoundEvent;
    public ScriptableGameEvent gameStartEvent;
    public ScriptableGameEvent gameEndEvent;

    public Enums.GameState CurrentState { get; private set; }

    public bool CompareState(Enums.GameState state) => CurrentState == state;

    protected override void Awake()
    {
        base.Awake();
        CurrentState = Enums.GameState.PAUSED;
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(2);
        StartWaitingForPlayers();
    }

    void StartWaitingForPlayers()
    {
        CurrentState = Enums.GameState.WAITINGFORPLAYERS;
    }

    float updateEvery = 1f;
    private void Update()
    {
        switch (CurrentState)
        {
            case Enums.GameState.WAITINGFORPLAYERS:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    CurrentState = Enums.GameState.STARTINGGAME;
                    gameStartEvent.Raise();
                }
                break;
        }

        //updateEvery -= Time.deltaTime;
        //if (updateEvery <= 0)
        //{
        //    updateEvery = 1f;
        //    if (CurrentState == Enums.GameState.PLAYING)
        //            CheckForGameEnd();
        //}
    }

    public void OnMapLoaded() // event
    {
        CurrentState = Enums.GameState.PLAYING;
        StartRound();
    }

    public void OnTimerEnd() // event
    {
        switch (CurrentState)
        {
            case Enums.GameState.PLAYING:
                StartCoroutine(RestartRound());
                break;
        }
    }

    IEnumerator RestartRound()
    {
        yield return new WaitForSeconds(2);
        StartRound();
    }

    private void StartRound()
    {
        if (CurrentState != Enums.GameState.PLAYING)
            return;
        timerManager.StartTimer(10);
        startRoundEvent.Raise();
    }

    private void CheckForGameEnd()
    {
        int alive = 0;
        foreach (Player player in PlayersList.GetPlayersList())
        {
            if (player.IsAlive)
                alive++;
        }

        if (alive <= 1)
        {
            CurrentState = Enums.GameState.ENDINGGAME;
            gameEndEvent.Raise();
        }
    }

    public void OnGameEnd() // event
    {
        StartWaitingForPlayers();
    }
}
