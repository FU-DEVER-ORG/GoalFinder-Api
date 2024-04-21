using GoalFinder.Application.Features.Auth.Login;
using GoalFinder.WebApi.Endpoints.Auth.Login.HttpResponseMapper.Others;
using Microsoft.AspNetCore.Http;

namespace GoalFinder.WebApi.Endpoints.Auth.Login.HttpResponseMapper;

/// <summary>
///     Login response status code
///     - user email is not confirmed
///     http response.
/// </summary>
internal sealed class UserEmailIsNotConfirmedHttpResponse : LoginHttpResponse
{
    internal UserEmailIsNotConfirmedHttpResponse(
        LoginRequest request,
        LoginResponse response)
    {
        HttpCode = StatusCodes.Status403Forbidden;
        AppCode = response.StatusCode.ToAppCode();
        ErrorMessages =
        [
            $"User with username = {request.Username} has not confirmed account creation email."
        ];
    }
}
