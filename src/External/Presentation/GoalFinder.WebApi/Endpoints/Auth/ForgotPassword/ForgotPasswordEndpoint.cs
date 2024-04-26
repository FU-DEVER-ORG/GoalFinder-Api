using FastEndpoints;
using GoalFinder.Application.Features.Auth.ForgotPassword;
using GoalFinder.Application.Features.Auth.Login;
using GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.HttpResponseMapper;
using GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.Middleware.Caching;
using GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.Middleware.Validation;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.WebApi.Endpoints.Auth.ForgotPassword;

/// <summary>
///     Forgot password endpoint
/// </summary>
internal sealed class ForgotPasswordEndpoint : Endpoint<
    ForgotPasswordRequest,
    ForgotPasswordHttpResponse>
{
    public override void Configure()
    {
        Post(routePatterns: "auth/forgot-password");
        AllowAnonymous();
        PreProcessor<ForgotPasswordValidationPreProcessor>();
        PreProcessor<ForgotPasswordCachingPreProcessor>();
        PostProcessor<ForgotPasswordCachingPostProcessor>();
        DontThrowIfValidationFails();
        Description(builder: builder =>
        {
            builder.ClearDefaultProduces(statusCodes: StatusCodes.Status400BadRequest);
        });
        Summary(endpointSummary: summary =>
        {
            summary.Summary = "Endpoint for sending reset password OTP code.";
            summary.Description = "This endpoint is used for login/signin purpose.";
            summary.ExampleRequest = new()
            {
                UserName = "string",
            };
            summary.Response<ForgotPasswordHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = ForgotPasswordResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                    Body = new ForgotPasswordResponse.Body()
                    {
                        OtpCode = "string"
                    }
                });
        });
    }
    public override async Task<ForgotPasswordHttpResponse> ExecuteAsync(
        ForgotPasswordRequest req,
        CancellationToken ct)
    {
        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = LazyForgotPasswordHttpResponseMapper
            .Get()
            .Resolve(statusCode: appResponse.StatusCode)
            .Invoke(arg1: req, arg2: appResponse);

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
