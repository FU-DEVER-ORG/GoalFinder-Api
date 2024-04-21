using GoalFinder.Application.Features.Auth.Login;
using GoalFinder.WebApi.Endpoints.Auth.Login.HttpResponseMapper.Others;
using Microsoft.AspNetCore.Http;

namespace GoalFinder.WebApi.Endpoints.Auth.Login.HttpResponseMapper;

/// <summary>
///     Login response status code
///     - user is not found http response.
/// </summary>
internal sealed class UserIsNotFoundHttpResponse : LoginHttpResponse
{
    internal UserIsNotFoundHttpResponse(
        LoginRequest request,
        LoginResponse response)
    {
        HttpCode = StatusCodes.Status404NotFound;
        AppCode = response.StatusCode.ToAppCode();
        ErrorMessages =
        [
            $"User with username = {request.Username} is not found."
        ];
    }
}
