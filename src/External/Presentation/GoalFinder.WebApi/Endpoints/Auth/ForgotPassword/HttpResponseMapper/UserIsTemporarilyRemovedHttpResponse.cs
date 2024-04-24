using GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.HttpResponseMapper.Others;
using Microsoft.AspNetCore.Http;
using GoalFinder.Application.Features.Auth.ForgotPassword;
using Org.BouncyCastle.Asn1.Ocsp;

namespace GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.HttpResponseMapper;
/// <summary>
///     UserIsTemporarilyRemovedHttpResponse
/// </summary>
internal sealed class UserIsTemporarilyRemovedHttpResponse : ForgotPasswordHttpReponse
{
    /// <summary>
    ///     UserIsTemporarilyRemovedHttpResponse
    /// </summary>
    /// <param name="response"></param>
    internal UserIsTemporarilyRemovedHttpResponse(ForgotPasswordRequest request,ForgotPasswordResponse response)
    {
        HttpCode = StatusCodes.Status403Forbidden;
        AppCode = response.StatusCode.ToAppCode();
        ErrorMessages =
        [
         $"User with username = {request.UserName} has already been banned or blocked by Admin.",
            "Please contact with admin to recover your account."
     ];
    }
}
