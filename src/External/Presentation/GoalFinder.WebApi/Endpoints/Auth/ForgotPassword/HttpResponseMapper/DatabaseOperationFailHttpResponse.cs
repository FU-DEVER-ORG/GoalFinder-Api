using GoalFinder.Application.Features.Auth.ForgotPassword;
using GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.HttpResponseMapper.Others;
using Microsoft.AspNetCore.Http;
using System.Reflection.Emit;

namespace GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.HttpResponseMapper;
/// <summary>
///     Database operation fail http response
/// </summary>
internal sealed class DatabaseOperationFailHttpResponse : ForgotPasswordHttpReponse
{
    /// <summary>
    ///     Database operation fail http response constructor
    /// </summary>
    /// <param name="response"></param>
    internal DatabaseOperationFailHttpResponse(ForgotPasswordResponse response)
    {
        HttpCode = StatusCodes.Status500InternalServerError;
        AppCode = response.StatusCode.ToAppCode();
        ErrorMessages =
        [
            "Server error !!!"
        ];
    }
}
