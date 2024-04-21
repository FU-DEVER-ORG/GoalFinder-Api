using GoalFinder.Application.Shared.Caching;
using GoalFinder.RedisCachingDb.Handler;
using Microsoft.Extensions.DependencyInjection;

namespace GoalFinder.AppJsonWebToken.ServiceConfigs;

/// <summary>
///     Core service config.
/// </summary>
internal static class CoreServiceConfig
{
    internal static void ConfigCore(this IServiceCollection services)
    {
        services.AddScoped<ICacheHandler, CacheHandler>();
    }
}
