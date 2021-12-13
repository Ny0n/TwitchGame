using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Command/Game/Move", fileName = "Game _MoveCommand_")]
public class MoveGameCommand : GameCommand
{
    private enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    [SerializeField] private Direction _direction;
    
    public ScriptableGameStateVariable gameState;
    
    public override void Execute(string playerName, ScriptablePlayersList playersList)
    {
        if (!gameState.CompareState(Enums.GameState.Playing)) return;
        
        Player player = playersList.TryGetPlayer(playerName);
        if (player == null) return;
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
