﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GoalFinder.WebApi.Others.ServiceConfigs;

/// <summary>
///     Logging service config.
/// </summary>
internal static class LoggingServiceConfig
{
    internal static void ConfigLogging(this IServiceCollection services)
    {
        services.AddLogging(configure: config =>
        {
            config.ClearProviders();
            config.AddConsole();
        });
    }
}
