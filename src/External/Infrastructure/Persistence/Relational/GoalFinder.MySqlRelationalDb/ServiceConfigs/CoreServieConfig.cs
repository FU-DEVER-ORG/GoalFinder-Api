using GoalFinder.Data.UnitOfWork;
using GoalFinder.MySqlRelationalDb.MySqlUnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace GoalFinder.MySqlRelationalDb.ServiceConfigs;

/// <summary>
///     Core service config.
/// </summary>
internal static class CoreServieConfig
{
    internal static void ConfigCore(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
