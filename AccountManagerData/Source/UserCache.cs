using Common.Cache;
using Common.Db.Entities;

namespace AccountManagerData;

public sealed class UserCache : CacheWithTimer<string, User>
{
    public UserCache(TimeSpan period): base(period) {}
}