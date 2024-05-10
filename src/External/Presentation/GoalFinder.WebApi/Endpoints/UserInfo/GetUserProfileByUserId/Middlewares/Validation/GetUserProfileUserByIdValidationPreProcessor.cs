using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using GoalFinder.Application.Features.UserInfo.GetUserProfileByUserId;
using GoalFinder.WebApi.Endpoints.UserInfo.GetUserProfileByUserId.Common;
using GoalFinder.WebApi.Endpoints.UserInfo.GetUserProfileByUserId.HttpResponseMapper;
using Microsoft.AspNetCore.Http;

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
            await context.HttpContext.Response.SendAsync(
                response: new GetUserProfileByUserIdHttpResponse
                {
                    AppCode =
                        GetUserProfileByUserIdResponseStatusCode.INPUT_VALIDATION_FAIL.ToAppCode(),
                    Body = context.ValidationFailures.Select(selector: failure => new
                    {
                        failure.PropertyName,
                        failure.ErrorMessage
                    })
                },
                statusCode: StatusCodes.Status400BadRequest,
                cancellation: ct
            );

            context.HttpContext.MarkResponseStart();
        }
    }
}
