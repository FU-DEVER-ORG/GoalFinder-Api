using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using GoalFinder.Application.Features.Enum.GetAllExperiences;
using GoalFinder.WebApi.Endpoints.Enum.GetAllExperiences.Common;
using GoalFinder.WebApi.Endpoints.Enum.GetAllExperiences.HttpResponseMapper;
using GoalFinder.WebApi.Endpoints.Enum.GetAllExperiences.Middlewares.Caching;
using Microsoft.AspNetCore.Http;

namespace GoalFinder.WebApi.Endpoints.User.GetAllExperiences;

/// <summary>
///     Endpoint for get all Experiences.
/// </summary>
internal sealed class GetAllExperiencesEndpoint : Endpoint<EmptyRequest, GetAllExperiencesHttpResponse>
{
    public override void Configure()
    {
        Get(routePatterns: "enum/experiences");
        AllowAnonymous();
        DontThrowIfValidationFails();
        PreProcessor<GetAllExperiencesCachingPreProcessor>();
        PostProcessor<GetAllExperiencesCachingPostProcessor>();
        Description(builder: builder =>
        {
            builder.ClearDefaultProduces(StatusCodes.Status400BadRequest);
        });
        Summary(endpointSummary: summary =>
        {
            summary.Summary = "Endpoint for get all experiences.";
            summary.Description = "This endpoint is used for dropdown enum experiences.";
            summary.ExampleRequest = new() { };
            summary.Response<GetAllExperiencesHttpResponse>(
                description: "Represent successful operation response.",
                example: new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = GetAllExperiencesResponseStatusCode.OPERATION_SUCCESS.ToAppCode(),
                    Body = new GetAllExperiencesResponse.ResponseBody { Experiences = [] }
                }
            );
        });
    }

    public override async Task<GetAllExperiencesHttpResponse> ExecuteAsync(
        EmptyRequest req,
        CancellationToken ct
    )
    {
        var stateBag = ProcessorState<GetAllExperiencesStateBag>();

        var appResponse = await stateBag.AppRequest.ExecuteAsync(ct: ct);

        var httpResponse = LazyGetAllExperiencesHttpResponseMapper
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
