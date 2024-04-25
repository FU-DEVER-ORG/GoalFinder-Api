using FastEndpoints;
using GoalFinder.Application.Features.Auth.Login;
using GoalFinder.Application.Features.User.UpdateUserInfo;
using GoalFinder.Application.Shared.Caching;
using GoalFinder.WebApi.Endpoints.User.UserInfoUpdate.Common;
using GoalFinder.WebApi.Endpoints.User.UserInfoUpdate.HttpResponseMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.WebApi.Endpoints.User.UserInfoUpdate.Middlewares.Caching;

/// <summary>
///     Caching post processor.
/// </summary>
internal sealed class UpdateUserInfoCachingPostProcessor : PostProcessor<
    UpdateUserInfoRequest,
    UpdateUserInfoStateBag,
    UpdateUserInfoHttpResponse>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public UpdateUserInfoCachingPostProcessor(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public override async Task PostProcessAsync(IPostProcessorContext<
        UpdateUserInfoRequest,
        UpdateUserInfoHttpResponse> context,
        UpdateUserInfoStateBag state,
        CancellationToken ct)
    {
        if (Equals(objA: context.Response, objB: default)) { return; }

        await using var scope = _serviceScopeFactory.CreateAsyncScope();

        var cacheHandler = scope.Resolve<ICacheHandler>();

        if (
            context.Response.AppCode.Equals(
                value: UpdateUserInfoResponseStatusCode.USER_NOT_FOUND.ToAppCode()) ||
            context.Response.AppCode.Equals(
                value: UpdateUserInfoResponseStatusCode.USERNAME_IS_ALREADY_TAKEN.ToAppCode()))
        {
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
