using GoalFinder.Configuration.Infrastructure.Persistence.Cache.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoalFinder.RedisCachingDb.ServiceConfigs;

/// <summary>
///     StackExchangeRedisCache service config.
/// </summary>
internal static class StackExchangeRedisCacheServiceConfig
{
    internal static void ConfigStackExchangeRedisCache(
        this IServiceCollection services,
        IConfigurationManager configuration)
    {
        var option = configuration
            .GetRequiredSection(key: "Cache")
            .GetRequiredSection(key: "Redis")
            .Get<RedisOption>();

        services.AddStackExchangeRedisCache(setupAction: config =>
        {
            config.Configuration = option.ConnectionString;
        });
    }
}
