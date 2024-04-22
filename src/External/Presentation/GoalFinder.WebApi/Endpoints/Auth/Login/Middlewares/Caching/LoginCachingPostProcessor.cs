using FastEndpoints;
using GoalFinder.Application.Features.Auth.Login;
using GoalFinder.Application.Shared.Caching;
using GoalFinder.WebApi.Endpoints.Auth.Login.Common;
using GoalFinder.WebApi.Endpoints.Auth.Login.HttpResponseMapper.Others;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.WebApi.Endpoints.Auth.Login.Middlewares.Caching;

/// <summary>
///     Post-processor for login endpoint.
/// </summary>
internal sealed class LoginCachingPostProcessor : PostProcessor<
    LoginRequest,
    LoginStateBag,
    LoginHttpResponse>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public LoginCachingPostProcessor(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public override async Task PostProcessAsync(
        IPostProcessorContext<LoginRequest, LoginHttpResponse> context,
        LoginStateBag state,
        CancellationToken ct)
    {
        if (Equals(objA: context.Response, objB: default)) { return; }

        await using var scope = _serviceScopeFactory.CreateAsyncScope();

        var cacheHandler = scope.Resolve<ICacheHandler>();

            if (
                context.Response.AppCode.Equals(value:
                    LoginResponseStatusCode.USER_IS_NOT_FOUND.ToAppCode()) ||
                context.Response.AppCode.Equals(value:
                    LoginResponseStatusCode.USER_IS_TEMPORARILY_REMOVED.ToAppCode()) ||
                context.Response.AppCode.Equals(value:
                    LoginResponseStatusCode.USER_EMAIL_IS_NOT_CONFIRMED.ToAppCode()))
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
