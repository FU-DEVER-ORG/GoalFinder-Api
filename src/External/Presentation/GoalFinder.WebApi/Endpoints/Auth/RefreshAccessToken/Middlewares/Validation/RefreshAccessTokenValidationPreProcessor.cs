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
            var httpResponse = LazyRefreshAccessTokenHttpResponseMapper
                .Get()
                .Resolve(statusCode: RefreshAccessTokenResponseStatusCode.INPUT_VALIDATION_FAIL)
                .Invoke(
                    arg1: context.Request,
                    arg2: new()
                    {
                        StatusCode = RefreshAccessTokenResponseStatusCode.INPUT_VALIDATION_FAIL
                    }
                );

            await context.HttpContext.Response.SendAsync(
                response: httpResponse,
                statusCode: httpResponse.HttpCode,
                cancellation: ct
            );

            context.HttpContext.MarkResponseStart();
        }
    }
}
