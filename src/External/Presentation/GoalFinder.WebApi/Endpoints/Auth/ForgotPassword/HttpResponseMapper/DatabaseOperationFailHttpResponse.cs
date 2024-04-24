using GoalFinder.Application.Features.Auth.ForgotPassword;
using GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.HttpResponseMapper.Others;
using Microsoft.AspNetCore.Http;

namespace GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.HttpResponseMapper;
/// <summary>
///     Database operation fail http response
/// </summary>
internal sealed class DatabaseOperationFailHttpResponse : ForgotPasswordHttpReponse
{
    internal DatabaseOperationFailHttpResponse(ForgotPasswordResponse response)
    {
        HttpCode = StatusCodes.Status500InternalServerError;
        AppCode = response.StatusCode.ToAppCode();
    }
}
