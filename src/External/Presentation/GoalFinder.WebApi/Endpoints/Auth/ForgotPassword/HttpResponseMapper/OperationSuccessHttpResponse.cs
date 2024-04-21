using GoalFinder.Application.Features.Auth.ForgotPassword;
using GoalFinder.Application.Features.Auth.Login;
using GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.HttpResponseMapper.Others;
using GoalFinder.WebApi.Endpoints.Auth.Login.HttpResponseMapper.Others;
using Microsoft.AspNetCore.Http;

namespace GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.HttpResponseMapper;

/// <summary>
///     Login response status code
///     - operation success http response.
/// </summary>
internal sealed class OperationSuccessHttpResponse : ForgotPasswordHttpReponse
{
    internal OperationSuccessHttpResponse(ForgotPasswordResponse response)
    {
        HttpCode = StatusCodes.Status200OK;
        AppCode = response.StatusCode.ToAppCode();
    }
}
