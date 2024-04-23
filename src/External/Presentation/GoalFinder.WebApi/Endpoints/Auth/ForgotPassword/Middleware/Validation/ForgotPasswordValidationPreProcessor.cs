using FastEndpoints;
using GoalFinder.Application.Features.Auth.ForgotPassword;
using GoalFinder.Application.Features.Auth.Login;
using GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.Common;
using GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.HttpResponseMapper.Others;
using GoalFinder.WebApi.Endpoints.Auth.Login.HttpResponseMapper.Others;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.Middleware.Validation;

internal class ForgotPasswordValidationPreProcessor : 
    PreProcessor<ForgotPasswordRequest, ForgotPasswordStateBag>
{
    public override  async Task PreProcessAsync(
        IPreProcessorContext<ForgotPasswordRequest> context, 
        ForgotPasswordStateBag state,
        CancellationToken ct)
    {
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

            await context.HttpContext.Response.SendAsync(
                response: httpResponse,
                statusCode: httpResponse.HttpCode,
                cancellation: ct);

            context.HttpContext.MarkResponseStart();
        }
    }
}
