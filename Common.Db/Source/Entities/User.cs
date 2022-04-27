namespace Common.Db.Entities;

public record User
{
    public string Name { get; init; }
    public string Password { get; init; }
    public string Group { get; init; }
    public string FullName { get; init; }
}