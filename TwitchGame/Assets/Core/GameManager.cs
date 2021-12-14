using System.Collections;
using System.Linq;
using UnityEngine;

public class GameManager : MySingleton<GameManager>
{
    [SerializeField] private ScriptablePlayersList _playersList;
    [SerializeField] private ScriptableGameStateVariable _gameState;

    [SerializeField] private ScriptableTimerVariable _roundTimer;
    [SerializeField] private ScriptableGameEvent _nextLevelEvent;
    [SerializeField] private ScriptableGameEvent _startRoundEvent;
    [SerializeField] private ScriptableGameEvent _startActionEvent;

    private Coroutine _startingCoroutine, _playingCoroutine, _endingCoroutine;
    
    // *********************** UNITY CALLS *********************** //
    
    private void OnEnable() => _playersList.Players.ValueChanged += OnPlayersUpdated;
    private void OnDisable() => _playersList.Players.ValueChanged -= OnPlayersUpdated;

    protected override void Awake()
    {
        base.Awake();
        _gameState.SwitchToState(Enums.GameState.Paused);
    }

    private void Start()
    {
        _gameState.SwitchToState(Enums.GameState.WaitingForPlayers);
    }

    private void Update()
    {
        if (_gameState.CompareState(Enums.GameState.WaitingForPlayers))
        {
            if (Input.GetKeyDown(KeyCode.Space))
                _gameState.SwitchToState(Enums.GameState.Starting);
        }
    }
    
    // *********************** GAME MANAGER *********************** //

    private void StartRound()
    {
        _roundTimer.StartTimer();
        _startRoundEvent.Raise();
    }
    
    private void StopRound()
    {
        _roundTimer.StopTimer();
    }
    
    private void CheckForGameEnd()
    {
        if (!_gameState.CompareState(Enums.GameState.Playing)) return;
        
        int alive = _playersList.GetPlayersList().Count(player => player.IsAlive);
        if (alive <= 1)
        {
            StopAllCoroutines();
            _gameState.SwitchToState(Enums.GameState.Ending);
        }
    }
    
    // *********************** COROUTINES *********************** //
    
    private IEnumerator StartingCoroutine()
    {
        yield return StartCoroutine(_gameState.CurrentStateEvent.WaitForSelfAnswers());
        _gameState.SwitchToState(Enums.GameState.Playing);
    }
    
    private IEnumerator PlayingCoroutine()
    {
        CheckForGameEnd();
        while (true) // while the end has not been detected
        {
            StartRound();
            yield return StartCoroutine(WaitForTimerEndCoroutine());
            yield return StartCoroutine(_startActionEvent.RaiseAndWait());
        }
    }
    
    private IEnumerator EndingCoroutine()
    {
        StopRound();
        yield return StartCoroutine(_gameState.CurrentStateEvent.WaitForSelfAnswers());
        _nextLevelEvent.Raise();
    }
    
    private IEnumerator WaitForTimerEndCoroutine()
    {
        while (!_roundTimer.IsTimerDone()) yield return null;
    }

    private void TryToStopCoroutine(Coroutine coroutine)
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
    }
    
    // *********************** EVENTS *********************** //

    private void OnPlayersUpdated() // C# event
    {
        CheckForGameEnd();
    }

    public void OnGameStart(GenericEvent evt) // SO event
    {
        evt.Answer();
        
        TryToStopCoroutine(_startingCoroutine);
        _startingCoroutine = StartCoroutine(StartingCoroutine());
    }

    public void OnGamePlay(GenericEvent evt) // SO event
    {
        evt.Answer();
        
        TryToStopCoroutine(_playingCoroutine);
        _playingCoroutine = StartCoroutine(PlayingCoroutine());
    }
    
    public void OnGameEnd(GenericEvent evt) // SO event
    {
        evt.Answer();
        
        TryToStopCoroutine(_endingCoroutine);
        _endingCoroutine = StartCoroutine(EndingCoroutine());
    }
    
    public void OnGamePause(GenericEvent evt) // SO event
    {
        evt.Answer();
        
    }
    
    public void OnGameWaiting(GenericEvent evt) // SO event
    {
        evt.Answer();
        
    }
    
    public void OnLoadSave() // SO event
    {
        StopAllCoroutines();
        StopRound();
    }
}
