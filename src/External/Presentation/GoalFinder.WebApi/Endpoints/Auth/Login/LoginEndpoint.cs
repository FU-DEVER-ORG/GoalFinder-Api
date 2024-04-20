using FastEndpoints;
using GoalFinder.Application.Features.Auth.Login;
using GoalFinder.Data.UnitOfWork;
using GoalFinder.WebApi.Endpoints.Auth.Login.HttpResponseMapper.Others;
using GoalFinder.WebApi.Endpoints.Auth.Login.Middlewares.Validation;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.WebApi.Endpoints.Auth.Login;

/// <summary>
///     Login endpoint.
/// </summary>
public sealed class LoginEndpoint : Endpoint<LoginRequest, LoginHttpResponse>
{
    public IUnitOfWork UnitOfWork { get; set; }

    public override void Configure()
    {
        Post(routePatterns: "auth/signin");
        AllowAnonymous();
        PreProcessor<LoginValidationPreProcessor>();
        DontThrowIfValidationFails();
        Description(builder: builder => builder.ClearDefaultProduces(
            StatusCodes.Status200OK,
            StatusCodes.Status400BadRequest,
            StatusCodes.Status401Unauthorized,
            StatusCodes.Status403Forbidden));
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
