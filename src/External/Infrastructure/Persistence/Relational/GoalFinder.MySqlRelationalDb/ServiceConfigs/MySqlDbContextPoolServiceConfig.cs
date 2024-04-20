using GoalFinder.Configuration.Infrastructure.Persistence.Database;
using GoalFinder.MySqlRelationalDb.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GoalFinder.MySqlRelationalDb.ServiceConfigs;

/// <summary>
///     MySqlDbContextPool service config.
/// </summary>
internal static class MySqlDbContextPoolServiceConfig
{
    internal static void ConfigMySqlDbContextPool(
        this IServiceCollection services,
        IConfigurationManager configuration)
    {
        services.AddDbContextPool<GoalFinderContext>(optionsAction: (provider, config) =>
        {
            var option = configuration
                .GetRequiredSection(key: "Database")
                .GetRequiredSection(key: "FuDever")
                .Get<MySqlDatabaseOption>();

            config.UseMySQL(
                connectionString: option.ConnectionString,
                mySqlOptionsAction: mySqlOptionsAction =>
                {
                    mySqlOptionsAction
                        .CommandTimeout(commandTimeout: option.CommandTimeOut)
                        .EnableRetryOnFailure(maxRetryCount: option.EnableRetryOnFailure)
                        .MigrationsAssembly(assemblyName: Assembly.GetExecutingAssembly().GetName().Name);
                })
                .EnableSensitiveDataLogging(sensitiveDataLoggingEnabled: option.EnableSensitiveDataLogging)
                .EnableDetailedErrors(detailedErrorsEnabled: option.EnableDetailedErrors)
                .EnableThreadSafetyChecks(enableChecks: option.EnableThreadSafetyChecks)
                .EnableServiceProviderCaching(cacheServiceProvider: option.EnableServiceProviderCaching);
        });
    }
}
