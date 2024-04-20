using GoalFinder.WebApi.Others.ServiceConfigs;
using Microsoft.Extensions.DependencyInjection;

namespace GoalFinder.WebApi;

/// <summary>
///     Entry to configuring multiple services
///     of web api.
/// </summary>
internal static class DependencyInjection
{
    /// <summary>
    ///     Entry to configuring multiple services.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    internal static void ConfigWebApi(this IServiceCollection services)
    {
        services.ConfigLogging();
        services.ConfigCors();
    }
}
