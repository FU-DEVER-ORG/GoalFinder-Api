using GoalFinder.WebApi.Shared.Middlewares;
using Microsoft.Extensions.DependencyInjection;

namespace GoalFinder.WebApi.Shared.ServiceConfigs;

/// <summary>
///     Exception handler service config.
/// </summary>
internal static class ExceptionHandlerServiceConfig
{
    internal static void ConfigExceptionHandler(this IServiceCollection services)
    {
        services
            .AddExceptionHandler<GlobalExceptionHandler>()
            .AddProblemDetails();
    }
}
