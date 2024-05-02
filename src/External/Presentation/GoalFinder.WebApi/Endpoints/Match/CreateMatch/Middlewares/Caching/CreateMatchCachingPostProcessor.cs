using System;
using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using GoalFinder.Application.Features.Match.CreateMatch;
using GoalFinder.Application.Features.Match.GetAllMatches;
using GoalFinder.Application.Shared.Caching;
using GoalFinder.WebApi.Endpoints.Match.CreateMatch.Common;
using GoalFinder.WebApi.Endpoints.Match.CreateMatch.HttpResponseMapper;
using Microsoft.Extensions.DependencyInjection;

namespace GoalFinder.WebApi.Endpoints.Match.CreateMatch.Middlewares.Caching;

/// <summary>
///     Caching post processor.
/// </summary>
internal sealed class CreateMatchCachingPostProcessor
    : PostProcessor<CreateMatchRequest, CreateMatchStateBag, CreateMatchHttpResponse>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public CreateMatchCachingPostProcessor(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public override async Task PostProcessAsync(
        IPostProcessorContext<CreateMatchRequest, CreateMatchHttpResponse> context,
        CreateMatchStateBag state,
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
                value: CreateMatchResponseStatusCode.USER_ID_IS_NOT_FOUND.ToAppCode()
            )
            || context.Response.AppCode.Equals(
                value: CreateMatchResponseStatusCode.USER_IS_TEMPORARILY_REMOVED.ToAppCode()
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
                value: CreateMatchResponseStatusCode.OPERATION_SUCCESS.ToAppCode()
            )
        )
        {
            await cacheHandler.RemoveAsync(
                key: $"{nameof(GetAllMatchesRequest)}_matches",
                cancellationToken: ct
            );
        }
    }
}
