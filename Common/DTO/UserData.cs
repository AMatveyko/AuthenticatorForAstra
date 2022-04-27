namespace Common.DTO;

public record UserData
{
    public string Name { get; init; }
    public string Group { get; init; }
    public string FullName { get; init; }
}