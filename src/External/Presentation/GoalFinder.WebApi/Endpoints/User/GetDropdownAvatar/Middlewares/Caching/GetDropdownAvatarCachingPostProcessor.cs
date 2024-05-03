using System;
using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using GoalFinder.Application.Features.Auth.Login;
using GoalFinder.Application.Features.User.GetDropdownAvatar;
using GoalFinder.Application.Shared.Caching;
using GoalFinder.WebApi.Endpoints.User.GetDropdownAvatar.Common;
using GoalFinder.WebApi.Endpoints.User.GetDropdownAvatar.HttpResponseMapper;
using Microsoft.Extensions.DependencyInjection;

namespace GoalFinder.WebApi.Endpoints.User.GetDropdownAvatar.Middlewares.Caching;

/// <summary>
///     Caching post processor.
/// </summary>
internal sealed class GetDropdownAvatarCachingPostProcessor
    : PostProcessor<EmptyRequest, GetDropdownAvatarStateBag, GetDropdownAvatarHttpResponse>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public GetDropdownAvatarCachingPostProcessor(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public override async Task PostProcessAsync(
        IPostProcessorContext<EmptyRequest, GetDropdownAvatarHttpResponse> context,
        GetDropdownAvatarStateBag state,
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
                value: GetDropdownAvatarResponseStatusCode.USER_IS_NOT_FOUND.ToAppCode()
            )
            || context.Response.AppCode.Equals(
                value: GetDropdownAvatarResponseStatusCode.OPERATION_SUCCESS.ToAppCode()
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
