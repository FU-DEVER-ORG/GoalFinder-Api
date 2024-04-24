using GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.HttpResponseMapper.Others;
using GoalFinder.Application.Features.Auth.ForgotPassword;
using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Asn1.Ocsp;

namespace GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.HttpResponseMapper;

/// <summary>
///     User is not verify http response
/// </summary>
internal sealed class UserIsNotVerifyHttpResponse : ForgotPasswordHttpReponse
{
    /// <summary>
    ///     User is not verify http response constructor
    /// </summary>
    /// <param name="response"></param>
    internal UserIsNotVerifyHttpResponse( ForgotPasswordRequest request, ForgotPasswordResponse response)
    {
        HttpCode = StatusCodes.Status404NotFound;
        AppCode = response.StatusCode.ToAppCode();
        ErrorMessages = [
           $"User with username = {request.UserName} is not verify yet!."];
    }
}
