using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersManager : MySingleton<PlayersManager>
{
    public override bool DoDestroyOnLoad => false;

    public Dictionary<string, Player> Players { get; private set; }

    [SerializeField]
    private GameObject playerPrefab;

    private int currentPlayerNumber = 1;

    private void Awake()
    {
        Players = new Dictionary<string, Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsPlayerRegistered(string name)
    {
        return Players.ContainsKey(name);
    }

    private GameObject CreateNewPlayerObject()
    {
        return Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
    }

    public void OnRecievePlayerEvent(ScriptablePlayerEvent playerEvent) // event
    {
        if (GameManager.Instance.CurrentState != Enums.GameState.WAITINGFORPLAYERS)
            return;

        switch (playerEvent.action)
        {
            case ScriptablePlayerEvent.Action.ADD:
                if (IsPlayerRegistered(playerEvent.name))
                    return;

                Player player = new Player(CreateNewPlayerObject(), playerEvent.name, currentPlayerNumber);
                Players.Add(playerEvent.name, player);
                currentPlayerNumber++;
                break;
            
            case ScriptablePlayerEvent.Action.REMOVE:
                if (!IsPlayerRegistered(playerEvent.name))
                    return;

                Players[playerEvent.name].Remove();
                Players.Remove(playerEvent.name);
                break;
        }
    }
}
