using GoalFinder.Application.Shared.Features;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.Application.Features.Auth.ForgotPassword;

internal sealed class ForgotPasswordHandler : 
    IFeatureHandler<ForgotPasswordRequest, ForgotPasswordResponse>
{
    public ForgotPasswordHandler()
    {
    }

    public async Task<ForgotPasswordResponse> ExecuteAsync(
        ForgotPasswordRequest command, 
        CancellationToken ct)
    {
        Console.WriteLine("Forgot oke");
        return new()
        {
                StatusCode = ForgotPasswordReponseStatusCode.OPERATION_SUCCESS
        };
    }
}
