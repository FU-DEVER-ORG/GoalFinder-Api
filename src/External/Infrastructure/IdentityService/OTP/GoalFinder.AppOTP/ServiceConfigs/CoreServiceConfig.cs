using GoalFinder.Application.Shared.Tokens.OTP;
using GoalFinder.AppOTP.Handler;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoalFinder.AppOTP.ServiceConfigs;
/// <summary>
/// Core Service Config
/// </summary>
internal static class CoreServiceConfig
{
    internal static void ConfigCore ( this IServiceCollection services) 
    {
        services.AddSingleton<IOtpHandler, OTPGenerator>();
    }
}
