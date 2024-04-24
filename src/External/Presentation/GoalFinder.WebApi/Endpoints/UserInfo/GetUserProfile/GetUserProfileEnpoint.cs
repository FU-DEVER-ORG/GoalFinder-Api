using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using GoalFinder.Application.Features.UserInfo.GetUserProfile;
using GoalFinder.WebApi.Endpoints.UserInfo.GetUserProfile.HttpResponseMapper;
using GoalFinder.WebApi.Endpoints.UserInfo.GetUserProfile.Middleware.Caching;
using GoalFinder.WebApi.Endpoints.UserInfo.GetUserProfile.Middleware.Validation;
using Microsoft.AspNetCore.Http;

namespace GoalFinder.WebApi.Endpoints.UserInfo.GetUserProfile
{
    internal sealed class GetUserProfileEndpoint : Endpoint<GetUserProfileRequest, GetUserProfileHttpResponse>
{
    public override void Configure()
    {
        Get(routePatterns: "user/{username}");
        AllowAnonymous();
        DontThrowIfValidationFails();
        PreProcessor<GetUserProfileValidationPreProcessor>();
        PreProcessor<GetUserProfileCachingPreProcessor>();
        PostProcessor<GetUserProfileCachingPostProcessor>();
        Description(builder: builder =>
        {
            builder.ClearDefaultProduces(statusCodes: StatusCodes.Status400BadRequest);
        });
        Summary(endpointSummary: summary =>
        {
            summary.Summary = "Endpoint for user profile feature";
            summary.Description = "This endpoint is used for get user profile purpose by username route.";
            summary.Response<GetUserProfileHttpResponse>(
                description: "Represent successful operation response.",
               example: new()
        {
            HttpCode = StatusCodes.Status200OK,
            AppCode = GetUserProfileResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
            Body = new GetUserProfileResponse.Body()
            {
                UserDetail = new GetUserProfileResponse.Body.User
                {
                    LastName = "string",
                    FirstName = "string",
                    Description = "string",
                    PrestigeScore = 0,
                    Address = "string",
                    AvatarUrl = "https://example.com/avatar.png",
                    Experience = "string",
                    CompetitionLevel = "string",
                    Positions = new List<string> { "string", "string" }
                },
                FootballMatches = new List<GetUserProfileResponse.Body.FootballMatch>
                {
                    new GetUserProfileResponse.Body.FootballMatch ()
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
                }
            }
        });
        });
    }

    public override async Task<GetUserProfileHttpResponse> ExecuteAsync(
        GetUserProfileRequest req,
        CancellationToken ct)
    {
        // Get app feature response.
        var appResponse = await req.ExecuteAsync(ct: ct);

        // Convert to http response.
        var httpResponse = LazyGetUserProfileHttResponseMapper
            .Get()
            .Resolve(statusCode: appResponse.StatusCode)
            .Invoke(arg1: req, arg2: appResponse);

        var httpResponseStatusCode = httpResponse.HttpCode;

        // Send http response to client.
        await SendAsync(
            response: httpResponse,
            statusCode: httpResponseStatusCode,
            cancellation: ct);

        return httpResponse;
    }
}
}