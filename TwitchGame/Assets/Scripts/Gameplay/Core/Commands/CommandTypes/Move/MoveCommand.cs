using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Command/Move", fileName = "Move Command")]
public class MoveCommand : Command
{
    private enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    [SerializeField] private Direction _direction;
    
    public override void Execute(string playerName, ScriptablePlayersList playersList)
    {
        if (!GameManager.Instance.CompareState(Enums.GameState.Playing)) return;
        if (!playersList.IsPlayerRegistered(playerName)) return;

        Player player = playersList.Players[playerName];
        if (!player.IsAlive) return;

        PlayerMove playerMove = player.PlayerObject.GetComponent<PlayerMove>();
        switch (_direction)
        {
            case Direction.Up:
                playerMove.GoUp();
                break;
            case Direction.Down:
                playerMove.GoDown();
                break;
            case Direction.Left:
                playerMove.GoLeft();
                break;
            case Direction.Right:
                playerMove.GoRight();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
