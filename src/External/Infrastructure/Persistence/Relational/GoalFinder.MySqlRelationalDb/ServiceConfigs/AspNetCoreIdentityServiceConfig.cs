using GoalFinder.Configuration.Infrastructure.Persistence.AspNetCoreIdentity;
using GoalFinder.Data.Entities;
using GoalFinder.MySqlRelationalDb.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;

namespace GoalFinder.MySqlRelationalDb.ServiceConfigs;

/// <summary>
///     AspNetCoreIdentity service config.
/// </summary>
internal static class AspNetCoreIdentityServiceConfig
{
    internal static void ConfigAspNetCoreIdentity(
        this IServiceCollection services,
        IConfigurationManager configuration)
    {
        services
            .AddIdentity<User, Role>(setupAction: config =>
            {
                var option = configuration
                    .GetRequiredSection(key: "AspNetCoreIdentity")
                    .Get<AspNetCoreIdentityOption>();

                config.Password.RequireDigit = option.Password.RequireDigit;
                config.Password.RequireLowercase = option.Password.RequireLowercase;
                config.Password.RequireNonAlphanumeric = option.Password.RequireNonAlphanumeric;
                config.Password.RequireUppercase = option.Password.RequireUppercase;
                config.Password.RequiredLength = option.Password.RequiredLength;
                config.Password.RequiredUniqueChars = option.Password.RequiredUniqueChars;

                config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(value:
                    option.Lockout.DefaultLockoutTimeSpanInSecond);
                config.Lockout.MaxFailedAccessAttempts = option.Lockout.MaxFailedAccessAttempts;
                config.Lockout.AllowedForNewUsers = option.Lockout.AllowedForNewUsers;

                config.User.AllowedUserNameCharacters = option.User.AllowedUserNameCharacters;
                config.User.RequireUniqueEmail = option.User.RequireUniqueEmail;

                config.SignIn.RequireConfirmedEmail = option.SignIn.RequireConfirmedEmail;
                config.SignIn.RequireConfirmedPhoneNumber = option.SignIn.RequireConfirmedPhoneNumber;
            })
            .AddEntityFrameworkStores<GoalFinderContext>()
            .AddDefaultTokenProviders();
    }
}
