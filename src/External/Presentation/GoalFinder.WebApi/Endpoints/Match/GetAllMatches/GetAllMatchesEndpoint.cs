using System;
using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using GoalFinder.Application.Features.Match.GetAllMatches;
using GoalFinder.WebApi.Endpoints.Match.GetAllMatches.HttpResponseMapper;
using GoalFinder.WebApi.Endpoints.Match.GetAllMatches.Middleware.Caching;
using GoalFinder.WebApi.Endpoints.Match.GetAllMatches.Middlewares.Caching;
using Microsoft.AspNetCore.Http;

namespace GoalFinder.WebApi.Endpoints.Match.GetAllMatches;

internal sealed class GetAllMatchesEndpoint : Endpoint<EmptyRequest, GetAllMatchesHttpResponse>
{
    public override void Configure()
    {
        Get(routePatterns: "/match");
        AllowAnonymous();
        DontThrowIfValidationFails();
        PreProcessor<GetAllMatchesCachingPreProcessor>();
        PostProcessor<GetAllMatchesCachingPostProcessor>();
        Description(builder: builder =>
        {
            builder.ClearDefaultProduces(statusCodes: StatusCodes.Status400BadRequest);
        });
        Summary(endpointSummary: summary =>
        {
            summary.Summary = "Endpoint for get all football matches";
            summary.Description =
                "This endpoint is used for get all football matches sort by created time and display list matches in home page";
            summary.Response<GetAllMatchesHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = GetAllMatchesResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                    Body = new GetAllMatchesResponse.Body()
                    {
                        FootballMatches =
                        [
                            new GetAllMatchesResponse.Body.FootballMatch()
                            {
                                Id = Guid.NewGuid(),
                                PitchAddress = "string",
                                MaxMatchPlayersNeed = default,
                                PitchPrice = default,
                                Description = "string",
                                MinPrestigeScore = default,
                                StartTime = default,
                                Address = "string",
                                CompetitionLevel = "string",
                                TimeAgo = default,
                                HostId = Guid.NewGuid(),
                                HostName = "string",
                            }
                        ]
                    }
                }
            );
        });
    }

    public override async Task<GetAllMatchesHttpResponse> ExecuteAsync(
        EmptyRequest emptyRequest,
        CancellationToken ct
    )
    {
        // Get command request
        var command = new GetAllMatchesRequest();

        // Get app feature response.
        var appResponse = await command.ExecuteAsync(ct: ct);

        // Convert to http response.
        var httpResponse = LazyGetAllMatchesHttResponseMapper
            .Get()
            .Resolve(statusCode: appResponse.StatusCode)
            .Invoke(arg1: command, arg2: appResponse);

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
