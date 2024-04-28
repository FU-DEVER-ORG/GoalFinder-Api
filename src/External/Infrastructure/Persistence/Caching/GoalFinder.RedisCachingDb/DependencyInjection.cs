using GoalFinder.AppJsonWebToken.ServiceConfigs;
using GoalFinder.RedisCachingDb.ServiceConfigs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoalFinder.RedisCachingDb;

/// <summary>
///     Configure services for this layer.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    ///     Entry to configuring multiple services.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    /// <param name="configuration">
    ///     Load configuration for configuration
    ///     file (appsetting).
    /// </param>
    public static void AddRedisCachingDatabase(
        this IServiceCollection services,
        IConfigurationManager configuration
    )
    {
        services.ConfigCore();
        services.ConfigStackExchangeRedisCache(configuration: configuration);
    }
}
