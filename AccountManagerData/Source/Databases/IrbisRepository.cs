using Common.Db;
using Common.Db.Entities;
using Irbis64Plus;

namespace AccountManagerData.Databases;

public sealed class IrbisRepository : IDataSource
{

    private readonly DatabaseConnectionInfo _connectionInfo;

    public IrbisRepository(DatabaseConnectionInfo connectionInfo) =>
        _connectionInfo = connectionInfo;
    
    public User GetUserInfo(string username) {
        var client = GetClient();
        var reader = client.GetReaderById(username);
        return new User {
            Name = reader.Id,
            Password = reader.Password,
            FullName = $"{reader.SecondName} {reader.FirstName} {reader.MiddleName}"
        };
    }

    private IrbisClient GetClient() =>
        new ( _connectionInfo.Host,
              _connectionInfo.Port,
              _connectionInfo.Login,
              _connectionInfo.Password);
}