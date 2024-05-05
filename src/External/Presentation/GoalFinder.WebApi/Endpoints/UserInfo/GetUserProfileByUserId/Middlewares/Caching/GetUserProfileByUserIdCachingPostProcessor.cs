using System;
using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using GoalFinder.Application.Features.UserInfo.GetUserProfile;
using GoalFinder.Application.Features.UserInfo.GetUserProfileByUserId;
using GoalFinder.Application.Shared.Caching;
using GoalFinder.WebApi.Endpoints.UserInfo.GetUserProfileByUserId.Common;
using GoalFinder.WebApi.Endpoints.UserInfo.GetUserProfileByUserId.HttpResponseMapper;
using Microsoft.Extensions.DependencyInjection;

namespace GoalFinder.WebApi.Endpoints.UserInfo.GetUserProfileByUserId.Middlewares.Caching;

internal sealed class GetUserProfileByUserIdCachingPostProcessor
    : PostProcessor<
        GetUserProfileByUserIdRequest,
        GetUserProfileByUserIdStateBag,
        GetUserProfileByUserIdHttpResponse
    >
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public GetUserProfileByUserIdCachingPostProcessor(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public override async Task PostProcessAsync(
        IPostProcessorContext<
            GetUserProfileByUserIdRequest,
            GetUserProfileByUserIdHttpResponse
        > context,
        GetUserProfileByUserIdStateBag state,
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
                value: GetUserProfileByUserIdResponseStatusCode.USER_IS_NOT_FOUND.ToAppCode()
            )
            || context.Response.AppCode.Equals(
                value: GetUserProfileByUserIdResponseStatusCode.USER_IS_TEMPORARILY_REMOVED.ToAppCode()
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
