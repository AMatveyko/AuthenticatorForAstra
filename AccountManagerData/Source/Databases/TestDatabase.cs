using Common.Db;
using Common.Db.Entities;

namespace AccountManagerData.Databases;

public class TestDatabase : IDataSource
{

    private const string User = "administrator";
    private const string TestUser = "testuser100";
    private const string TestUser1 = "testuser101";
    private const string TestUser2 = "testuser102";
    private const string TestUser4 = "testuser104";
    private const string TestUser5 = "testuser105";
    private const string TestUser6 = "testuser106";
    private const string TestUser7 = "testuser107";
    private const string TestUser8 = "testuser108";
    private const string TestUser9 = "testuser109";

    public User GetUserInfo(string username) =>
        username switch {
            "administrator" => new User {Name = User, Password = User, Group = "Admins", FullName = "Admin"},
            "testuser100" => new User{ Name = TestUser, Password = TestUser, Group = "newUsers", FullName = "User Userovich Userskiy"},
            "testuser101" => new User{ Name = TestUser1, Password = TestUser1, Group = "newUsers2", FullName = "User Userovich Userskiy"},
            "testuser102" => new User{ Name = TestUser2, Password = TestUser2, Group = "newUsers3", FullName = "User Userovich Userskiy"},
            "testuser104" => new User{ Name = TestUser4, Password = TestUser4, Group = "newUsers4", FullName = "User Userovich Userskiy"},
            "testuser105" => new User{ Name = TestUser5, Password = TestUser5, Group = "newUsers5", FullName = "User Userovich Userskiy"},
            "testuser106" => new User{ Name = TestUser6, Password = TestUser6, Group = "newUsers6", FullName = "User Userovich Userskiy"},
            "testuser107" => new User{ Name = TestUser7, Password = TestUser7, Group = "newUsers7", FullName = "User Userovich Userskiy"},
            "testuser108" => new User{ Name = TestUser8, Password = TestUser8, Group = "newUsers8", FullName = "User Userovich Userskiy"},
            "testuser109" => new User{ Name = TestUser9, Password = TestUser9, Group = "newUsers9", FullName = "User Userovich Userskiy"},
            _ => throw new KeyNotFoundException($"User '{username}' not found.")
        };
}