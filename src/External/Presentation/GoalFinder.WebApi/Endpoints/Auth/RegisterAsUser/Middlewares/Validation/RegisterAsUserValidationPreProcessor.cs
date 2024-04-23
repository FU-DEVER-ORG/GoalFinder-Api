using FastEndpoints;
using GoalFinder.Application.Features.Auth.Register;
using GoalFinder.WebApi.Endpoints.Auth.RegisterAsUser.Common;
using GoalFinder.WebApi.Endpoints.Auth.RegisterAsUser.HttpResponseMapper.Others;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.WebApi.Endpoints.Auth.RegisterAsUser.Middlewares.Validation;

/// <summary>
///     Pre-processor for register as user validation.
/// </summary>
internal sealed class RegisterAsUserValidationPreProcessor : PreProcessor<RegisterAsUserRequest, RegisterAsUserStateBag>
{
    public override async Task PreProcessAsync(
        IPreProcessorContext<RegisterAsUserRequest> context,
        RegisterAsUserStateBag state,
        CancellationToken ct)
    {
        if (context.HasValidationFailures)
        {
            var httpResponse = LazyRegisterAsUserHttResponseMapper
                .Get()
                .Resolve(statusCode: RegisterAsUserResponseStatusCode.INPUT_VALIDATION_FAIL)
                .Invoke(
                    arg1: context.Request,
                    arg2: new()
                    {
                        StatusCode = RegisterAsUserResponseStatusCode.INPUT_VALIDATION_FAIL
                    });

            await context.HttpContext.Response.SendAsync(
                response: httpResponse,
                statusCode: httpResponse.HttpCode,
                cancellation: ct);

            context.HttpContext.MarkResponseStart();
        }
    }
}
