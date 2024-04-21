using FastEndpoints;
using GoalFinder.Application.Features.Auth.ForgotPassword;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.Middleware.Validation;

public class ForgotPasswordValidationPreProcessor : 
    PreProcessor<ForgotPasswordRequest, ForgotPasswordResponse>
{
    public override Task PreProcessAsync(
        IPreProcessorContext<ForgotPasswordRequest> context, 
        ForgotPasswordResponse state, 
        CancellationToken ct)
    {
        if(context.HasValidationFailures)
        {
            Console.WriteLine("Email validationf failded");
        }
        return Task.CompletedTask;
    }
}
