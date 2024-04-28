using System;
using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using GoalFinder.Application.Features.Auth.Login;
using GoalFinder.Application.Features.User.GetUserInfoOnSidebar;
using GoalFinder.Application.Shared.Caching;
using GoalFinder.WebApi.Endpoints.User.GetUserInfoOnSidebar.Common;
using GoalFinder.WebApi.Endpoints.User.GetUserInfoOnSidebar.HttpResponseMapper;
using Microsoft.Extensions.DependencyInjection;

namespace GoalFinder.WebApi.Endpoints.User.GetUserInfoOnSidebar.Middlewares.Caching;

/// <summary>
///     Caching post processor.
/// </summary>
internal sealed class GetUserInfoOnSidebarCachingPostProcessor
    : PostProcessor<EmptyRequest, GetUserInfoOnSidebarStateBag, GetUserInfoOnSidebarHttpResponse>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public GetUserInfoOnSidebarCachingPostProcessor(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public override async Task PostProcessAsync(
        IPostProcessorContext<EmptyRequest, GetUserInfoOnSidebarHttpResponse> context,
        GetUserInfoOnSidebarStateBag state,
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
                value: GetUserInfoOnSidebarResponseStatusCode.USER_IS_NOT_FOUND.ToAppCode()
            )
            || context.Response.AppCode.Equals(
                value: GetUserInfoOnSidebarResponseStatusCode.USER_IS_TEMPORARILY_REMOVED.ToAppCode()
            )
            || context.Response.AppCode.Equals(
                value: GetUserInfoOnSidebarResponseStatusCode.OPERATION_SUCCESS.ToAppCode()
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
