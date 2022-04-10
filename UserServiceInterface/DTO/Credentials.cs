namespace UserServiceInterface.DTO;

public record Credentials
{
    public string Username { get; set; }
    public string PasswordSignature { get; set; }
    public string TimeStamp { get; set; }
}