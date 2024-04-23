﻿using GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.HttpResponseMapper.Others;
using Microsoft.AspNetCore.Http;
using GoalFinder.Application.Features.Auth.ForgotPassword;

namespace GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.HttpResponseMapper;

internal sealed class UserIsTemporarilyRemovedHttpResponse : ForgotPasswordHttpReponse
{
    internal UserIsTemporarilyRemovedHttpResponse(ForgotPasswordResponse response)
    {
        HttpCode = StatusCodes.Status403Forbidden;
        AppCode = response.StatusCode.ToAppCode();
    }
}