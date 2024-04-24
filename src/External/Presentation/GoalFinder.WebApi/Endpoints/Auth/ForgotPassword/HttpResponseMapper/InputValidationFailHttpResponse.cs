using GoalFinder.Application.Features.Auth.ForgotPassword;
using GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.HttpResponseMapper.Others;
using Microsoft.AspNetCore.Http;

namespace GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.HttpResponseMapper;

/// <summary>
///     Input validation fail http response
/// </summary>
internal sealed class InputValidationFailHttpResponse : ForgotPasswordHttpReponse
{
    /// <summary>
    ///     Input validation fail http response constructor
    /// </summary>
    /// <param name="response"></param>
    internal InputValidationFailHttpResponse(ForgotPasswordResponse response)
    {
        HttpCode = StatusCodes.Status400BadRequest;
        AppCode = response.StatusCode.ToAppCode();
        ErrorMessages =
        [
           "Input validation fail. Please check your inputs and try again."
        ];
    }
}
