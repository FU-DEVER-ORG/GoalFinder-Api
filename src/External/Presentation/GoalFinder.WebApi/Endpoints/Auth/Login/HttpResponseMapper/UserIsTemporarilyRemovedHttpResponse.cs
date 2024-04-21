using GoalFinder.Application.Features.Auth.Login;
using GoalFinder.WebApi.Endpoints.Auth.Login.HttpResponseMapper.Others;
using Microsoft.AspNetCore.Http;

namespace GoalFinder.WebApi.Endpoints.Auth.Login.HttpResponseMapper;

/// <summary>
///     Login response status code
///     - user is temporarily removed
///     http response.
/// </summary>
internal sealed class UserIsTemporarilyRemovedHttpResponse : LoginHttpResponse
{
    internal UserIsTemporarilyRemovedHttpResponse(
        LoginRequest request,
        LoginResponse response)
    {
        HttpCode = StatusCodes.Status403Forbidden;
        AppCode = response.StatusCode.ToAppCode();
        ErrorMessages =
        [
            $"User with username = {request.Username} has already been banned or blocked by Admin.",
            "Please contact with admin to recover your account."
        ];
    }
}
