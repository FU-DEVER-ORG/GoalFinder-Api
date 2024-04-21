using FastEndpoints;
using GoalFinder.Application.Features.Auth.Login;
using GoalFinder.WebApi.Endpoints.Auth.Login.HttpResponseMapper.Others;
using GoalFinder.WebApi.Endpoints.Auth.Login.Middlewares;
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
        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = LazyLoginHttResponseMapper
            .Get()
            .Resolve(statusCode: appResponse.StatusCode)
            .Invoke(arg1: req, arg2: appResponse);

        await SendAsync(
            response: httpResponse,
            statusCode: httpResponse.HttpCode,
            cancellation: ct);

        return default;
    }
}
