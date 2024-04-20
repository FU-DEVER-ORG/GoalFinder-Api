using Microsoft.Extensions.DependencyInjection;

namespace GoalFinder.WebApi.Others.ServiceConfigs;

/// <summary>
///     Authorization service config.
/// </summary>
public static class AuthorizationServiceConfig
{
    internal static void ConfigAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization();
    }
}
