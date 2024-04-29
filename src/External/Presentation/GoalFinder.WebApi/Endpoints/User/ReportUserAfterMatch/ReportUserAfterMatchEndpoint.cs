using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using GoalFinder.Application.Features.User.GetUserInfoOnSidebar;
using GoalFinder.WebApi.Endpoints.User.GetUserInfoOnSidebar.HttpResponseMapper;
using GoalFinder.WebApi.Endpoints.User.ReportUserAfterMatch.Common;
using GoalFinder.WebApi.Endpoints.User.ReportUserAfterMatch.HttpResponseMapper;
using GoalFinder.WebApi.Endpoints.User.ReportUserAfterMatch.Middleware.Authorization;
using GoalFinder.WebApi.Endpoints.User.ReportUserAfterMatch.Middleware.Caching;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace GoalFinder.WebApi.Endpoints.User.ReportUserAfterMatch;

/// <summary>
///     Endpoint for report user after match
/// </summary>
internal sealed class ReportUserAfterMatchEndpoint
    : Endpoint<EmptyRequest, ReportUserAfterMatchHttpResponse>
{
    public override void Configure()
    {
        Patch(routePatterns: "user/report");
        AuthSchemes(authSchemeNames: JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        PreProcessor<ReportUserAfterMatchAuthorizationPreProcessor>();
        PreProcessor<ReportUserAfterMatchCachingPreProcessor>();
        PostProcessor<ReportUserAfterMatchCachingPostProcessor>();
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
            summary.Summary = "Endpoint for update prestige score after each match.";
            summary.Description =
                "This endpoint is used for update prestige score after each match.";
            summary.ExampleRequest = new() { };
            summary.Response<GetUserInfoOnSidebarHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = GetUserInfoOnSidebarResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                    Body = new GetUserInfoOnSidebarResponse.ResponseBody
                    {
                        Area = "string",
                        UserName = "string",
                        PrestigeScore = default
                    }
                }
            );
        });
    }

    public override async Task<ReportUserAfterMatchHttpResponse> ExecuteAsync(
        EmptyRequest req,
        CancellationToken ct
    )
    {
        var stateBag = ProcessorState<ReportUserAfterMatchStateBag>();

        var appResponse = await stateBag.AppRequest.ExecuteAsync(ct: ct);

        var httpResponse = LazyReportUserAfterMatchHttpResponseMapper
            .Get()
            .Resolve(statusCode: appResponse.StatusCode)
            .Invoke(arg1: stateBag.AppRequest, arg2: appResponse);

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
