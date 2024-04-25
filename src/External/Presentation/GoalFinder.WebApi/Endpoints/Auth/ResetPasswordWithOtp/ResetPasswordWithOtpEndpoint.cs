using FastEndpoints;
using GoalFinder.Application.Features.Auth.ResetPasswordWithOtp;
using GoalFinder.WebApi.Endpoints.Auth.ResetPasswordWithOtp.HttpResponseMapper;
using GoalFinder.WebApi.Endpoints.Auth.ResetPasswordWithOtp.Middlewares.Caching;
using GoalFinder.WebApi.Endpoints.Auth.ResetPasswordWithOtp.Middlewares.Validation;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.WebApi.Endpoints.Auth.ResetPasswordWithOtp;

/// <summary>
///     Reset password with otp endpoint
/// </summary>

internal sealed class ResetPasswordWithOtpEndpoint :
    Endpoint<ResetPasswordWithOtpRequest, ResetPasswordWithOtpHttpResponse>
{
    public override void Configure()
    {
        Patch(routePatterns: "auth/reset-password-with-otp-code");
        AllowAnonymous();
        DontAutoSendResponse();
        PreProcessor<ResetPasswordWithOtpValidationPreProcessor>();
        PreProcessor<ResetPasswordWithOtpCachingPreProcessor>();
        PostProcessor<ResetPasswordWithOptCachingPostProcessor>();
        Description(builder: builder =>
        {
            builder.ClearDefaultProduces(statusCodes: StatusCodes.Status400BadRequest);
        });
        Summary(endpointSummary: sumary =>
        {
            sumary.Summary = "Endpoint for resetting password with OTP code";
            sumary.Description = "This endpoint is use for resetting password with OTP code purpose!";
            sumary.ExampleRequest = new()
            {
                OtpCode = "string",
                newPassword = "string",
                confirmPassword = "string"
            };
            sumary.Response<ResetPasswordWithOtpHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = ResetPasswordWithOtpResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                    ErrorMessages = [$"Messaging errors!"]
                });
        });
    }
    public override async Task<ResetPasswordWithOtpHttpResponse> ExecuteAsync(
        ResetPasswordWithOtpRequest req,
        CancellationToken ct)
    {
        // Get app feature response
        var appResponse  = await req.ExecuteAsync(ct: ct);

        //Convert app feature response to http response
        var httpResponse = LazyResetPasswordWithOtpHttpResponseMapper
            .Get()
            .Resolve(statusCode: appResponse.StatusCode)
            .Invoke(arg1: req, arg2: appResponse);

        /*
        * Store the real http code of http response into a temporary variable.
        * Set the http code of http response to default for not serializing.
        */
        var httpResponseStatusCode = httpResponse.HttpCode;
        httpResponse.HttpCode = default;

        await SendAsync(
            response: httpResponse,
            statusCode: httpResponseStatusCode,
            cancellation: ct);
        httpResponse.HttpCode = httpResponseStatusCode;

        return httpResponse;
    }

}
