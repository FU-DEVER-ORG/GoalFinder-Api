using System;
using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using GoalFinder.Application.Features.Auth.Login;
using GoalFinder.Application.Features.Enum.GetAllExperiences;
using GoalFinder.Application.Shared.Caching;
using GoalFinder.WebApi.Endpoints.Enum.GetAllExperiences.Common;
using GoalFinder.WebApi.Endpoints.Enum.GetAllExperiences.HttpResponseMapper;
using Microsoft.Extensions.DependencyInjection;

namespace GoalFinder.WebApi.Endpoints.Enum.GetAllExperiences.Middlewares.Caching;

/// <summary>
///     Caching post processor.
/// </summary>
internal sealed class GetAllExperiencesCachingPostProcessor
    : PostProcessor<EmptyRequest, GetAllExperiencesStateBag, GetAllExperiencesHttpResponse>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public GetAllExperiencesCachingPostProcessor(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public override async Task PostProcessAsync(
        IPostProcessorContext<EmptyRequest, GetAllExperiencesHttpResponse> context,
        GetAllExperiencesStateBag state,
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
                value: GetAllExperiencesResponseStatusCode.OPERATION_SUCCESS.ToAppCode()
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
