using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using GoalFinder.Application.Features.Match.GetMatchesUpcoming;
using GoalFinder.WebApi.Endpoints.Match.GetMatchesUpcoming.Common;
using GoalFinder.WebApi.Endpoints.Match.GetMatchesUpcoming.HttpResponseMapper;
using GoalFinder.WebApi.Endpoints.Match.GetMatchesUpcoming.Middlewares.Authorization;
using GoalFinder.WebApi.Endpoints.Match.GetMatchesUpcoming.Middlewares.Caching;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace GoalFinder.WebApi.Endpoints.User.GetMatchesUpcoming;

/// <summary>
///     Endpoint for updating user information.
/// </summary>
internal sealed class GetMatchesUpcomingEndpoint
    : Endpoint<EmptyRequest, GetMatchesUpcomingHttpResponse>
{
    public override void Configure()
    {
        Get(routePatterns: "user/sidebar");
        AuthSchemes(authSchemeNames: JwtBearerDefaults.AuthenticationScheme);
        DontThrowIfValidationFails();
        PreProcessor<GetMatchesUpcomingAuthorizationPreProcessor>();
        PreProcessor<GetMatchesUpcomingCachingPreProcessor>();
        PostProcessor<GetMatchesUpcomingCachingPostProcessor>();
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
            summary.Summary = "Endpoint for updating user information.";
            summary.Description = "This endpoint is used for updating user information purpose.";
            summary.ExampleRequest = new() { };
            summary.Response<GetMatchesUpcomingHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = GetMatchesUpcomingResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                    Body = new GetMatchesUpcomingResponse.ResponseBody
                    {
                       MatchesUpcoming = []
                    }
                }
            );
        });
    }

    public override async Task<GetMatchesUpcomingHttpResponse> ExecuteAsync(
        EmptyRequest req,
        CancellationToken ct
    )
    {
        var stateBag = ProcessorState<GetMatchesUpcomingStateBag>();

        var appResponse = await stateBag.AppRequest.ExecuteAsync(ct: ct);

        var httpResponse = LazyGetMatchesUpcomingHttpResponseMapper
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
