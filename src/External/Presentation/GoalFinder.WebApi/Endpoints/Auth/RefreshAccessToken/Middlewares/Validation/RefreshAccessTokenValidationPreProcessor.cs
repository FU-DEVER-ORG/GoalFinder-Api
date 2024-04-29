using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using GoalFinder.Application.Features.Auth.RefreshAccessToken;
using GoalFinder.WebApi.Endpoints.Auth.RefreshAccessToken.Common;
using GoalFinder.WebApi.Endpoints.Auth.RefreshAccessToken.HttpResponseMapper;

namespace GoalFinder.WebApi.Endpoints.Auth.RefreshAccessToken.Middlewares.Validation;

/// <summary>
///     PreProcessor for RefreshAccessToken
/// </summary>

internal sealed class RefreshAccessTokenValidationPreProcessor
    : PreProcessor<RefreshAccessTokenRequest, RefreshAccessTokenStateBag>
{
    public override async Task PreProcessAsync(
        IPreProcessorContext<RefreshAccessTokenRequest> context,
        RefreshAccessTokenStateBag state,
        CancellationToken ct
    )
    {
        if (context.HasValidationFailures)
        {
            RefreshAccessTokenHttpResponse httpResponse;
            if (
                !Equals(
                    objA: context.ValidationFailures.Find(match: failure =>
                        failure.PropertyName.Equals(value: "SerializerErrors")
                    ),
                    objB: default
                )
            )
            {
                httpResponse = LazyRefreshAccessTokenHttpResponseMapper
                    .Get()
                    .Resolve(
                        statusCode: RefreshAccessTokenResponseStatusCode.INPUT_NOT_UNDERSTANDABLE
                    )
                    .Invoke(
                        arg1: context.Request,
                        arg2: new()
                        {
                            StatusCode =
                                RefreshAccessTokenResponseStatusCode.INPUT_NOT_UNDERSTANDABLE
                        }
                    );
            }
            else
            {
                httpResponse = LazyRefreshAccessTokenHttpResponseMapper
                    .Get()
                    .Resolve(
                        statusCode: RefreshAccessTokenResponseStatusCode.INPUT_VALIDATION_FAILED
                    )
                    .Invoke(
                        arg1: context.Request,
                        arg2: new()
                        {
                            StatusCode =
                                RefreshAccessTokenResponseStatusCode.INPUT_VALIDATION_FAILED
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
