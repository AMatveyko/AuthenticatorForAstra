namespace Common.Db.Entities;

public record UserInfo
{
    public string Username { get; set; }
    public string Password { get; set; }
}