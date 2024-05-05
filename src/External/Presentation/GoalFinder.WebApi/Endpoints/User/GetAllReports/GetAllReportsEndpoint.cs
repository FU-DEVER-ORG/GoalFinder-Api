using FastEndpoints;
using System;
using GoalFinder.Application.Features.User.GetAllReports;
using GoalFinder.WebApi.Endpoints.User.GetAllReports.HttpResponseMapper;
using GoalFinder.WebApi.Endpoints.User.GetAllReports.Middleware.Caching;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Threading;
using GoalFinder.WebApi.Endpoints.Match.GetAllMatches.HttpResponseMapper;

namespace GoalFinder.WebApi.Endpoints.User.GetAllReports;

internal sealed class GetAllReportsEndpoint : Endpoint<EmptyRequest, GetAllReportsHttpResponse>
{
    public override void Configure()
    {
        Get(routePatterns: "/reports");
        AllowAnonymous();
        DontThrowIfValidationFails();
        PreProcessor<GetAllReportsCachingPreProcessor>();
        PostProcessor<GetAllReportsCachingPostProcessor>();
        Description(builder: builder =>
        {
            builder.ClearDefaultProduces(statusCodes: StatusCodes.Status400BadRequest);
        });

        Summary(endpointSummary: summary =>
        {
            summary.Summary = "Endpoint for get all reports.";
            summary.Description =
                "This endpoint is used for get reports sort by end time of matches";
            summary.Response<GetAllReportsHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = GetAllReportsStatusCode.OPERATION_SUCCESS.ToAppCode(),
                    Body = new GetAllReportsResponse.Body()
                    {
                        MatchPlayers =
                        [
                            new GetAllReportsResponse.Body.MatchPlayer(){
                                MatchId = Guid.NewGuid(),
                                PlayerId = Guid.NewGuid(),
                                PlayerName = "string",
                                NumberOfReports = default,

                            }
                        ]
                    }
                }
            );
        });
    }

    public override async Task<GetAllReportsHttpResponse> ExecuteAsync(
        EmptyRequest emptyRequest, CancellationToken ct
    )
    {
        //get command request
        var command = new GetAllReportsRequest();

        // Get app feature response.
        var appResponse = await command.ExecuteAsync(ct: ct);

        // Convert to http response.
        var httpResponse = LazyGetAllReportsHttpResponseMapper
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
