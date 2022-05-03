namespace Common.Cache;

public class Cache<TK,TV> : ICache<TK,TV> where TK : notnull
{
    private static readonly object LockFlag = new();
    private static Dictionary<TK, TV> _cache = new();

    public TV GetValue(TK key, Func<TK,TV> valueGetter) {

        lock (LockFlag) {
            if (_cache.ContainsKey(key) == false) {
                _cache[key] = valueGetter(key);
            }
        }
        
        return _cache[key];
    }
}