using GoalFinder.Application.Features.Auth.Register;
using GoalFinder.WebApi.Endpoints.Auth.RegisterAsUser.HttpResponseMapper.Others;
using Microsoft.AspNetCore.Http;

namespace GoalFinder.WebApi.Endpoints.Auth.RegisterAsUser.HttpResponseMapper;

/// <summary>
///     Register as user response status code
///     - user is existed http response.
/// </summary>
internal sealed class UserIsExistedHttpResponse : RegisterAsUserHttpResponse
{
    internal UserIsExistedHttpResponse(
        RegisterAsUserRequest request,
        RegisterAsUserResponse response)
    {
        HttpCode = StatusCodes.Status409Conflict;
        AppCode = response.StatusCode.ToAppCode();
        ErrorMessages =
        [
            $"User with username = {request.Email} already exists"
        ];
    }
}
