using GoalFinder.Application.Shared.Caching;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.RedisCachingDb.Handler;

/// <summary>
///     Implementation of cache handler using redis as storage.
/// </summary>
internal sealed class CacheHandler : ICacheHandler
{
    private readonly IDistributedCache _distributedCache;

    public CacheHandler(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public async Task<AppCacheModel<TSource>> GetAsync<TSource>(
        string key,
        CancellationToken cancellationToken)
    {
        var cachedValue = await _distributedCache.GetStringAsync(
            key: key,
            token: cancellationToken);

        if (string.IsNullOrWhiteSpace(value: cachedValue))
        {
            return AppCacheModel<TSource>.NotFound;
        }

        return JsonSerializer.Deserialize<TSource>(json: cachedValue);
    }

    public Task RemoveAsync(
        string key,
        CancellationToken cancellationToken)
    {
        return _distributedCache.RemoveAsync(
            key: key,
            token: cancellationToken);
    }

    public Task SetAsync<TSource>(
        string key,
        TSource value,
        DistributedCacheEntryOptions distributedCacheEntryOptions,
        CancellationToken cancellationToken)
    {
        return _distributedCache.SetStringAsync(
            key: key,
            value: JsonSerializer.Serialize(value: value),
            options: distributedCacheEntryOptions,
            token: cancellationToken);
    }
}
