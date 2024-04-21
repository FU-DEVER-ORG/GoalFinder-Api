
using System.Collections.Generic;
using System;
using GoalFinder.Application.Features.Auth.ForgotPassword;
using GoalFinder.Application.Features.Auth.Login;

namespace GoalFinder.WebApi.Endpoints.Auth.ForgotPassword.HttpResponseMapper.Others;

internal sealed class ForgotPasswordHttpResponseManager
{
    private readonly Dictionary<
        ForgotPasswordReponseStatusCode,
        Func<
            ForgotPasswordRequest,
            ForgotPasswordResponse,
            ForgotPasswordHttpReponse>>
                _dictionary;
    internal ForgotPasswordHttpResponseManager()
    {
        _dictionary = [];
        _dictionary.Add(
           key: ForgotPasswordReponseStatusCode.OPERATION_SUCCESS,
           value: (_, response) => new OperationSuccessHttpResponse(response: response));
    }

    internal Func<
       ForgotPasswordRequest,
       ForgotPasswordResponse,
       ForgotPasswordHttpReponse>
           Resolve(ForgotPasswordReponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
