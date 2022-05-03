namespace Common.Cache;

public interface ICache<TK, TV>
{
    public TV GetValue(TK key, Func<TK,TV> valueGetter);
}

public interface ICache
{
    public void Clear();
}