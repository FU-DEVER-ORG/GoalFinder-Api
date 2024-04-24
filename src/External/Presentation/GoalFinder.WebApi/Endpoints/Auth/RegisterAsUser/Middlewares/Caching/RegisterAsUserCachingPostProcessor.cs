using FastEndpoints;
using GoalFinder.Application.Features.Auth.ForgotPassword;
using GoalFinder.Application.Features.Auth.Login;
using GoalFinder.Application.Features.Auth.Register;
using GoalFinder.Application.Shared.Caching;
using GoalFinder.WebApi.Endpoints.Auth.Login.HttpResponseMapper;
using GoalFinder.WebApi.Endpoints.Auth.RegisterAsUser.Common;
using GoalFinder.WebApi.Endpoints.Auth.RegisterAsUser.HttpResponseMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.WebApi.Endpoints.Auth.RegisterAsUser.Middlewares.Caching;

/// <summary>
///     Post-processor for register as user caching.
/// </summary>
internal sealed class RegisterAsUserCachingPostProcessor : PostProcessor<
    RegisterAsUserRequest,
    RegisterAsUserStateBag,
    RegisterAsUserHttpResponse>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public RegisterAsUserCachingPostProcessor(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public override async Task PostProcessAsync(
        IPostProcessorContext<RegisterAsUserRequest, RegisterAsUserHttpResponse> context,
        RegisterAsUserStateBag state,
        CancellationToken ct)
    {
        if (Equals(objA: context.Response, objB: default)) { return; }

        await using var scope = _serviceScopeFactory.CreateAsyncScope();

        var cacheHandler = scope.Resolve<ICacheHandler>();

        // Set new cache if current app code is suitable.
        if (context.Response.AppCode.Equals(value:
                RegisterAsUserResponseStatusCode.USER_IS_EXISTED.ToAppCode()))
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

        // If registered successfully, remove login cache.
        else if (context.Response.AppCode.Equals(value:
            RegisterAsUserResponseStatusCode.OPERATION_SUCCESS.ToAppCode()))
        {
            await Task.WhenAll(
                cacheHandler.RemoveAsync(
                    key: $"{nameof(ForgotPasswordRequest)}_username_{context.Request.Email}",
                    cancellationToken: ct),
                cacheHandler.RemoveAsync(
                    key: $"{nameof(LoginHttpResponse)}_username_{context.Request.Email}",
                    cancellationToken: ct));
        }
    }
}
