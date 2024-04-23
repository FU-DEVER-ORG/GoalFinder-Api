using GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.HttpResponseMapper.Others;
using Microsoft.AspNetCore.Http;
using GoalFinder.Application.Features.Auth.ForgotPassword;

namespace GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.HttpResponseMapper;

internal sealed class UserWithEmailNotFoundHttpsResponse : ForgotPasswordHttpReponse
{
    internal UserWithEmailNotFoundHttpsResponse(ForgotPasswordResponse response)
    {
        HttpCode = StatusCodes.Status404NotFound;
        AppCode = response.StatusCode.ToAppCode(); 
    }
}
