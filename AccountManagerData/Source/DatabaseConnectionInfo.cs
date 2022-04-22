namespace AccountManagerData;

public record DatabaseConnectionInfo
{
    public string Host { get; set; }
    public string Port { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
}