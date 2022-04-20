namespace Common.Db.Entities;

public record User
{
    public string Name { get; set; }
    public string Password { get; set; }
    public string Group { get; set; }
    public string FullName { get; set; }
}