using Common.Db.Entities;

namespace Common.Db;

public interface IDataSource
{
    UserInfo GetUserInfo(string username);
}