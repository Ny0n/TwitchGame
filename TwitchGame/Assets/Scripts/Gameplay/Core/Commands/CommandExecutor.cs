using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandExecutor : MonoBehaviour
{
    public ScriptablePlayerEvent playerEvent;

    // ******************* Execution ******************* //

    public void Execute(Command command)
    {
        print("Execute " + command.ToString());
        switch (command.type)
        {
            case Enums.CommandType.REGISTER:
            case Enums.CommandType.UNREGISTER:
                ConnexionCommand(command);
                break;

            default: // TODO only default for now
                MoveCommand(command);
                break;
        }
    }

    // ******************* Commands ******************* //

    private void ConnexionCommand(Command command)
    {
        switch (command.type)
        {
            case Enums.CommandType.REGISTER:
                playerEvent.SetAndRaise(command.playerName, Enums.PlayerEventAction.ADD);
                break;

            case Enums.CommandType.UNREGISTER:
                playerEvent.SetAndRaise(command.playerName, Enums.PlayerEventAction.REMOVE);
                break;
        }
    }

    private void MoveCommand(Command command)
    {
        Player player = PlayersManager.Instance.Players[command.playerName];
        if (!player.IsAlive) return;

        PlayerMove playerMove = player.PlayerObject.GetComponent<PlayerMove>();
        switch (command.type)
        {
            case Enums.CommandType.STAY:
                break;

            case Enums.CommandType.UP:
                playerMove.GoUp();
                break;

            case Enums.CommandType.DOWN:
                playerMove.GoDown();
                break;

            case Enums.CommandType.LEFT:
                playerMove.GoLeft();
                break;

            case Enums.CommandType.RIGHT:
                playerMove.GoRight();
                break;
        }
    }

    private void ShootCommand()
    {
        // Shoot behaviour...
    }
    
    private void HitCommand()
    {
        // Hit behaviour...
    }
}
