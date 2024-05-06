using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using GoalFinder.Application.Features.Enum.GetAllCompetitionLevels;
using GoalFinder.WebApi.Endpoints.Enum.GetAllCompetitionLevels.Common;
using GoalFinder.WebApi.Endpoints.Enum.GetAllCompetitionLevels.HttpResponseMapper;
using GoalFinder.WebApi.Endpoints.Enum.GetAllCompetitionLevels.Middlewares.Caching;
using Microsoft.AspNetCore.Http;

namespace GoalFinder.WebApi.Endpoints.User.GetAllCompetitionLevels;

/// <summary>
///     Endpoint for get all CompetitionLevels.
/// </summary>
internal sealed class GetAllCompetitionLevelsEndpoint : Endpoint<EmptyRequest, GetAllCompetitionLevelsHttpResponse>
{
    public override void Configure()
    {
        Get(routePatterns: "enum/competitionLevels");
        AllowAnonymous();
        DontThrowIfValidationFails();
        PreProcessor<GetAllCompetitionLevelsCachingPreProcessor>();
        PostProcessor<GetAllCompetitionLevelsCachingPostProcessor>();
        Description(builder: builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(endpointSummary: summary =>
        {
            summary.Summary = "Endpoint for get all competitionLevels.";
            summary.Description = "This endpoint is used for dropdown enum competitionLevels.";
            summary.ExampleRequest = new() { };
            summary.Response<GetAllCompetitionLevelsHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = GetAllCompetitionLevelsResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                    Body = new GetAllCompetitionLevelsResponse.ResponseBody { CompetitionLevels = [] }
                }
            );
        });
    }

    public override async Task<GetAllCompetitionLevelsHttpResponse> ExecuteAsync(
        EmptyRequest req,
        CancellationToken ct
    )
    {
        var stateBag = ProcessorState<GetAllCompetitionLevelsStateBag>();

        var appResponse = await stateBag.AppRequest.ExecuteAsync(ct: ct);

        var httpResponse = LazyGetAllCompetitionLevelsHttpResponseMapper
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
