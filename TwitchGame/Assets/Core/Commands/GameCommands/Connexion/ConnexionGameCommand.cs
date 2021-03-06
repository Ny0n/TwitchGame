using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Command/Game/Connexion", fileName = "Game _ConnexionCommand_")]
public class ConnexionGameCommand : GameCommand
{
    private enum Type
    {
        Register,
        Unregister
    }

    [SerializeField] private Type _connexionType;
    [SerializeField] private ScriptablePlayerEvent _playerEvent;

    public override void Execute(string playerName, ScriptablePlayersList playersList)
    {
        Enums.PlayerEventAction action = _connexionType switch
        {
            Type.Register => Enums.PlayerEventAction.Add,
            Type.Unregister => Enums.PlayerEventAction.Remove,
            _ => throw new ArgumentOutOfRangeException()
        };
        _playerEvent.SetAndRaise(playerName, action);
    }
}
