using System;
using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using GoalFinder.Application.Features.UserInfo.GetUserProfile;
using GoalFinder.Application.Features.UserInfo.GetUserProfileByUserId;
using GoalFinder.WebApi.Endpoints.UserInfo.GetUserProfileByUserId.HttpResponseMapper;
using GoalFinder.WebApi.Endpoints.UserInfo.GetUserProfileByUserId.Middlewares.Caching;
using GoalFinder.WebApi.Endpoints.UserInfo.GetUserProfileByUserId.Middlewares.Validation;
using Microsoft.AspNetCore.Http;

namespace GoalFinder.WebApi.Endpoints.UserInfo.GetUserProfileByUserId;

/// <summary>
///     Get user profile by user id endpoint.
/// </summary>

internal sealed class GetUserProfileByUserIdEnpoint
    : Endpoint<GetUserProfileByUserIdRequest, GetUserProfileByUserIdHttpResponse>
{
    public override void Configure()
    {
        Get(routePatterns: "user/profile-id/{id}");
        AllowAnonymous();
        DontThrowIfValidationFails();
        PreProcessor<GetUserProfileUserByIdValidationPreProcessor>();
        PreProcessor<GetUserProfileByUserIdCachingPreProcessor>();
        PostProcessor<GetUserProfileByUserIdCachingPostProcessor>();
        Description(builder: builder =>
        {
            builder.ClearDefaultProduces(statusCodes: StatusCodes.Status400BadRequest);
        });
        Summary(endpointSummary: summary =>
        {
            summary.Summary = "Endpoint for user profile feature by user id";
            summary.Description =
                "This endpoint is used for get user profile purpose by user id route prameter.";
            summary.Response<GetUserProfileByUserIdHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode =
                        GetUserProfileByUserIdResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                    Body = new GetUserProfileByUserIdResponse.Body()
                    {
                        UserDetail = new GetUserProfileByUserIdResponse.Body.User
                        {
                            NickName = "string",
                            LastName = "string",
                            FirstName = "string",
                            Description = "string",
                            PrestigeScore = default,
                            Address = "string",
                            AvatarUrl = "https://example.com/avatar.png",
                            Experience = "string",
                            CompetitionLevel = "string",
                            Positions = ["string", "string"]
                        },
                        FootballMatches =
                        [
                            new GetUserProfileByUserIdResponse.Body.FootballMatch()
                            {
                                Id = Guid.NewGuid(),
                                PitchAddress = "string",
                                MaxMatchPlayersNeed = 0,
                                PitchPrice = 0.00m,
                                Description = "string",
                                StartTime = "2024-04-25 10:00 AM",
                                Address = "string",
                                CompetitionLevel = "string"
                            }
                        ]
                    }
                }
            );
        });
    }

    public override async Task<GetUserProfileByUserIdHttpResponse> ExecuteAsync(
        GetUserProfileByUserIdRequest req,
        CancellationToken ct
    )
    {
        // Get app feature response.
        var appResponse = await req.ExecuteAsync(ct: ct);

        // Convert to http response.
        var httpResponse = LazyGetUserProfileByUserIdHttpResponseMapper
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
