using UnityEngine;

[CreateAssetMenu(menuName = "Command/Game/Empty", fileName = "Game _EmptyCommand_")]
public class EmptyGameCommand : GameCommand
{
    [SerializeField] private ScriptableGameStateVariable _gameState;
    
    public override void Execute(string playerName, ScriptablePlayersList playersList)
    {
        // use one charge of the tile under the player
        if (!_gameState.CompareState(Enums.GameState.Playing)) return;
        
        var player = playersList.TryGetPlayer(playerName);
        if (!(player is Player)) return;
        if (!player.IsAlive) return;

        PlayerCollision playerCollision = player.PlayerObject.GetComponent<PlayerCollision>();
        PlatformData platformData = playerCollision.Platform.GetComponent<PlatformData>();
        platformData.GoToNextState();
    }
}
