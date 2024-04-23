using GoalFinder.AppOTP.ServiceConfigs;
using Microsoft.Extensions.DependencyInjection;

namespace GoalFinder.AppOTP;

/// <summary>
///     Config Dependecy Injection
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    ///     Config App OTP
    /// </summary>
    /// <param name="services">
    ///     Service Collection for managing services.
    /// </param>
    public static void ConfigAppOTP(this IServiceCollection services)
    {
        services.ConfigCore();
    }
}
