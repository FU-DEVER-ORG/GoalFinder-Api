using GoalFinder.Application.Shared.Tokens.OTP;
using GoalFinder.AppOTP.Handler;
using Microsoft.Extensions.DependencyInjection;

namespace GoalFinder.AppOTP.ServiceConfigs;

/// <summary>
/// Core Service Config
/// </summary>
internal static class CoreServiceConfig
{
    internal static void ConfigCore(this IServiceCollection services)
    {
        services.AddSingleton<IOtpHandler, OtpGenerator>();
    }
}
