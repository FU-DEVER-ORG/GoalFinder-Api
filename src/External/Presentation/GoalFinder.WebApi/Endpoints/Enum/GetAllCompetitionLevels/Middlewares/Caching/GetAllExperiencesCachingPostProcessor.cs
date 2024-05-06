using System;
using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using GoalFinder.Application.Features.Auth.Login;
using GoalFinder.Application.Features.Enum.GetAllCompetitionLevels;
using GoalFinder.Application.Shared.Caching;
using GoalFinder.WebApi.Endpoints.Enum.GetAllCompetitionLevels.Common;
using GoalFinder.WebApi.Endpoints.Enum.GetAllCompetitionLevels.HttpResponseMapper;
using Microsoft.Extensions.DependencyInjection;

namespace GoalFinder.WebApi.Endpoints.Enum.GetAllCompetitionLevels.Middlewares.Caching;

/// <summary>
///     Caching post processor.
/// </summary>
internal sealed class GetAllCompetitionLevelsCachingPostProcessor
    : PostProcessor<EmptyRequest, GetAllCompetitionLevelsStateBag, GetAllCompetitionLevelsHttpResponse>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public GetAllCompetitionLevelsCachingPostProcessor(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public override async Task PostProcessAsync(
        IPostProcessorContext<EmptyRequest, GetAllCompetitionLevelsHttpResponse> context,
        GetAllCompetitionLevelsStateBag state,
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
                value: GetAllCompetitionLevelsResponseStatusCode.OPERATION_SUCCESS.ToAppCode()
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
