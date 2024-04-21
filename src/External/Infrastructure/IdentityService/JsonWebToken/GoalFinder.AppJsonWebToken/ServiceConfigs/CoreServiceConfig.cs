using GoalFinder.AppJsonWebToken.Handler;
using GoalFinder.Application.Shared.Tokens.AccessToken;
using GoalFinder.Application.Shared.Tokens.RefreshToken;
using Microsoft.Extensions.DependencyInjection;

namespace GoalFinder.AppJsonWebToken.ServiceConfigs;

/// <summary>
///     Core service config.
/// </summary>
internal static class CoreServiceConfig
{
    internal static void ConfigCore(this IServiceCollection services)
    {
        services
            .AddSingleton<IAccessTokenHandler, AccessTokenHandler>()
            .AddSingleton<IRefreshTokenHandler, RefreshTokenHandler>();
    }
}
