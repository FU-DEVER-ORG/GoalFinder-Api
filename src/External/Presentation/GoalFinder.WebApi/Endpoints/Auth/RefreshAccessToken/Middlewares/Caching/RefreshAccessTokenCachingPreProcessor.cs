using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using GoalFinder.Application.Features.Auth.RefreshAccessToken;
using GoalFinder.Application.Shared.Caching;
using GoalFinder.WebApi.Endpoints.Auth.RefreshAccessToken.Common;
using GoalFinder.WebApi.Endpoints.Auth.RefreshAccessToken.HttpResponseMapper;
using Microsoft.Extensions.DependencyInjection;

namespace GoalFinder.WebApi.Endpoints.Auth.RefreshAccessToken.Middlewares.Caching;

/// <summary>
///     Caching pre processor for refresh access token
/// </summary>

internal sealed class RefreshAccessTokenCachingPreProcessor
    : PreProcessor<RefreshAccessTokenRequest, RefreshAccessTokenStateBag>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public RefreshAccessTokenCachingPreProcessor(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public override async Task PreProcessAsync(
        IPreProcessorContext<RefreshAccessTokenRequest> context,
        RefreshAccessTokenStateBag state,
        CancellationToken ct
    )
    {
        if (context.HttpContext.ResponseStarted())
        {
            return;
        }
        state.CacheKey = $"{nameof(RefreshAccessTokenHttpResponse)}.{context.Request.RefreshToken}";

        await using var scope = _serviceScopeFactory.CreateAsyncScope();

        var cacheHandler = scope.Resolve<ICacheHandler>();

        var cacheModel = await cacheHandler.GetAsync<RefreshAccessTokenHttpResponse>(
            key: state.CacheKey,
            cancellationToken: ct
        );

        if (!Equals(objA: cacheModel, objB: AppCacheModel<RefreshAccessTokenHttpResponse>.NotFound))
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
