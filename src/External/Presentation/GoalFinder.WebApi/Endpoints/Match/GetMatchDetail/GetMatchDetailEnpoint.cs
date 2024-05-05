using System;
using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using GoalFinder.Application.Features.Match.GetMatchDetail;
using GoalFinder.WebApi.Endpoints.Match.GetMatchDetail.HttpResponseMapper;
using GoalFinder.WebApi.Endpoints.Match.GetMatchDetail.Middlewares.Authorization;
using GoalFinder.WebApi.Endpoints.Match.GetMatchDetail.Middlewares.Caching;
using GoalFinder.WebApi.Endpoints.Match.GetMatchDetail.Middlewares.Validation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace GoalFinder.WebApi.Endpoints.Match.GetMatchDetail;

internal sealed class GetMatchDetailEnpoint
    : Endpoint<GetMatchDetailRequest, GetMatchDetailHttpResponse>
{
    public override void Configure()
    {
        Get(routePatterns: "match/{matchId}");
        AuthSchemes(authSchemeNames: JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        PreProcessor<GetMatchDetailAuthorizationPreProcessor>();
        PreProcessor<GetMatchDetailValidationPreProcessor>();
        PreProcessor<GetMatchDetailCachingPreProcessor>();
        PostProcessor<GetMatchDetailCachingPostProcessor>();
        Description(builder: builder =>
        {
            builder.ClearDefaultProduces(statusCodes: StatusCodes.Status400BadRequest);
        });
        Summary(endpointSummary: summary =>
        {
            summary.Summary = "Endpoint for getting match detail";
            summary.Description = "This endpoint is used for getting match detail by matchId";
            summary.Response<GetMatchDetailHttpResponse>(
                description: "Represent successfull operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = GetMatchDetailResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                    Body = new GetMatchDetailResponse.Body()
                    {
                        FootBallMatchDetail = new()
                        {
                            MatchInfor = new()
                            {
                                Id = Guid.NewGuid(),
                                Address = "string",
                                Title = "string",
                                TimeAgo = default,
                                CompetitionLevel = default,
                                Description = "string",
                                MaxMatchPlayersNeed = default,
                                MinPrestigeScore = default,
                                PitchAddress = "string",
                                PitchPrice = default,
                                StartTime = default,
                            },
                            HostOfMatch = new()
                            {
                                Id = Guid.NewGuid(),
                                HostName = "string",
                                HostAvatar = "string",
                                HostPrestigeScore = default
                            },
                        },
                        ParticipatedUser =
                        [
                            new()
                            {
                                Id = Guid.NewGuid(),
                                UserAvatar = "string",
                                UserName = "string",
                                PhoneNumber = "string",
                                PrestigeScore = default,
                                UserAddress = "string",
                                UserCompetitionLevel = default,
                                UserPosition = ["string", "string"]
                            }
                        ],
                        CurrentPendingUser =
                        [
                            new()
                            {
                                Id = Guid.NewGuid(),
                                UserAvatar = "string",
                                UserName = "string",
                                PhoneNumber = "string",
                                PrestigeScore = default,
                                UserAddress = "string",
                                UserCompetitionLevel = default,
                                UserPosition = ["string", "string"]
                            }
                        ],
                        RejectedUsers =
                        [
                            new()
                            {
                                Id = Guid.NewGuid(),
                                UserAvatar = "string",
                                UserName = "string",
                                PhoneNumber = "string",
                                PrestigeScore = default,
                                UserAddress = "string",
                                UserCompetitionLevel = default,
                                UserPosition = ["string", "string"]
                            }
                        ]
                    }
                }
            );
        });
    }

    public override async Task<GetMatchDetailHttpResponse> ExecuteAsync(
        GetMatchDetailRequest req,
        CancellationToken ct
    )
    {
        // Get app feature response
        var appResponse = await req.ExecuteAsync(ct: ct);

        var httpResponse = LazyGetMatchDetailHttpResponseMapper
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
