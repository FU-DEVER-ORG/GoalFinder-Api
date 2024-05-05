using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using GoalFinder.Application.Features.Match.GetMatchDetail;
using GoalFinder.WebApi.Endpoints.Match.GetMatchDetail.Common;
using GoalFinder.WebApi.Endpoints.Match.GetMatchDetail.HttpResponseMapper;

namespace GoalFinder.WebApi.Endpoints.Match.GetMatchDetail.Middlewares.Validation;

internal sealed class GetMatchDetailValidationPreProcessor
    : PreProcessor<GetMatchDetailRequest, GetMatchDetailStateBag>
{
    public override async Task PreProcessAsync(
        IPreProcessorContext<GetMatchDetailRequest> context,
        GetMatchDetailStateBag state,
        CancellationToken ct
    )
    {
        if (context.HttpContext.ResponseStarted())
        {
            return;
        }
        if (context.HasValidationFailures)
        {
            GetMatchDetailHttpResponse httpResponse;

            if (
                !Equals(
                    objA: context.ValidationFailures.Find(match: failure =>
                        failure.PropertyName.Equals(value: "SerializerErrors")
                    ),
                    objB: default
                )
            )
            {
                httpResponse = LazyGetMatchDetailHttpResponseMapper
                    .Get()
                    .Resolve(statusCode: GetMatchDetailResponseStatusCode.INPUT_NOT_UNDERSTANDABLE)
                    .Invoke(
                        arg1: context.Request,
                        arg2: new()
                        {
                            StatusCode = GetMatchDetailResponseStatusCode.INPUT_NOT_UNDERSTANDABLE
                        }
                    );
            }
            else
            {
                httpResponse = LazyGetMatchDetailHttpResponseMapper
                    .Get()
                    .Resolve(statusCode: GetMatchDetailResponseStatusCode.INPUT_VALIDATION_FAIL)
                    .Invoke(
                        arg1: context.Request,
                        arg2: new()
                        {
                            StatusCode = GetMatchDetailResponseStatusCode.INPUT_VALIDATION_FAIL
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
