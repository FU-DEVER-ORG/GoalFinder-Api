using FastEndpoints;
using GoalFinder.Application.Features.Auth.ForgotPassword;
using GoalFinder.Application.Shared.Caching;
using GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.Common;
using GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.HttpResponseMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.Middleware.Caching;

/// <summary>
///     This class is used for caching the forgot password response.
/// </summary>
internal sealed class ForgotPasswordCachingPostProcessor : PostProcessor<
    ForgotPasswordRequest,
    ForgotPasswordStateBag,
    ForgotPasswordHttpReponse>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public ForgotPasswordCachingPostProcessor(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public override async Task PostProcessAsync(
        IPostProcessorContext<ForgotPasswordRequest, ForgotPasswordHttpReponse> context,
        ForgotPasswordStateBag state,
        CancellationToken ct)
    {
        if(Equals(objA: context.Response, objB: default)) { return; }

        await using var scope = _serviceScopeFactory.CreateAsyncScope();

        var  cacheHandler = scope.Resolve<ICacheHandler>();

        // Caching
        if(
            context.Response.AppCode.Equals(
                value: ForgotPasswordReponseStatusCode.OPERATION_SUCCESS.ToAppCode()) ||
            context.Response.AppCode.Equals(
                value: ForgotPasswordReponseStatusCode.USER_WITH_EMAIL_IS_NOT_FOUND.ToAppCode()) ||
            context.Response.AppCode.Equals(
                value: ForgotPasswordReponseStatusCode.USER_IS_TEMPORARILY_REMOVED.ToAppCode()))
        {
            // Set cache
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
