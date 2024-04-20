using GoalFinder.Application.Features.Auth.Login;
using GoalFinder.WebApi.Endpoints.Auth.Login.HttpResponseMapper.Others;
using Microsoft.AspNetCore.Http;

namespace GoalFinder.WebApi.Endpoints.Auth.Login.HttpResponseMapper;

/// <summary>
///     Login response status code
///     - user is locked out http response.
/// </summary>
internal sealed class UserIsLockedOutHttpResponse : LoginHttpResponse
{
    internal UserIsLockedOutHttpResponse(
        LoginRequest request,
        LoginResponse response)
    {
        HttpCode = StatusCodes.Status429TooManyRequests;
        AppCode = response.StatusCode.ToAppCode();
        ErrorMessages =
        [
            $"User with username = {request.Username} has been temporarily locked due to entering the wrong password too many times",
            $"Please try again after 15 seconds."
        ];
    }
}
