using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using GoalFinder.Application.Features.Match.CreateMatch;
using GoalFinder.WebApi.Endpoints.Match.CreateMatch.Common;
using GoalFinder.WebApi.Endpoints.Match.CreateMatch.HttpResponseMapper;

namespace GoalFinder.WebApi.Endpoints.Match.CreateMatch.Middlewares.Validation;

/// <summary>
///     Pre-processor for validation.
/// </summary>
internal class CreateMatchValidationPreProcessor
    : PreProcessor<CreateMatchRequest, CreateMatchStateBag>
{
    public override async Task PreProcessAsync(
        IPreProcessorContext<CreateMatchRequest> context,
        CreateMatchStateBag state,
        CancellationToken ct
    )
    {
        if (context.HttpContext.ResponseStarted())
        {
            return;
        }

        if (context.HasValidationFailures)
        {
            CreateMatchHttpResponse httpResponse;

            if (
                !Equals(
                    objA: context.ValidationFailures.Find(match: failure =>
                        failure.PropertyName.Equals(value: "SerializerErrors")
                    ),
                    objB: default
                )
            )
            {
                httpResponse = LazyCreateMatchHttpResponseMapper
                    .Get()
                    .Resolve(statusCode: CreateMatchResponseStatusCode.INPUT_NOT_UNDERSTANDABLE)
                    .Invoke(
                        arg1: context.Request,
                        arg2: new()
                        {
                            StatusCode = CreateMatchResponseStatusCode.INPUT_NOT_UNDERSTANDABLE
                        }
                    );
            }
            else
            {
                httpResponse = LazyCreateMatchHttpResponseMapper
                    .Get()
                    .Resolve(statusCode: CreateMatchResponseStatusCode.INPUT_VALIDATION_FAIL)
                    .Invoke(
                        arg1: context.Request,
                        arg2: new()
                        {
                            StatusCode = CreateMatchResponseStatusCode.INPUT_VALIDATION_FAIL,
                        }
                    );
            }

            await context.HttpContext.Response.SendAsync(
                response: httpResponse,
                statusCode: httpResponse.HttpCode,
                cancellation: ct
            );

            context.HttpContext.MarkResponseStart();
        }
    }
}
