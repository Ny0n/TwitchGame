public readonly struct CommandObject
{
    public string PlayerName { get; }
    private Command CommandRef { get; }

    public CommandObject(string playerName, Command commandRef)
    {
        PlayerName = playerName;
        CommandRef = commandRef;
    }

    public void Execute(ScriptablePlayersList playersList)
    {
        if (CommandRef != null)
            CommandRef.Execute(PlayerName, playersList);
    }

    public override string ToString() => $"{{ PlayerName = \"{PlayerName}\", Command = \"{CommandRef}\" }}";
}
