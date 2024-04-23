using GoalFinder.Application.Features.Auth.ForgotPassword;
using GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.HttpResponseMapper.Others;
using Microsoft.AspNetCore.Http;

namespace GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.HttpResponseMapper;

internal sealed class InputValidationFailHttpResponse : ForgotPasswordHttpReponse
{
    internal InputValidationFailHttpResponse(ForgotPasswordResponse response)
    {
        HttpCode = StatusCodes.Status400BadRequest;
        AppCode = response.StatusCode.ToAppCode(); 
    }
}
