namespace AccountManagerData;

public record DatabaseConnectionInfo
{
    public string Host { get; init; }
    public string Port { get; init; }
    public string Login { get; init; }
    public string Password { get; init; }
}