﻿using GoalFinder.Configuration.Presentation.WebApi.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoalFinder.WebApi.Shared.ServiceConfigs;

/// <summary>
///     Core service config.
/// </summary>
internal static class CoreServiceConfig
{
    internal static void ConfigCore(
        this IServiceCollection services,
        IConfigurationManager configuration)
    {
        services.AddSingleton(configuration
            .GetRequiredSection(key: "Authentication")
            .Get<JwtAuthenticationOption>());
    }
}