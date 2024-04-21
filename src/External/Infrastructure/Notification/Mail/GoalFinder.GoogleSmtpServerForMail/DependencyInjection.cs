using GoalFinder.GoogleSmtpServerForMail.ServiceConfigs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoalFinder.GoogleSmtpServerForMail;

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
    /// <param name="configuration">
    ///     Configuration manager.
    /// </param>
    public static void ConfigGoogleSmtpMailNotification(
        this IServiceCollection services,
        IConfigurationManager configuration)
    {
        services.ConfigCore(configuration: configuration);
    }
}
