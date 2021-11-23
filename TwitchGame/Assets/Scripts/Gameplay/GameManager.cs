using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MySingleton<GameManager>
{
    public override bool DoDestroyOnLoad => false;

    public GenericEvent startRoundEvent;
    public GenericEvent timerEndEvent;

    public Enums.GameState CurrentState { get; private set; }

    private void Awake()
    {
        CurrentState = Enums.GameState.WAITINGFORPLAYERS;
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(2);

        CurrentState = Enums.GameState.PLAYING; // use event
        startRoundEvent.Raise();

        StartCoroutine(StartTimer(30f));
    }

    private IEnumerator StartTimer(float time)
    {
        yield return new WaitForSeconds(time/3f);
        timerEndEvent.Raise();
        yield return new WaitForSeconds(time / 3f);
        timerEndEvent.Raise();
        yield return new WaitForSeconds(time / 3f);
        timerEndEvent.Raise();
        CurrentState = Enums.GameState.WAITINGFORPLAYERS; // use event
    }

}
