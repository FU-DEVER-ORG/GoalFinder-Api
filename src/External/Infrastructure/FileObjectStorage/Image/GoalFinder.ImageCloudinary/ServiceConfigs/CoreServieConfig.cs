using GoalFinder.Application.Shared.FIleObjectStorage;
using GoalFinder.ImageCloudinary.Image;
using Microsoft.Extensions.DependencyInjection;

namespace GoalFinder.ImageCloudinary.ServiceConfigs;

/// <summary>
///     Core service config.
/// </summary>
internal static class CoreServiceConfig
{
    internal static void ConfigCore(this IServiceCollection services)
    {
        services.AddSingleton<IDefaultUserAvatarAsUrlHandler, DefaultUserAvatarAsUrlSourceHandler>();
    }
}
