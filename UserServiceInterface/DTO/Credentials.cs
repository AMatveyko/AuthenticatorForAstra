namespace UserServiceInterface.DTO;

public record Credentials
{
    public string Username { get; init; }
    public string PasswordSignature { get; init; }
    public string TimeStamp { get; init; }
}