using GoalFinder.Application.Features.Auth.Register;
using GoalFinder.WebApi.Endpoints.Auth.RegisterAsUser.HttpResponseMapper.Others;
using Microsoft.AspNetCore.Http;

namespace GoalFinder.WebApi.Endpoints.Auth.RegisterAsUser.HttpResponseMapper;

/// <summary>
///     Register as user response status code
///     - user password is not valid
///     http response.
/// </summary>
internal sealed class UserPasswordIsNotValidHttpResponse : RegisterAsUserHttpResponse
{
    internal UserPasswordIsNotValidHttpResponse(RegisterAsUserResponse response)
    {
        HttpCode = StatusCodes.Status400BadRequest;
        AppCode = response.StatusCode.ToAppCode();
        ErrorMessages =
        [
            "Password is not valid."
        ];
    }
}
