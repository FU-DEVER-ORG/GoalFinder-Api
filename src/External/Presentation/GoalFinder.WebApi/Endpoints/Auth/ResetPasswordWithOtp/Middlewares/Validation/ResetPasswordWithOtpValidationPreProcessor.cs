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
            ResetPasswordWithOtpHttpResponse httpResponse;

            if (!Equals(
                objA: context.ValidationFailures
                    .Find(match: failure => failure.PropertyName
                        .Equals(value: "SerializerErrors")),
                objB: default))
            {
                httpResponse = LazyResetPasswordWithOtpHttpResponseMapper
                    .Get()
                    .Resolve(statusCode: ResetPasswordWithOtpResponseStatusCode.INPUT_NOT_UNDERSTANDABLE)
                    .Invoke(
                        arg1: context.Request,
                        arg2: new()
                        {
                            StatusCode = ResetPasswordWithOtpResponseStatusCode.INPUT_NOT_UNDERSTANDABLE
                        });
            }
            else
            {
                httpResponse = LazyResetPasswordWithOtpHttpResponseMapper
                    .Get()
                    .Resolve(statusCode: ResetPasswordWithOtpResponseStatusCode.INPUT_VALIDATION_FAILED)
                    .Invoke(
                        arg1: context.Request,
                        arg2: new()
                        {
                            StatusCode = ResetPasswordWithOtpResponseStatusCode.INPUT_VALIDATION_FAILED
                        });
            }

            await context.HttpContext.Response.SendAsync(
                response: httpResponse,
                statusCode: httpResponse.HttpCode,
                cancellation: ct);
            context.HttpContext.MarkResponseStart();
        }
    }
}
