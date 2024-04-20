using GoalFinder.Application.Features.Auth.Login;
using GoalFinder.WebApi.Endpoints.Auth.Login.HttpResponseMapper.Others;
using Microsoft.AspNetCore.Http;

namespace GoalFinder.WebApi.Endpoints.Auth.Login.HttpResponseMapper;

/// <summary>
///     Login response status code - database operation
///     fail http response.
/// </summary>
internal sealed class DatabaseOperationFailHttpResponse : LoginHttpResponse
{
    internal DatabaseOperationFailHttpResponse(LoginResponse response)
    {
        HttpCode = StatusCodes.Status500InternalServerError;
        AppCode = response.StatusCode.ToAppCode();
        ErrorMessages =
        [
            "Server error !!!"
        ];
    }
}
