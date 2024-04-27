using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using GoalFinder.Application.Features.Auth.ForgotPassword;
using GoalFinder.Application.Shared.Caching;
using GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.Common;
using GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.HttpResponseMapper;
using Microsoft.Extensions.DependencyInjection;

namespace GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.Middleware.Caching;

/// <summary>
///     Caching pre processor for forgot password feature.
/// </summary>
internal sealed class ForgotPasswordCachingPreProcessor
    : PreProcessor<ForgotPasswordRequest, ForgotPasswordStateBag>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public ForgotPasswordCachingPreProcessor(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public override async Task PreProcessAsync(
        IPreProcessorContext<ForgotPasswordRequest> context,
        ForgotPasswordStateBag state,
        CancellationToken ct
    )
    {
        if (context.HttpContext.ResponseStarted())
        {
            return;
        }

        state.CacheKey = $"{nameof(ForgotPasswordRequest)}_username_{context.Request.UserName}";

        await using var scope = _serviceScopeFactory.CreateAsyncScope();

        var cacheHandler = scope.Resolve<ICacheHandler>();

        // get from cache
        var cacheModel = await cacheHandler.GetAsync<ForgotPasswordHttpResponse>(
            key: state.CacheKey,
            cancellationToken: ct
        );

        // send response
        if (!Equals(objA: cacheModel, objB: AppCacheModel<ForgotPasswordHttpResponse>.NotFound))
        {
            var httpCode = cacheModel.Value.HttpCode;
            cacheModel.Value.HttpCode = default;

            await context.HttpContext.Response.SendAsync(
                response: cacheModel.Value,
                statusCode: httpCode,
                cancellation: ct
            );

            context.HttpContext.MarkResponseStart();
        }
    }
}
