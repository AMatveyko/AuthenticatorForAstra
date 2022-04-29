using Common.Cache;
using Common.Db;
using Common.Db.Entities;

namespace AccountManagerData.Databases;

public sealed class CachedIrbisRepository : IDataSource
{

    private readonly ICache<string,User> _cache;
    private readonly IDataSource _realDataSource;

    public CachedIrbisRepository(IDataSource dataSource, ICache<string, User> cache) =>
        (_cache, _realDataSource) = (cache, dataSource);

    public User GetUserInfo(string username) =>
        _cache.GetValue(username, un => _realDataSource.GetUserInfo(un));
}