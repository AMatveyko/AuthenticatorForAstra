using Common.Db.Entities;

namespace Common.Db;

public interface IDataSource
{
    User GetUserInfo(string username);
}