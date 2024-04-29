using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using GoalFinder.Application.Features.Auth.Login;
using GoalFinder.Application.Features.Auth.RefreshAccessToken;
using GoalFinder.WebApi.Endpoints.Auth.RefreshAccessToken.HttpResponseMapper;
using GoalFinder.WebApi.Endpoints.Auth.RefreshAccessToken.Middlewares.Autorization;
using GoalFinder.WebApi.Endpoints.Auth.RefreshAccessToken.Middlewares.Caching;
using GoalFinder.WebApi.Endpoints.Auth.RefreshAccessToken.Middlewares.Validation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace GoalFinder.WebApi.Endpoints.Auth.RefreshAccessToken;

/// <summary>
/// `   RefreshAccessToken` endpoint
/// </summary>

internal sealed class RefreshAccessTokenEndpoint
    : Endpoint<RefreshAccessTokenRequest, RefreshAccessTokenHttpResponse>
{
    public override void Configure()
    {
        Post(routePatterns: "auth/refresh-access-token");
        AuthSchemes(authSchemeNames: JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        PreProcessor<RefreshAccessTokenAuthorizationPreProcessor>();
        PreProcessor<RefreshAccessTokenValidationPreProcessor>();
        PreProcessor<RefreshAccessTokenCachingPreProcessor>();
        PostProcessor<RefreshAccessTokenCachingPostProcessor>();

        Description(builder: builder =>
        {
            builder.ClearDefaultProduces(statusCodes: StatusCodes.Status400BadRequest);
        });
        Summary(endpointSummary: summary =>
        {
            summary.Summary = "Endpoint for login/signin feature";
            summary.Description = "This endpoint is used for login/signin purpose.";
            summary.ExampleRequest = new() { RefreshToken = "string" };
            summary.Response<RefreshAccessTokenHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = RefreshAccessTokenResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                    Body = new RefreshAccessTokenResponse.Body()
                    {
                        AccessToken = "string",
                        RefreshToken = "string",
                    }
                }
            );
        });
    }

    public override async Task<RefreshAccessTokenHttpResponse> ExecuteAsync(
        RefreshAccessTokenRequest req,
        CancellationToken ct
    )
    {
        //Get app feature response
        var appResponse = await req.ExecuteAsync(ct: ct);

        //Convert to http response

        var httpResponse = LazyRefreshAccessTokenHttpResponseMapper
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
        // Set the http code of http response back.
        httpResponse.HttpCode = httpResponseStatusCode;

        return httpResponse;
    }
}
