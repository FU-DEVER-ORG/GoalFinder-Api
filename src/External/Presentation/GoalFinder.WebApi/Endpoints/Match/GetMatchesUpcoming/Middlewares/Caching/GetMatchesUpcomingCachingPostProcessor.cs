using System;
using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using GoalFinder.Application.Features.Auth.Login;
using GoalFinder.Application.Features.Match.GetMatchesUpcoming;
using GoalFinder.Application.Shared.Caching;
using GoalFinder.WebApi.Endpoints.Match.GetMatchesUpcoming.Common;
using GoalFinder.WebApi.Endpoints.Match.GetMatchesUpcoming.HttpResponseMapper;
using Microsoft.Extensions.DependencyInjection;

namespace GoalFinder.WebApi.Endpoints.Match.GetMatchesUpcoming.Middlewares.Caching;

/// <summary>
///     Caching post processor.
/// </summary>
internal sealed class GetMatchesUpcomingCachingPostProcessor
    : PostProcessor<EmptyRequest, GetMatchesUpcomingStateBag, GetMatchesUpcomingHttpResponse>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public GetMatchesUpcomingCachingPostProcessor(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public override async Task PostProcessAsync(
        IPostProcessorContext<EmptyRequest, GetMatchesUpcomingHttpResponse> context,
        GetMatchesUpcomingStateBag state,
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
                value: GetMatchesUpcomingResponseStatusCode.USER_IS_NOT_FOUND.ToAppCode()
            )
            || context.Response.AppCode.Equals(
                value: GetMatchesUpcomingResponseStatusCode.USER_IS_TEMPORARILY_REMOVED.ToAppCode()
            )
            || context.Response.AppCode.Equals(
                value: GetMatchesUpcomingResponseStatusCode.OPERATION_SUCCESS.ToAppCode()
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
