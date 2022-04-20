using Authorization.Source.Helpers;
using Common.Db;
using Common.DTO;
using UserServiceInterface;
using UserServiceInterface.DTO;

namespace Authorization.Source.Workers;

public sealed class AccountGetter : IAccounting
{

    private readonly IDataSource _db;
    private readonly string _defaultUserGroup;

    public AccountGetter(IDataSource db, string defaultGroup) => (_db, _defaultUserGroup) = (db, defaultGroup);

    public Result<UserData> GetUser(string username) => ResultGetter.Get(() => DoGetUser(username));

    private UserData DoGetUser(string username) {
        var user = _db.GetUserInfo(username);
        return new UserData {
            Name = user.Name,
            Group = string.IsNullOrWhiteSpace(user.Group) ? _defaultUserGroup : user.Group,
            FullName = user.FullName
        };
    }
}