using GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.HttpResponseMapper.Others;
using Microsoft.AspNetCore.Http;
using GoalFinder.Application.Features.Auth.ForgotPassword;

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
    internal UserIsTemporarilyRemovedHttpResponse(ForgotPasswordResponse response)
    {
        HttpCode = StatusCodes.Status403Forbidden;
        AppCode = response.StatusCode.ToAppCode();
    }
}
