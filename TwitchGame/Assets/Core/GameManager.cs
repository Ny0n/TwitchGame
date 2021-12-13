using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MySingleton<GameManager>
{
    public ScriptablePlayersList playersList;
    public ScriptableGameStateVariable gameState;

    public ScriptableTimerVariable roundTimer;
    public ScriptableGameEvent nextLevelEvent;
    public ScriptableGameEvent startRoundEvent;
    public ScriptableGameEvent startActionEvent;

    private bool _playing;
    
    // *********************** UNITY CALLS *********************** //
    
    private void OnEnable() => playersList.Players.ValueChanged += OnPlayersUpdated;
    private void OnDisable() => playersList.Players.ValueChanged -= OnPlayersUpdated;

    protected override void Awake()
    {
        base.Awake();
        gameState.SwitchToState(Enums.GameState.Paused);
    }

    private void Start()
    {
        gameState.SwitchToState(Enums.GameState.WaitingForPlayers);
    }

    private void Update()
    {
        if (gameState.CompareState(Enums.GameState.WaitingForPlayers))
        {
            if (Input.GetKeyDown(KeyCode.Space))
                StartCoroutine(GameLoopCoroutine());
        }
    }
    
    // *********************** GAME MANAGER *********************** //

    private void StartRound()
    {
        if (!gameState.CompareState(Enums.GameState.Playing))
            return;
        roundTimer.StartTimer();
        startRoundEvent.Raise();
    }
    
    private void CheckForGameEnd()
    {
        if (!gameState.CompareState(Enums.GameState.Playing)) return;
        
        int alive = playersList.GetPlayersList().Count(player => player.IsAlive);
        if (alive <= 1)
        {
            _playing = false;
            StartCoroutine(EndingCoroutine());
        }
    }
    
    // *********************** COROUTINES *********************** //

    private IEnumerator GameLoopCoroutine()
    {
        yield return StartCoroutine(StartingCoroutine());
        yield return StartCoroutine(PlayingCoroutine());
    }
    
    private IEnumerator StartingCoroutine()
    {
        gameState.SwitchToState(Enums.GameState.Starting);
        yield return StartCoroutine(gameState.CurrentStateEvent.WaitForSelfAnswers());
    }
    
    private IEnumerator PlayingCoroutine()
    {
        gameState.SwitchToState(Enums.GameState.Playing);
        _playing = true;
        
        CheckForGameEnd();
        while (_playing)
        {
            StartRound();
            yield return StartCoroutine(WaitForTimerEndCoroutine());
            yield return StartCoroutine(startActionEvent.RaiseAndWait());
        }
    }
    
    private IEnumerator EndingCoroutine()
    {
        gameState.SwitchToState(Enums.GameState.Ending);
        yield return StartCoroutine(gameState.CurrentStateEvent.WaitForSelfAnswers());
        nextLevelEvent.Raise();
    }
    
    private IEnumerator WaitForTimerEndCoroutine()
    {
        while (!roundTimer.IsTimerDone()) yield return null;
    }
    
    // *********************** EVENTS *********************** //

    private void OnPlayersUpdated() // C# event
    {
        CheckForGameEnd();
    }
}
