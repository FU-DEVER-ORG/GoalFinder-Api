using FastEndpoints;
using GoalFinder.Application.Features.User.UpdateUserInfo;
using GoalFinder.WebApi.Endpoints.User.UserInfoUpdate.Common;
using GoalFinder.WebApi.Endpoints.User.UserInfoUpdate.HttpResponseMapper;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.WebApi.Endpoints.User.UserInfoUpdate.Middlewares.Validation;

/// <summary>
///     Pre-processor for validation.
/// </summary>
internal class UpdateUserInfoValidationPreProcessor : PreProcessor<
    UpdateUserInfoRequest,
    UpdateUserInfoStateBag>
{
    public override async Task PreProcessAsync(
        IPreProcessorContext<UpdateUserInfoRequest> context,
        UpdateUserInfoStateBag state,
        CancellationToken ct)
    {
        if (context.HttpContext.ResponseStarted()) { return; }

        if (context.HasValidationFailures)
        {
            UpdateUserInfoHttpResponse httpResponse;

            if (!Equals(
                    objA: context.ValidationFailures
                        .Find(match: failure => failure.PropertyName
                            .Equals(value: "SerializerErrors")),
                    objB: default))
            {
                httpResponse = LazyUpdateUserInfoHttpResponseMapper
                    .Get()
                    .Resolve(statusCode: UpdateUserInfoResponseStatusCode.INPUT_NOT_UNDERSTANDABLE)
                    .Invoke(
                        arg1: context.Request,
                        arg2: new()
                        {
                            StatusCode = UpdateUserInfoResponseStatusCode.INPUT_NOT_UNDERSTANDABLE
                        });
            }
            else
            {
                httpResponse = LazyUpdateUserInfoHttpResponseMapper
                    .Get()
                    .Resolve(statusCode: UpdateUserInfoResponseStatusCode.INPUT_VALIDATION_FAIL)
                    .Invoke(
                        arg1: context.Request,
                        arg2: new()
                        {
                            StatusCode = UpdateUserInfoResponseStatusCode.INPUT_VALIDATION_FAIL
                        });
            }

            await context.HttpContext.Response.SendAsync(
                response: httpResponse,
                statusCode: httpResponse.HttpCode,
                cancellation: ct);

            context.HttpContext.MarkResponseStart();
        }
    }
}
