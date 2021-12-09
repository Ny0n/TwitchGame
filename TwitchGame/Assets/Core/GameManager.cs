using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MySingleton<GameManager>
{
    public ScriptablePlayersList playersList;

    public ScriptableTimerVariable roundTimer;
    public ScriptableGameEvent startRoundEvent;
    public ScriptableGameEvent startActionEvent;
    
    [Header("State events")]
    public ScriptableGameEvent gameStartEvent;
    public ScriptableGameEvent gameEndEvent;

    private ScriptableGameEvent _currentStateEvent;
    private bool _playing;

    // *********************** GAME STATE *********************** //
    
    public Enums.GameState CurrentState { get; private set; }

    public bool CompareState(Enums.GameState state) => CurrentState == state;

    private void SwitchToState(Enums.GameState state)
    {
        CurrentState = state;
        switch (CurrentState)
        {
            case Enums.GameState.Paused:
                _currentStateEvent = null;
                break;
            case Enums.GameState.WaitingForPlayers:
                _currentStateEvent = null;
                break;
            case Enums.GameState.Starting:
                _currentStateEvent = gameStartEvent;
                break;
            case Enums.GameState.Playing:
                _playing = true;
                _currentStateEvent = null;
                break;
            case Enums.GameState.Ending:
                _currentStateEvent = gameEndEvent;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }

        if (_currentStateEvent is ScriptableGameEvent)
            _currentStateEvent.Raise();
    }
    
    // *********************** UNITY CALLS *********************** //
    
    private void OnEnable() => playersList.Players.ValueChanged += OnPlayersUpdated;
    private void OnDisable() => playersList.Players.ValueChanged -= OnPlayersUpdated;

    protected override void Awake()
    {
        base.Awake();
        SwitchToState(Enums.GameState.Paused);
    }

    private void Start()
    {
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
        roundTimer.StartTimer();
        startRoundEvent.Raise();
    }
    
    private void CheckForGameEnd()
    {
        int alive = playersList.GetPlayersList().Count(player => player.IsAlive);
        if (alive <= 1)
        {
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
        SwitchToState(Enums.GameState.Starting);
        yield return StartCoroutine(_currentStateEvent.WaitForSelfAnswers());
    }
    
    private IEnumerator PlayingCoroutine()
    {
        SwitchToState(Enums.GameState.Playing);
        while (_playing)
        {
            StartRound();
            yield return StartCoroutine(WaitForTimerEndCoroutine());
            yield return StartCoroutine(startActionEvent.RaiseAndWait());
        }
    }
    
    private IEnumerator EndingCoroutine()
    {
        SwitchToState(Enums.GameState.Ending);
        yield return StartCoroutine(_currentStateEvent.WaitForSelfAnswers());
        SceneManager.LoadScene("Scenes/WinnerScene");
    }
    
    private IEnumerator WaitForTimerEndCoroutine()
    {
        while (!roundTimer.IsTimerDone()) yield return null;
    }
    
    // *********************** EVENTS *********************** //

    private void OnPlayersUpdated() // C# event
    {
        
    }
    
}
