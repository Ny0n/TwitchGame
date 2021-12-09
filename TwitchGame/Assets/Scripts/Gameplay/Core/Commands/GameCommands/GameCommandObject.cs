public readonly struct GameCommandObject
{
    public string PlayerName { get; }
    private GameCommand GameCommand { get; }

    public GameCommandObject(string playerName, GameCommand gameCommand)
    {
        PlayerName = playerName;
        GameCommand = gameCommand;
    }

    public void Execute(ScriptablePlayersList playersList)
    {
        if (GameCommand != null)
            GameCommand.Execute(PlayerName, playersList);
    }

    public override string ToString() => $"{{ PlayerName = \"{PlayerName}\", Command = \"{GameCommand}\" }}";
}
