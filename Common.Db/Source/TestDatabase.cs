using Common.Db.Entities;

namespace Common.Db;

public class TestDatabase : IDataSource
{

    private const string User = "administrator";

    public UserInfo GetUserInfo(string username) =>
        username switch {
            "administrator" => new UserInfo {Username = User, Password = User},
            _ => throw new KeyNotFoundException($"User '{username}' not found.")
        };
}