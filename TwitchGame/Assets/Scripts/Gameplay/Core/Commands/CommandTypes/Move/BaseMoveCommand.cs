public class BaseMoveCommand : Command
{
    protected Player _player;
    protected PlayerMove _playerMove;

    protected bool IsValid { get; private set; } // can sub-classes execute?

    public override void Execute(string playerName, ScriptablePlayersList playersList)
    {
        IsValid = false;
        if (!GameManager.Instance.CompareState(Enums.GameState.Playing)) return;
        if (!playersList.IsPlayerRegistered(playerName)) return;

        _player = playersList.Players[playerName];
        if (!_player.IsAlive) return;

        _playerMove = _player.PlayerObject.GetComponent<PlayerMove>();
        IsValid = true;
    }
}
