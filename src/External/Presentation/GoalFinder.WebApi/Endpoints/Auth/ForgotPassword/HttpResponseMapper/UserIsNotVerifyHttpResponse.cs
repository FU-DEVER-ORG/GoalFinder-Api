using GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.HttpResponseMapper.Others;
using GoalFinder.Application.Features.Auth.ForgotPassword;
using Microsoft.AspNetCore.Http;

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
    internal UserIsNotVerifyHttpResponse(ForgotPasswordResponse response) 
    { 
        HttpCode = StatusCodes.Status404NotFound;
        AppCode = response.StatusCode.ToAppCode();
    }
}
