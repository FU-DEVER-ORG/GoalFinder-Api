using GoalFinder.Application.Features.Auth.Login;
using GoalFinder.WebApi.Endpoints.Auth.Login.HttpResponseMapper.Others;
using Microsoft.AspNetCore.Http;

namespace GoalFinder.WebApi.Endpoints.Auth.Login.HttpResponseMapper;

/// <summary>
///     Login response status code
///     - user password is not correct
///     http response.
/// </summary>
internal sealed class UserPasswordIsNotCorrectHttpResponse : LoginHttpResponse
{
    internal UserPasswordIsNotCorrectHttpResponse(LoginResponse response)
    {
        HttpCode = StatusCodes.Status404NotFound;
        AppCode = response.StatusCode.ToAppCode();
        ErrorMessages =
        [
            "Password is not correct on this user."
        ];
    }
}
