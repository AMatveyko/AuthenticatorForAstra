using Common.Cache;

namespace AccountManager.BackgroundServices;

public sealed class CacheHandler : IHostedService
{

    private readonly ICache _cache;
    private readonly TimeSpan _period;
    private Timer _timer;

    public CacheHandler(ICache cache, TimeSpan time) =>
        (_cache, _period) = (cache, time);
    
    public Task StartAsync(CancellationToken cancellationToken) {
        _timer = new Timer(DoWord, null, TimeSpan.FromMinutes(5), _period);
        return Task.CompletedTask;
    }

    private void DoWord(object state) => _cache.Clear();
    
    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}