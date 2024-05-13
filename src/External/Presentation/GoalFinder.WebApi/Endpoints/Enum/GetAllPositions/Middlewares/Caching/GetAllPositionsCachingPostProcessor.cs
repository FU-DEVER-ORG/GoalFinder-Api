using System;
using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using GoalFinder.Application.Features.Auth.Login;
using GoalFinder.Application.Features.Enum.GetAllPositions;
using GoalFinder.Application.Shared.Caching;
using GoalFinder.WebApi.Endpoints.Enum.GetAllPositions.Common;
using GoalFinder.WebApi.Endpoints.Enum.GetAllPositions.HttpResponseMapper;
using Microsoft.Extensions.DependencyInjection;

namespace GoalFinder.WebApi.Endpoints.Enum.GetAllPositions.Middlewares.Caching;

/// <summary>
///     Caching post processor.
/// </summary>
internal sealed class GetAllPositionsCachingPostProcessor
    : PostProcessor<EmptyRequest, GetAllPositionsStateBag, GetAllPositionsHttpResponse>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public GetAllPositionsCachingPostProcessor(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public override async Task PostProcessAsync(
        IPostProcessorContext<EmptyRequest, GetAllPositionsHttpResponse> context,
        GetAllPositionsStateBag state,
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
                value: GetAllPositionsResponseStatusCode.OPERATION_SUCCESS.ToAppCode()
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
