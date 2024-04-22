using Microsoft.AspNetCore.Http;
using GoalFinder.WebApi.Endpoints.Auth.RegisterAsUser.HttpResponseMapper.Others;
using GoalFinder.Application.Features.Auth.Register;

namespace GoalFinder.WebApi.Endpoints.Auth.RegisterAsUser.HttpResponseMapper;

/// <summary>
///     Register as user response status code -
///     database operation fail http response.
/// </summary>
internal sealed class DatabaseOperationFailHttpResponse : RegisterAsUserHttpResponse
{
    internal DatabaseOperationFailHttpResponse(RegisterAsUserResponse response)
    {
        HttpCode = StatusCodes.Status500InternalServerError;
        AppCode = response.StatusCode.ToAppCode();
        ErrorMessages =
        [
            "Server error !!!"
        ];
    }
}
