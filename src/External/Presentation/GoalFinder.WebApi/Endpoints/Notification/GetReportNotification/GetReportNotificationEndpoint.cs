using System;
using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using GoalFinder.Application.Features.Notification.GetReportNotification;
using GoalFinder.WebApi.Endpoints.Notification.GetReportNotification.Middlewares.Caching;
using GoalFinder.WebApi.Endpoints.Notification.GetReportNotification.HttpResponseMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using GoalFinder.WebApi.Endpoints.Notification.GetReportNotification.Middlewares.Authorization;

namespace GoalFinder.WebApi.Endpoints.Notification.GetReportNotification;

internal sealed class GetReportNotificationEndpoint
    : Endpoint<EmptyRequest, GetReportNotificationHttpResponse>
{
    public override void Configure()
    {
        Get(routePatterns: "/notification/get-report-notification");
        AuthSchemes(authSchemeNames: JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        PreProcessor<GetReportNotificationAuthorizationPreProcessor>();
        PreProcessor<GetReportNotificationCachingPreProcessor>();
        PostProcessor<GetReportNotificationCachingPostProcessor>();
        Description(builder: builder =>
        {
            builder.ClearDefaultProduces(statusCodes: StatusCodes.Status400BadRequest);
        });
        Summary(endpointSummary: summary =>
        {
            summary.Summary = "Endpoint for get notification report";
            summary.Description =
                "This endpoint is used for get report notification sort by created time";
            summary.Response<GetReportNotificationHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = GetReportNotificationResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                    Body = new GetReportNotificationResponse.Body()
                    {
                        ReportNotifications =
                        [
                            new GetReportNotificationResponse.Body.ReportNotification()
                            {
                                MatchId = Guid.NewGuid(),
                                EndTimeToReport = DateTime.Now,
                                IsReported = false,
                            }
                        ]
                    }
                }
            );
        });
    }

    public override async Task<GetReportNotificationHttpResponse> ExecuteAsync(
        EmptyRequest empty,
        CancellationToken ct
    )
    {
        // Get command request
        var command = new GetReportNotificationRequest();

        command.SetUserId(
            userId: Guid.Parse(
                input: HttpContext.User.FindFirstValue(claimType: JwtRegisteredClaimNames.Sub)
            )
        );

        // Get app feature response.
        var appResponse = await command.ExecuteAsync(ct: ct);

        // Convert to http response.
        var httpResponse = LazyGetReportNotificationHttResponseMapper
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