using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using GoalFinder.Application.Features.Match.CreateMatch;
using GoalFinder.WebApi.Endpoints.Match.CreateMatch.HttpResponseMapper;
using GoalFinder.WebApi.Endpoints.Match.CreateMatch.Middlewares.Authorization;
using GoalFinder.WebApi.Endpoints.Match.CreateMatch.Middlewares.Caching;
using GoalFinder.WebApi.Endpoints.Match.CreateMatch.Middlewares.Validation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace GoalFinder.WebApi.Endpoints.Match.CreateMatch;

/// <summary>
///     Endpoint for updating user information.
/// </summary>
internal sealed class CreateMatchEndpoint : Endpoint<CreateMatchRequest, CreateMatchHttpResponse>
{
    public override void Configure()
    {
        Post(routePatterns: "match/create");
        AuthSchemes(authSchemeNames: JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        PreProcessor<CreateMatchAuthorizationPreProcessor>();
        PreProcessor<CreateMatchValidationPreProcessor>();
        PreProcessor<CreateMatchCachingPreProcessor>();
        PostProcessor<CreateMatchCachingPostProcessor>();
        Description(builder: builder =>
        {
            builder.ClearDefaultProduces(
                StatusCodes.Status400BadRequest,
                StatusCodes.Status401Unauthorized,
                StatusCodes.Status403Forbidden
            );
        });
        Summary(endpointSummary: summary =>
        {
            summary.Summary = "Endpoint for create match information.";
            summary.Description = "This endpoint is used for creating new match purpose.";
            summary.ExampleRequest = new()
            {
                PitchAddress = "string",
                MaxMatchPlayersNeed = default,
                PitchPrice = default,
                Title = "string",
                Description = "string",
                MinPrestigeScore = default,
                Address = "string",
                StartTime = DateTime.MinValue,
                CompetitionLevelId = Guid.Empty,
            };
            summary.Response<CreateMatchHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = CreateMatchResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                }
            );
        });
    }

    public override async Task<CreateMatchHttpResponse> ExecuteAsync(
        CreateMatchRequest req,
        CancellationToken ct
    )
    {
        req.SetHostId(
            hostId: Guid.Parse(
                input: HttpContext.User.FindFirstValue(claimType: JwtRegisteredClaimNames.Sub)
            )
        );

        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = LazyCreateMatchHttpResponseMapper
            .Get()
            .Resolve(statusCode: appResponse.StatusCode)
            .Invoke(arg1: req, arg2: appResponse);

        var httpResponseStatusCode = httpResponse.HttpCode;
        httpResponse.HttpCode = default;

        await SendAsync(
            response: httpResponse,
            statusCode: httpResponseStatusCode,
            cancellation: ct
        );

        httpResponse.HttpCode = httpResponseStatusCode;

        return httpResponse;
    }
}
