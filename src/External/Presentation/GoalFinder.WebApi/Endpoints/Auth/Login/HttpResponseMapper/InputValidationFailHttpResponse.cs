using GoalFinder.Application.Features.Auth.Login;
using GoalFinder.WebApi.Endpoints.Auth.Login.HttpResponseMapper.Others;
using Microsoft.AspNetCore.Http;

namespace GoalFinder.WebApi.Endpoints.Auth.Login.HttpResponseMapper;

/// <summary>
///     Login response status code
///     - input validation fail http
///     response.
/// </summary>
internal sealed class InputValidationFailHttpResponse : LoginHttpResponse
{
    internal InputValidationFailHttpResponse(LoginResponse response)
    {
        HttpCode = StatusCodes.Status400BadRequest;
        AppCode = response.StatusCode.ToAppCode();
        ErrorMessages =
        [
            "Input validation fail. Please check your inputs and try again."
        ];
    }
}
