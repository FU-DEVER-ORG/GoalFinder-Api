using FastEndpoints;
using GoalFinder.Application.Features.Auth.ResetPasswordWithOtp;
using GoalFinder.Application.Shared.Caching;
using GoalFinder.WebApi.Endpoints.Auth.ResetPasswordWithOtp.Common;
using GoalFinder.WebApi.Endpoints.Auth.ResetPasswordWithOtp.HttpResponseMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.WebApi.Endpoints.Auth.ResetPasswordWithOtp.Middlewares.Caching;

/// <summary>
/// Reset Password With Otp Caching Post Processor
/// </summary>

internal sealed class ResetPasswordWithOptCachingPostProcessor : PostProcessor<
    ResetPasswordWithOtpRequest,
    ResetPasswordWithOtpStateBag,
    ResetPasswordWithOtpHttpResponse
    >
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public ResetPasswordWithOptCachingPostProcessor(
        IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public override async Task PostProcessAsync(IPostProcessorContext<ResetPasswordWithOtpRequest, ResetPasswordWithOtpHttpResponse> context, ResetPasswordWithOtpStateBag state, CancellationToken ct)
    {
        // checking if response is not null
        if(Equals(objA: context.Response, objB: default(ResetPasswordWithOtpHttpResponse)))
        {
            return;
        }

        await using var scope = _serviceScopeFactory.CreateAsyncScope();

        // Set new cache if current app code is suitable.
        var cachHandler = scope.Resolve<ICacheHandler>();

        if(
            context.Response.AppCode.Equals(
                value: ResetPasswordWithOtpResponseStatusCode.OTP_CODE_NOT_FOUND.ToAppCode()) ||
            context.Response.AppCode.Equals(
                value: ResetPasswordWithOtpResponseStatusCode.OTP_CODE_NOT_VALID.ToAppCode()) ||
            context.Response.AppCode.Equals(
                value: ResetPasswordWithOtpResponseStatusCode.OTP_CODE_IS_EXPIRED.ToAppCode())
            )
        {
            //Caching the return value
            await cachHandler.SetAsync(
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
