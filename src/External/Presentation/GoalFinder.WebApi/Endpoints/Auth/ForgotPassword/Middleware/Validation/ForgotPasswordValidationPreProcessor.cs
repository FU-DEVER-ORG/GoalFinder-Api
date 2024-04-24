using FastEndpoints;
using GoalFinder.Application.Features.Auth.ForgotPassword;
using GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.Common;
using GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.HttpResponseMapper.Others;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.Middleware.Validation;
/// <summary>
/// Forgot Password Validation Pre Processor
/// </summary>
internal class ForgotPasswordValidationPreProcessor : 
    PreProcessor<ForgotPasswordRequest, ForgotPasswordStateBag>
{
    /// <summary>
    /// Pre Process validation for Forgot Password
    /// </summary>
    /// <param name="context"></param>
    /// <param name="state"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public override  async Task PreProcessAsync(
        IPreProcessorContext<ForgotPasswordRequest> context, 
        ForgotPasswordStateBag state,
        CancellationToken ct)
    {
        // Validation
        if(context.HasValidationFailures)
        {
            var httpResponse = LazyForgotPasswordHttpResponseMapper
               .Get()
               .Resolve(statusCode: ForgotPasswordReponseStatusCode.INPUT_VALIDATION_FAIL)
               .Invoke(
                   arg1: context.Request,
                   arg2: new()
                   {
                       StatusCode = ForgotPasswordReponseStatusCode.INPUT_VALIDATION_FAIL
                   });
            // Send Response
            await context.HttpContext.Response.SendAsync(
                response: httpResponse,
                statusCode: httpResponse.HttpCode,
                cancellation: ct);

            context.HttpContext.MarkResponseStart();
        }
    }
}
