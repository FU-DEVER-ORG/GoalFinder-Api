using System;
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
///     Caching post processor for refresh access token
/// </summary>

internal sealed class RefreshAccessTokenCachingPostProcessor
    : PostProcessor<
        RefreshAccessTokenRequest,
        RefreshAccessTokenStateBag,
        RefreshAccessTokenHttpResponse
    >
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public RefreshAccessTokenCachingPostProcessor(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public override async Task PostProcessAsync(
        IPostProcessorContext<RefreshAccessTokenRequest, RefreshAccessTokenHttpResponse> context,
        RefreshAccessTokenStateBag state,
        CancellationToken ct
    )
    {
        if (Equals(objA: context.Response, objB: default))
        {
            return;
        }

        await using var scope = _serviceScopeFactory.CreateAsyncScope();

        var cacheHandler = scope.Resolve<ICacheHandler>();

        if (
            context.Response.AppCode.Equals(
                value: RefreshAccessTokenResponseStatusCode.REFRESH_TOKEN_IS_NOT_FOUND.ToAppCode()
            )
            || context.Response.AppCode.Equals(
                value: RefreshAccessTokenResponseStatusCode.REFRESH_TOKEN_IS_EXPIRED.ToAppCode()
            )
            || context.Response.AppCode.Equals(
                value: RefreshAccessTokenResponseStatusCode.OPERATION_SUCCESS.ToAppCode()
            )
        )
        {
            await cacheHandler.SetAsync(
                key: state.CacheKey,
                value: context.Response,
                new()
                {
                    AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(
                        seconds: state.CacheDurationInSeconds
                    )
                },
                cancellationToken: ct
            );
        }
    }
}
