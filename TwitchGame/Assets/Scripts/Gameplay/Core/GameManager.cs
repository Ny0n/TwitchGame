using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class GameManager : MySingleton<GameManager>
{
    public ScriptablePlayersList playersList;

    public ScriptableTimerVariable roundTimer;
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
        print("StartRound");
        roundTimer.StartTimer();
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
        yield return StartCoroutine(StartingCoroutine());
        yield return StartCoroutine(PlayingCoroutine());
        yield return StartCoroutine(EndingCoroutine());
        SwitchToState(Enums.GameState.WaitingForPlayers);
    }
    
    private IEnumerator StartingCoroutine()
    {
        SwitchToState(Enums.GameState.Starting);
        yield return StartCoroutine(WaitCoroutine(2f));
    }
    
    private IEnumerator PlayingCoroutine()
    {
        SwitchToState(Enums.GameState.Playing);
        while (!CheckForGameEnd())
        {
            yield return StartCoroutine(StartRoundCoroutine());
            yield return StartCoroutine(WaitForTimerEndCoroutine());
            yield return StartCoroutine(WaitCoroutine(4f));
        }
        StartCoroutine(EndingCoroutine());
    }
    
    private IEnumerator EndingCoroutine()
    {
        SwitchToState(Enums.GameState.Ending);
        yield return StartCoroutine(WaitCoroutine(2f));
    }
    
    private IEnumerator WaitCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
    }
    
    private IEnumerator WaitForTimerEndCoroutine()
    {
        while (roundTimer.IsTimerDone()) yield return null;
    }
    
    private IEnumerator StartRoundCoroutine()
    {
        StartRound();
        yield return new WaitForSeconds(2);
    }
    
    // *********************** EVENTS *********************** //
    
    public void OnMapLoaded() // event
    {
        // SwitchToState(Enums.GameState.Playing);
    }
}
