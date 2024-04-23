using GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.HttpResponseMapper.Others;
using GoalFinder.Application.Features.Auth.ForgotPassword;
using Microsoft.AspNetCore.Http;

namespace GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.HttpResponseMapper;

internal sealed class UserIsNotVerifyHttpResponse : ForgotPasswordHttpReponse
{
    internal UserIsNotVerifyHttpResponse(ForgotPasswordResponse response) 
    { 
        HttpCode = StatusCodes.Status404NotFound;
        AppCode = response.StatusCode.ToAppCode();
    }
}
