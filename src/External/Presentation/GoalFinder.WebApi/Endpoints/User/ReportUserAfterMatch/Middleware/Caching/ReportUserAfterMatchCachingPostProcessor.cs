using System;
using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using GoalFinder.Application.Features.User.ReportUserAfterMatch;
using GoalFinder.Application.Features.User.UpdateUserInfo;
using GoalFinder.Application.Shared.Caching;
using GoalFinder.WebApi.Endpoints.User.ReportUserAfterMatch.Common;
using GoalFinder.WebApi.Endpoints.User.ReportUserAfterMatch.HttpResponseMapper;
using Microsoft.Extensions.DependencyInjection;

namespace GoalFinder.WebApi.Endpoints.User.ReportUserAfterMatch.Middleware.Caching;

/// <summary>
///     Caching post processor
/// </summary>
internal sealed class ReportUserAfterMatchCachingPostProcessor
    : PostProcessor<EmptyRequest, ReportUserAfterMatchStateBag, ReportUserAfterMatchHttpResponse>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public ReportUserAfterMatchCachingPostProcessor(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public override async Task PostProcessAsync(
        IPostProcessorContext<EmptyRequest, ReportUserAfterMatchHttpResponse> context,
        ReportUserAfterMatchStateBag state,
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
                value: ReportUserAfterMatchResponseStatusCode.USER_IS_NOT_FOUND.ToAppCode()
            )
            || context.Response.AppCode.Equals(
                value: ReportUserAfterMatchResponseStatusCode.FOOTBALL_MATCH_IS_NOT_FOUND.ToAppCode()
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
