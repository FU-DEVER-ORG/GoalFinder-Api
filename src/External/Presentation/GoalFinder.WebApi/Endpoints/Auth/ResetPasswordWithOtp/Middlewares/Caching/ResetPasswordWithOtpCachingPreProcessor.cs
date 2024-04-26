using FastEndpoints;
using GoalFinder.Application.Features.Auth.ResetPasswordWithOtp;
using GoalFinder.Application.Shared.Caching;
using GoalFinder.WebApi.Endpoints.Auth.ResetPasswordWithOtp.Common;
using GoalFinder.WebApi.Endpoints.Auth.ResetPasswordWithOtp.HttpResponseMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.WebApi.Endpoints.Auth.ResetPasswordWithOtp.Middlewares.Caching;

/// <summary>
///     Reset Password With Otp Caching Pre Processor
/// </summary>
internal sealed class ResetPasswordWithOtpCachingPreProcessor :
    PreProcessor<ResetPasswordWithOtpRequest, ResetPasswordWithOtpStateBag>
{

    private readonly IServiceScopeFactory _serviceScopeFactory;

    public ResetPasswordWithOtpCachingPreProcessor(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public override async Task PreProcessAsync(
        IPreProcessorContext<ResetPasswordWithOtpRequest> context,
        ResetPasswordWithOtpStateBag state,
        CancellationToken ct)
    {
        if(context.HttpContext.ResponseStarted()) { return; }

        state.CacheKey = $"{nameof(ResetPasswordWithOtpResponse)}_OTP_CODE_{context.Request.OtpCode}";

        await using var scope = _serviceScopeFactory.CreateAsyncScope();

        var cacheHandler = scope.Resolve<ICacheHandler>();

        // Retrieve from cache
        var cacheModel = await cacheHandler.GetAsync<ResetPasswordWithOtpHttpResponse>(
                key: state.CacheKey,
                cancellationToken: ct);

        //Cache value not found
        if(!Equals(
            objA: cacheModel,
            objB: AppCacheModel<ResetPasswordWithOtpHttpResponse>.NotFound))
        {
            await context.HttpContext.Response.SendAsync(
                response: cacheModel.Value,
                statusCode: cacheModel.Value.HttpCode,
                cancellation: ct);

            context.HttpContext.MarkResponseStart();
        }

    }
}
