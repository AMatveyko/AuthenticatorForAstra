namespace Common.Cache;

public class CacheWithTimer<TK, TV> : ICache<TK, TV>, ICache where TK : notnull
{
    private static readonly object LockFlag = new();
    private readonly TimeSpan _period;
    private static readonly Dictionary<TK, TV> Cache = new();
    private static readonly Dictionary<TK, DateTime> Times = new();

    public CacheWithTimer(TimeSpan period) => _period = period;


    public TV GetValue(TK key, Func<TK, TV> valueGetter) {
        
        Lock( () => RefreshValueIfNeed(key, valueGetter) );
        
        return Cache[key];
    }

    private void RefreshValueIfNeed(TK key, Func<TK, TV> valueGetter) {
        if (Cache.ContainsKey(key) == false || IsStale(key) ) {
            Cache[key] = valueGetter(key);
            Times[key] = DateTime.UtcNow;
        }
    }

    public void Clear() => Lock(RemoveStaleCache);

    private void RemoveStaleCache() {
        foreach (var key in Cache.Keys.Where( IsStale ) ) {
            Cache.Remove(key);
        }
    }
    
    private static void Lock(Action action) {
        lock (LockFlag) {
            action();
        }
    }

    private bool IsStale(TK key) => Times[key].AddSeconds(_period.Seconds) < DateTime.UtcNow;
}