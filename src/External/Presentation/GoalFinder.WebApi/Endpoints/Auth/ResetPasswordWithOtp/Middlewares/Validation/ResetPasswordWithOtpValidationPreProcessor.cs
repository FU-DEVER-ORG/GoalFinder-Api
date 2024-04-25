using FastEndpoints;
using GoalFinder.Application.Features.Auth.ResetPasswordWithOtp;
using GoalFinder.WebApi.Endpoints.Auth.ResetPasswordWithOtp.Common;
using GoalFinder.WebApi.Endpoints.Auth.ResetPasswordWithOtp.HttpResponseMapper;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.WebApi.Endpoints.Auth.ResetPasswordWithOtp.Middlewares.Validation;

/// <summary>
///     PreProcessor for ResetPasswordWithOtp for validation
/// </summary>

internal sealed class ResetPasswordWithOtpValidationPreProcessor :
    PreProcessor<ResetPasswordWithOtpRequest, ResetPasswordWithOtpStateBag>
{

    public override async Task PreProcessAsync(
        IPreProcessorContext<ResetPasswordWithOtpRequest> context,
        ResetPasswordWithOtpStateBag state,
        CancellationToken ct)
    {
        if(context.HasValidationFailures)
        {
            var httpResponse = LazyResetPasswordWithOtpHttpResponseMapper
                .Get()
                .Resolve(statusCode: ResetPasswordWithOtpResponseStatusCode.INPUT_VALIDATION_FAILD)
                .Invoke(
                    arg1: context.Request,
                    arg2: new()
                    {
                        StatusCode = ResetPasswordWithOtpResponseStatusCode.INPUT_VALIDATION_FAILD
                    }
                );
            await context.HttpContext.Response.SendAsync(
                response: httpResponse,
                statusCode: httpResponse.HttpCode,
                cancellation: ct);
            context.HttpContext.MarkResponseStart();
        }
    }
}
