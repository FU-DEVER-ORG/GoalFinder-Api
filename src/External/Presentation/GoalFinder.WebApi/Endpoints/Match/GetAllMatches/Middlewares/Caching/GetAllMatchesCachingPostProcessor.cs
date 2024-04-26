using FastEndpoints;
using GoalFinder.Application.Features.Match.GetAllMatches;
using GoalFinder.Application.Shared.Caching;
using GoalFinder.WebApi.Endpoints.Match.GetAllMatches.Common;
using GoalFinder.WebApi.Endpoints.Match.GetAllMatches.HttpResponseMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.WebApi.Endpoints.Match.GetAllMatches.Middlewares.Caching;

/// <summary>
///     Post-processor for get all football matches caching.
/// </summary>
internal sealed class GetAllMatchesCachingPostProcessor : PostProcessor<
    EmptyRequest,
    GetAllMatchesStateBag,
    GetAllMatchesHttpResponse>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public GetAllMatchesCachingPostProcessor(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public override async Task PostProcessAsync(
        IPostProcessorContext<EmptyRequest, GetAllMatchesHttpResponse> context,
        GetAllMatchesStateBag state,
        CancellationToken ct)
    {
        if (Equals(objA: context.Response, objB: default)) { return; }

        await using var scope = _serviceScopeFactory.CreateAsyncScope();

        var cacheHandler = scope.Resolve<ICacheHandler>();

        // Set new cache if current app code is suitable.
        if (context.Response.AppCode.Equals(value:
            GetAllMatchesResponseStatusCode.OPERATION_SUCCESS.ToAppCode()))
        {
            // Caching the return value.
            await cacheHandler.SetAsync(
                key: state.CacheKey,
                value: context.Response,
                new()
                {
                    AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(
                        seconds: state.CacheDurationInSeconds)
                },
                cancellationToken: ct);
        }
    }
}
