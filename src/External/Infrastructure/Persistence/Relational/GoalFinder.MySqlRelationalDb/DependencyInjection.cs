using GoalFinder.MySqlRelationalDb.ServiceConfigs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoalFinder.MySqlRelationalDb;

/// <summary>
///     Entry to configuring multiple services
///     of my sql relational database.
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
    public static void ConfigMySqlRelationalDatabase(
        this IServiceCollection services,
        IConfigurationManager configuration)
    {
        services.ConfigMySqlDbContextPool(configuration: configuration);
        services.ConfigCore();
        services.ConfigAspNetCoreIdentity(configuration: configuration);
    }
}
