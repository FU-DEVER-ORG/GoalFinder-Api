using GoalFinder.Application.Features.Auth.ForgotPassword;
using GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.HttpResponseMapper.Others;
using Microsoft.AspNetCore.Http;
using System.Reflection.Emit;

namespace GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.HttpResponseMapper;

internal sealed class DatabaseOperationFailHttpResponse : ForgotPasswordHttpReponse
{
    internal DatabaseOperationFailHttpResponse(ForgotPasswordResponse response)
    {
        HttpCode = StatusCodes.Status500InternalServerError;
        AppCode = response.StatusCode.ToAppCode();
    }
}
