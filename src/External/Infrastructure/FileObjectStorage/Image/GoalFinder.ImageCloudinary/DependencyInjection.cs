using GoalFinder.ImageCloudinary.ServiceConfigs;
using Microsoft.Extensions.DependencyInjection;

namespace GoalFinder.ImageCloudinary;

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
    public static void ConfigCloudinaryImageStorage(this IServiceCollection services)
    {
        services.ConfigCore();
    }
}
