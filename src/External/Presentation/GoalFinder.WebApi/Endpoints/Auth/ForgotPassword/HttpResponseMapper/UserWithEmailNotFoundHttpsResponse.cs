﻿using GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.HttpResponseMapper.Others;
using Microsoft.AspNetCore.Http;
using GoalFinder.Application.Features.Auth.ForgotPassword;

namespace GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.HttpResponseMapper;
/// <summary>
/// User with email not found
/// </summary>
internal sealed class UserWithEmailNotFoundHttpsResponse : ForgotPasswordHttpReponse
{
    /// <summary>
    ///     User with email not found
    /// </summary>
    /// <param name="response"></param>
    internal UserWithEmailNotFoundHttpsResponse(ForgotPasswordResponse response)
    {
        HttpCode = StatusCodes.Status404NotFound;
        AppCode = response.StatusCode.ToAppCode(); 
    }
}
