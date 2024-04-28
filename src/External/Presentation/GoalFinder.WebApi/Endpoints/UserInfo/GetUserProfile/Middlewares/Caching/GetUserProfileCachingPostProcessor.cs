using System;
using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using GoalFinder.Application.Features.UserInfo.GetUserProfile;
using GoalFinder.Application.Shared.Caching;
using GoalFinder.WebApi.Endpoints.UserInfo.GetUserProfile.Common;
using GoalFinder.WebApi.Endpoints.UserInfo.GetUserProfile.HttpResponseMapper;
using Microsoft.Extensions.DependencyInjection;

namespace GoalFinder.WebApi.Endpoints.UserInfo.GetUserProfile.Middleware.Caching;

/// <summary>
///     This class is used for caching the get user profile response.
/// </summary>
internal sealed class GetUserProfileCachingPostProcessor
    : PostProcessor<GetUserProfileRequest, GetUserProfileStateBag, GetUserProfileHttpResponse>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public GetUserProfileCachingPostProcessor(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public override async Task PostProcessAsync(
        IPostProcessorContext<GetUserProfileRequest, GetUserProfileHttpResponse> context,
        GetUserProfileStateBag state,
        CancellationToken ct
    )
    {
        if (Equals(objA: context.Response, objB: default))
        {
            return;
        }

        await using var scope = _serviceScopeFactory.CreateAsyncScope();

        var cacheHandler = scope.Resolve<ICacheHandler>();

        // Caching
        if (
            context.Response.AppCode.Equals(
                value: GetUserProfileResponseStatusCode.USER_IS_NOT_FOUND.ToAppCode()
            )
            || context.Response.AppCode.Equals(
                value: GetUserProfileResponseStatusCode.USER_IS_TEMPORARILY_REMOVED.ToAppCode()
            )
        )
        {
            // Set cache
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
