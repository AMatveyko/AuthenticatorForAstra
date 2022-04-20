using Common.Db;
using Common.Db.Entities;
using Irbis64Plus;

namespace AccountManagerData.Databases;

public sealed class IrbisDatabase : IDataSource
{

    private readonly DatabaseConnectionInfo _connectionInfo;

    public IrbisDatabase(DatabaseConnectionInfo connectionInfo) =>
        _connectionInfo = connectionInfo;
    
    public User GetUserInfo(string username) {
        using var client = GetClient();
        var reader = client.GetReaderById(username);
        return new User {
            Name = reader.Id,
            Password = reader.Password,
            FullName = $"{reader.FirstName} {reader.SecondName} {reader.MiddleName}"
        };
    }

    private IrbisClient GetClient() =>
        new ( _connectionInfo.Host,
              _connectionInfo.Port,
              _connectionInfo.Username,
              _connectionInfo.Password);
}