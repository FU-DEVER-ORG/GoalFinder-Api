using Microsoft.Extensions.DependencyInjection;

namespace GoalFinder.WebApi.Shared.ServiceConfigs;

/// <summary>
///     Response caching service config.
/// </summary>
internal static class ResponseCachingServiceConfig
{
    internal static void ConfigResponseCaching(this IServiceCollection services)
    {
        services.AddResponseCaching();
    }
}
