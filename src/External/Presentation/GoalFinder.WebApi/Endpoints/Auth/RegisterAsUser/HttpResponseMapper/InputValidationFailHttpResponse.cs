using Microsoft.AspNetCore.Http;
using GoalFinder.WebApi.Endpoints.Auth.RegisterAsUser.HttpResponseMapper.Others;
using GoalFinder.Application.Features.Auth.Register;

namespace GoalFinder.WebApi.Endpoints.Auth.RegisterAsUser.HttpResponseMapper;

/// <summary>
///     Register as user response status code
///     - input validation fail http
///     response.
/// </summary>
internal sealed class InputValidationFailHttpResponse : RegisterAsUserHttpResponse
{
    internal InputValidationFailHttpResponse(RegisterAsUserResponse response)
    {
        HttpCode = StatusCodes.Status400BadRequest;
        AppCode = response.StatusCode.ToAppCode();
        ErrorMessages =
        [
            "Input validation fail. Please check your inputs and try again."
        ];
    }
}
