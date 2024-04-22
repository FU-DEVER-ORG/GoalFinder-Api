using FastEndpoints;
using GoalFinder.Application.Features.Auth.Login;
using GoalFinder.WebApi.Endpoints.Auth.Login.HttpResponseMapper.Others;
using GoalFinder.WebApi.Endpoints.Auth.Login.Middlewares.Caching;
using GoalFinder.WebApi.Endpoints.Auth.Login.Middlewares.Validation;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.WebApi.Endpoints.Auth.Login;

/// <summary>
///     Login endpoint.
/// </summary>
internal sealed class LoginEndpoint : Endpoint<LoginRequest, LoginHttpResponse>
{
    public override void Configure()
    {
        Post(routePatterns: "auth/signin");
        AllowAnonymous();
        DontThrowIfValidationFails();
        PreProcessor<LoginValidationPreProcessor>();
        PreProcessor<LoginCachingPreProcessor>();
        PostProcessor<LoginCachingPostProcessor>();
        Description(builder: builder =>
        {
            builder.ClearDefaultProduces(statusCodes: StatusCodes.Status400BadRequest);
        });
        Summary(endpointSummary: summary =>
        {
            summary.Summary = "Endpoint for login/signin feature";
            summary.Description = "This endpoint is used for login/signin purpose.";
            summary.ExampleRequest = new()
            {
                Username = "string",
                Password = "string",
                IsRemember = true
            };
            summary.Response<LoginHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = LoginResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                    Body = new LoginResponse.Body()
                    {
                        AccessToken = "string",
                        RefreshToken = "string",
                        User = new()
                        {
                            Email = "string",
                            AvatarUrl = "string"
                        }
                    }
                });
        });
    }

    public override async Task<LoginHttpResponse> ExecuteAsync(
        LoginRequest req,
        CancellationToken ct)
    {
        // Get app feature response.
        var appResponse = await req.ExecuteAsync(ct: ct);

        // Convert to http response.
        var httpResponse = LazyLoginHttResponseMapper
            .Get()
            .Resolve(statusCode: appResponse.StatusCode)
            .Invoke(arg1: req, arg2: appResponse);

        /*
         * Store the real http code of http response into a temporary variable.
         * Set the http code of http response to default for not serializing.
         */
        var httpResponseStatusCode = httpResponse.HttpCode;
        httpResponse.HttpCode = default;

        // Send http response to client.
        await SendAsync(
            response: httpResponse,
            statusCode: httpResponseStatusCode,
            cancellation: ct);

        // Set the http code of http response back to real one.
        httpResponse.HttpCode = httpResponseStatusCode;

        return httpResponse;
    }
}
