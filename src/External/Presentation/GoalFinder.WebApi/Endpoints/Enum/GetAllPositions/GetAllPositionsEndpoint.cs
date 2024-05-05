using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using GoalFinder.Application.Features.Enum.GetAllPositions;
using GoalFinder.WebApi.Endpoints.Enum.GetAllPositions.Common;
using GoalFinder.WebApi.Endpoints.Enum.GetAllPositions.HttpResponseMapper;
using GoalFinder.WebApi.Endpoints.Enum.GetAllPositions.Middlewares.Caching;
using Microsoft.AspNetCore.Http;

namespace GoalFinder.WebApi.Endpoints.User.GetAllPositions;

/// <summary>
///     Endpoint for get all positions.
/// </summary>
internal sealed class GetAllPositionsEndpoint : Endpoint<EmptyRequest, GetAllPositionsHttpResponse>
{
    public override void Configure()
    {
        Get(routePatterns: "enum/positions");
        AllowAnonymous();
        DontThrowIfValidationFails();
        PreProcessor<GetAllPositionsCachingPreProcessor>();
        PostProcessor<GetAllPositionsCachingPostProcessor>();
        Description(builder: builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(endpointSummary: summary =>
        {
            summary.Summary = "Endpoint for get all positions.";
            summary.Description = "This endpoint is used for dropdown enum position.";
            summary.ExampleRequest = new() { };
            summary.Response<GetAllPositionsHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = GetAllPositionsResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                    Body = new GetAllPositionsResponse.ResponseBody { Positions = [] }
                }
            );
        });
    }

    public override async Task<GetAllPositionsHttpResponse> ExecuteAsync(
        EmptyRequest req,
        CancellationToken ct
    )
    {
        var stateBag = ProcessorState<GetAllPositionsStateBag>();

        var appResponse = await stateBag.AppRequest.ExecuteAsync(ct: ct);

        var httpResponse = LazyGetAllPositionsHttpResponseMapper
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
