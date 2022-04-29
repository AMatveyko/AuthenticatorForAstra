namespace AccountManagerClient;

public record AccountManagerClientRequirements
{
    public string Url { get; init; }
    public string Password { get; init; }
    public string Secret { get; init; }
    public int TimeOut { get; init; }
}