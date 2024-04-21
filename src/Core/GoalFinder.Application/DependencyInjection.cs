using GoalFinder.Application.Shared.ServiceConfigs;
using Microsoft.Extensions.DependencyInjection;

namespace GoalFinder.Application;

/// <summary>
///     Entry to configuring multiple services
///     of the application.
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
    public static void ConfigApplication(this IServiceCollection services)
    {
        services.ConfigFastEndpoint();
    }
}
