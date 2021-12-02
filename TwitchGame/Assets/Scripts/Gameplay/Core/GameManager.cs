using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class GameManager : MySingleton<GameManager>
{
    public ScriptablePlayersList playersList;

    public TimerManager timerManager;
    public ScriptableGameEvent startRoundEvent;
    public ScriptableGameEvent gameStartEvent;
    public ScriptableGameEvent gameEndEvent;

    // *********************** GAME STATE *********************** //
    
    public Enums.GameState CurrentState { get; private set; }

    public bool CompareState(Enums.GameState state) => CurrentState == state;

    private void SwitchToState(Enums.GameState state)
    {
        CurrentState = state;
        switch (CurrentState)
        {
            case Enums.GameState.Paused:
                break;
            case Enums.GameState.WaitingForPlayers:
                break;
            case Enums.GameState.Starting:
                gameStartEvent.Raise();
                break;
            case Enums.GameState.Playing:
                StartRound();
                break;
            case Enums.GameState.Ending:
                gameEndEvent.Raise();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }
    
    // *********************** UNITY CALLS *********************** //

    protected override void Awake()
    {
        base.Awake();
        SwitchToState(Enums.GameState.Paused);
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(2);
        SwitchToState(Enums.GameState.WaitingForPlayers);
    }

    private void Update()
    {
        if (CompareState(Enums.GameState.WaitingForPlayers))
        {
            if (Input.GetKeyDown(KeyCode.Space))
                StartCoroutine(GameLoopCoroutine());
        }
    }
    
    // *********************** GAME MANAGER *********************** //

    private void StartRound()
    {
        if (!CompareState(Enums.GameState.Playing))
            return;
        timerManager.StartTimer(10);
        startRoundEvent.Raise();
    }
    
    private bool CheckForGameEnd()
    {
        int alive = playersList.GetPlayersList().Count(player => player.IsAlive);
        return alive <= 1;
    }
    
    // *********************** COROUTINES *********************** //

    private IEnumerator GameLoopCoroutine()
    {
        SwitchToState(Enums.GameState.Starting);
        yield return WaitCoroutine(2f);
        SwitchToState(Enums.GameState.Playing);
        // loop timer until game end
        yield return StartCoroutine(WaitCoroutine(2f));
        SwitchToState(Enums.GameState.Ending);
        yield return StartCoroutine(WaitCoroutine(2f));
        SwitchToState(Enums.GameState.WaitingForPlayers);
    }
    
    private IEnumerator WaitCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
    }
    
    private IEnumerator RestartRound()
    {
        yield return new WaitForSeconds(2);
        StartRound();
    }
    
    // *********************** EVENTS *********************** //
    
    public void OnMapLoaded() // event
    {
        SwitchToState(Enums.GameState.Playing);
    }

    public void OnTimerEnd() // event
    {
        switch (CurrentState)
        {
            case Enums.GameState.Playing:
                StartCoroutine(RestartRound());
                break;
        }
    }
}
