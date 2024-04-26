using FastEndpoints;
using GoalFinder.Application.Features.UserInfo.GetUserProfile;
using GoalFinder.WebApi.Endpoints.UserInfo.GetUserProfile.Common;
using GoalFinder.WebApi.Endpoints.UserInfo.GetUserProfile.HttpResponseMapper;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.WebApi.Endpoints.UserInfo.GetUserProfile.Middleware.Validation;

/// <summary>
///     Preprocessor for get user profile validation.
/// </summary>
internal sealed class GetUserProfileValidationPreProcessor : PreProcessor<
    GetUserProfileRequest,
    GetUserProfileStateBag>
{
    public override async Task PreProcessAsync(
        IPreProcessorContext<GetUserProfileRequest> context,
        GetUserProfileStateBag state,
        CancellationToken ct)
    {
        if (context.HasValidationFailures)
        {
            var httpResponse = LazyGetUserProfileHttResponseMapper
                .Get()
                .Resolve(statusCode: GetUserProfileResponseStatusCode.INPUT_VALIDATION_FAIL)
                .Invoke(
                    arg1: context.Request,
                    arg2: new()
                    {
                        StatusCode = GetUserProfileResponseStatusCode.INPUT_VALIDATION_FAIL
                    });

            await context.HttpContext.Response.SendAsync(
                response: httpResponse,
                statusCode: httpResponse.HttpCode,
                cancellation: ct);

            context.HttpContext.MarkResponseStart();
        }
    }
}
