using GoalFinder.AppJsonWebToken.ServiceConfigs;
using Microsoft.Extensions.DependencyInjection;

namespace GoalFinder.AppJsonWebToken;

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
    public static void ConfigAppJwtIdentityService(this IServiceCollection services)
    {
        services.ConfigCore();
    }
}
