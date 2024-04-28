using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using GoalFinder.Application.Features.Auth.Login;
using GoalFinder.Application.Features.Auth.Register;
using GoalFinder.WebApi.Endpoints.Auth.RegisterAsUser.HttpResponseMapper;
using GoalFinder.WebApi.Endpoints.Auth.RegisterAsUser.Middlewares.Caching;
using GoalFinder.WebApi.Endpoints.Auth.RegisterAsUser.Middlewares.Validation;
using Microsoft.AspNetCore.Http;

namespace GoalFinder.WebApi.Endpoints.Auth.RegisterAsUser;

/// <summary>
///     Endpoint for register as user.
/// </summary>
internal sealed class RegisterAsUserEndpoint
    : Endpoint<RegisterAsUserRequest, RegisterAsUserHttpResponse>
{
    public override void Configure()
    {
        Post(routePatterns: "auth/sign-up");
        AllowAnonymous();
        DontThrowIfValidationFails();
        PreProcessor<RegisterAsUserValidationPreProcessor>();
        PreProcessor<RegisterAsUserCachingPreProcessor>();
        PostProcessor<RegisterAsUserCachingPostProcessor>();
        Description(builder: builder =>
        {
            builder.ClearDefaultProduces(statusCodes: StatusCodes.Status400BadRequest);
        });
        Summary(endpointSummary: summary =>
        {
            summary.Summary = "Endpoint for register/signup feature";
            summary.Description = "This endpoint is used for register/signup purpose.";
            summary.ExampleRequest = new() { Email = "string", Password = "string" };
            summary.Response<RegisterAsUserHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = LoginResponseStatusCode.OPERATION_SUCCESS.ToAppCode()
                }
            );
        });
    }

    public override async Task<RegisterAsUserHttpResponse> ExecuteAsync(
        RegisterAsUserRequest req,
        CancellationToken ct
    )
    {
        // Get app feature response.
        var appResponse = await req.ExecuteAsync(ct: ct);

        // Convert to http response.
        var httpResponse = LazyRegisterAsUserHttResponseMapper
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
            cancellation: ct
        );

        // Set the http code of http response back to real one.
        httpResponse.HttpCode = httpResponseStatusCode;

        return httpResponse;
    }
}
