using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using GoalFinder.Application.Features.UserInfo.GetUserProfileByUserId;
using GoalFinder.WebApi.Endpoints.UserInfo.GetUserProfileByUserId.Common;
using GoalFinder.WebApi.Endpoints.UserInfo.GetUserProfileByUserId.HttpResponseMapper;

namespace GoalFinder.WebApi.Endpoints.UserInfo.GetUserProfileByUserId.Middlewares.Validation;

/// <summary>
///     Pre processor for validation
/// </summary>

internal sealed class GetUserProfileUserByIdValidationPreProcessor
    : PreProcessor<GetUserProfileByUserIdRequest, GetUserProfileByUserIdStateBag>
{
    public override async Task PreProcessAsync(
        IPreProcessorContext<GetUserProfileByUserIdRequest> context,
        GetUserProfileByUserIdStateBag state,
        CancellationToken ct
    )
    {
        if (context.HasValidationFailures)
        {
            GetUserProfileByUserIdHttpResponse httpResponse;

            if (
                !Equals(
                    objA: context.ValidationFailures.Find(match: failure =>
                        failure.PropertyName.Equals(value: "SerializerErrors")
                    ),
                    objB: default
                )
            )
            {
                httpResponse = LazyGetUserProfileByUserIdHttpResponseMapper
                    .Get()
                    .Resolve(
                        statusCode: GetUserProfileByUserIdResponseStatusCode.INPUT_NOT_UNDERSTANDABLE
                    )
                    .Invoke(
                        arg1: context.Request,
                        arg2: new()
                        {
                            StatusCode =
                                GetUserProfileByUserIdResponseStatusCode.INPUT_NOT_UNDERSTANDABLE
                        }
                    );
            }
            else
            {
                httpResponse = LazyGetUserProfileByUserIdHttpResponseMapper
                    .Get()
                    .Resolve(
                        statusCode: GetUserProfileByUserIdResponseStatusCode.INPUT_VALIDATION_FAIL
                    )
                    .Invoke(
                        arg1: context.Request,
                        arg2: new()
                        {
                            StatusCode =
                                GetUserProfileByUserIdResponseStatusCode.INPUT_VALIDATION_FAIL,
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
