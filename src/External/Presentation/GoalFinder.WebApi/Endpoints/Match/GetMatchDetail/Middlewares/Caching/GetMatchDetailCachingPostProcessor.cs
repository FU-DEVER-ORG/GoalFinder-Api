using System;
using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using GoalFinder.Application.Features.Match.CreateMatch;
using GoalFinder.Application.Features.Match.GetAllMatches;
using GoalFinder.Application.Features.Match.GetMatchDetail;
using GoalFinder.Application.Shared.Caching;
using GoalFinder.WebApi.Endpoints.Match.GetMatchDetail.Common;
using GoalFinder.WebApi.Endpoints.Match.GetMatchDetail.HttpResponseMapper;
using Microsoft.Extensions.DependencyInjection;

namespace GoalFinder.WebApi.Endpoints.Match.GetMatchDetail.Middlewares.Caching;

/// <summary>
///     Post-processor for caching
/// </summary>

internal sealed class GetMatchDetailCachingPostProcessor
    : PostProcessor<GetMatchDetailRequest, GetMatchDetailStateBag, GetMatchDetailHttpResponse>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public GetMatchDetailCachingPostProcessor(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public override async Task PostProcessAsync(
        IPostProcessorContext<GetMatchDetailRequest, GetMatchDetailHttpResponse> context,
        GetMatchDetailStateBag state,
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
                value: GetMatchDetailResponseStatusCode.USER_IS_TEMPORARILY_REMOVED.ToAppCode()
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
        else if (
            context.Response.AppCode.Equals(
                value: GetMatchDetailResponseStatusCode.OPERATION_SUCCESS.ToAppCode()
            )
        )
        {
            await cacheHandler.RemoveAsync(key: state.CacheKey, cancellationToken: ct);
        }
    }
}
