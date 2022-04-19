using Common.Db.Entities;

namespace Common.Db;

public class TestDatabase : IDataSource
{

    private const string User = "administrator";
    private const string TestUser = "testuser100";

    public UserInfo GetUserInfo(string username) =>
        username switch {
            "administrator" => new UserInfo {Username = User, Password = User},
            "testuser100" => new UserInfo{ Username = TestUser, Password = TestUser},
            _ => throw new KeyNotFoundException($"User '{username}' not found.")
        };
}