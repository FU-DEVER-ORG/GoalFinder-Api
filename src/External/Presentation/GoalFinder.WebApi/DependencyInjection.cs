using GoalFinder.AppJsonWebToken.ServiceConfigs;
using GoalFinder.WebApi.Shared.ServiceConfigs;
using Microsoft.Extensions.Configuration;
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
    internal static void ConfigWebApi(
        this IServiceCollection services,
        IConfigurationManager configuration)
    {
        services.ConfigLogging();
        services.ConfigCors();
        services.ConfigAuthentication(configuration: configuration);
        services.ConfigAuthorization();
        services.ConfigCore(configuration: configuration);
        services.ConfigSwagger(configuration: configuration);
        services.ConfigResponseCaching();
    }
}
