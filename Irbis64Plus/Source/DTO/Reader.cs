namespace Irbis64Plus.DTO;

public record Reader
{
    public string Id { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public string MiddleName { get; set; }
}